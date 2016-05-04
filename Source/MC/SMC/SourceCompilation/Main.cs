namespace SMC.SourceCompilation
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;
    
    using SMC.SourceCompilation.Contracts;
    using SMC.SourceCompilation.Logic;
    using SMC.SourceLib.Intermediate;

    public class Main : ConsoleApplicationBase, IMain
    {
        private static readonly CarbonDirectory SourceTargetDir = new CarbonDirectory("Source");

        private readonly ICompiler compiler;
        private readonly ICompilationState compilerState;
        
        private readonly IList<CarbonDirectory> includes;

        private readonly IDictionary<CompilationJob, int> jobs;

        private readonly IDictionary<string, CompilationJob> includeJobDictionary;
        
        private CarbonFile projectFileName;
        private CarbonDirectory targetDirectory;

        private bool skipProjectUpdate;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory)
            : base(factory)
        {
            this.compiler = factory.Resolve<ICompiler>();
            this.compilerState = factory.Resolve<ICompilationState>();

            this.includes = new List<CarbonDirectory>();
            this.jobs = new Dictionary<CompilationJob, int>();
            this.includeJobDictionary = new Dictionary<string, CompilationJob>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "SMC.SourceCompilation";

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void StartFinished()
        {
            // Check if we are compiling a project
            if (this.projectFileName != null)
            {
                this.DoCompileProject();
            }
        }

        protected override bool RegisterCommandLineArguments()
        {
            // -I Partials -I Overrides -o ..\..\..\..\Source -p ..\..\..\..\SharpMC.jtlproj
            ICommandLineSwitchDefinition definition = this.Arguments.Define("p", "projectFile", x => this.projectFileName = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "The project file to compile";

            definition = this.Arguments.Define("o", "output", x => this.targetDirectory = new CarbonDirectory(x));
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "Target directory for the compilation";

            definition = this.Arguments.Define("I", "include", x => this.includes.Add(new CarbonDirectory(x)));
            definition.RequireArgument = true;
            definition.AllowMultiple = true;
            definition.Description = "Include directories for files to be included in the project file";

            definition = this.Arguments.Define("s", "skipProjectUpdate", x => this.skipProjectUpdate = true);
            definition.Description = "Skip project file re-generation";
            definition.RequireArgument = false;

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoCompileProject()
        {
            var project = JsonExtensions.LoadFromFile<TempProject>(this.projectFileName, false);
            if (!string.IsNullOrEmpty(project.Name))
            {
                this.compilerState.BaseNameSpace = project.Name;
            }

            // Set a default target directory if we have none
            if (this.targetDirectory == null || this.targetDirectory.IsNull)
            {
                this.targetDirectory = new CarbonDirectory(this.projectFileName.DirectoryName);
                if (this.targetDirectory == null || this.targetDirectory.IsNull)
                {
                    this.targetDirectory = RuntimeInfo.WorkingDirectory;
                }
            }
            
            if (this.targetDirectory.IsRelative)
            {
                CarbonDirectory root = RuntimeInfo.WorkingDirectory;
                if (!this.projectFileName.IsRelative)
                {
                    root = this.projectFileName.GetDirectory();
                }

                this.targetDirectory = this.targetDirectory.ToAbsolute<CarbonDirectory>(root);
            }
            
            // Check if we have a template to generate a project file and are not skipping
            //  if not we will only compile sources
            IProjectFile projectFile = null;
            if (!string.IsNullOrEmpty(project.TemplateFile) && !this.skipProjectUpdate)
            {
                projectFile = new ProjectFile(this.targetDirectory.ToFile(project.TemplateFile));
            }
            
            // Create jobs for each class in each file
            foreach (TempProjectFileEntry entry in project.Files)
            {
                CompilationJob job = this.CreateJob(project, entry);
                this.jobs.Add(job, 0);
            }

            using (new ProfileRegion("Checking Dependencies"))
            {
                this.RebuildDependencyInfo();
            }

            this.CompileJobs();
            
            // Update the project file
            if (projectFile != null)
            {
                this.ProcessIncludes();

                CarbonFile targetProjectFile = this.targetDirectory.ToFile(this.projectFileName.FileNameWithoutExtension + ".csproj");
                projectFile.Save(targetProjectFile);
            }
        }

        private void ProcessIncludes()
        {
        }

        private CompilationJob CreateJob(TempProject project, TempProjectFileEntry entry)
        {
            var job = new CompilationJob
            {
                IsCompressed = entry.IsCompressed,
                NameSpace = this.compilerState.BaseNameSpace
            };

            if (entry.RelativePath != null && !entry.RelativePath.IsNull)
            {
                string fileNameSpace = this.compiler.GetNameSpace(entry.RelativePath);
                job.NameSpace = string.Join(Compiler.NameSpaceSeparator.ToString(CultureInfo.InvariantCulture), job.NameSpace, fileNameSpace);
            }

            job.NameSpace = this.GetNameSpace(project.RootNameSpace, entry);

            CarbonDirectory fileTargetDirectory = this.targetDirectory.ToDirectory(SourceTargetDir);
            if (entry.RelativePath != null)
            {
                fileTargetDirectory = fileTargetDirectory.ToDirectory(entry.RelativePath);
            }

            job.SourceFile = this.targetDirectory.ToFile(entry.File);
            job.TargetFile = fileTargetDirectory.ToFile(entry.RelativeFile);

            if (entry.Classes != null)
            {
                foreach (string @class in entry.Classes)
                {
                    var classNameSpace = string.Concat(job.NameSpace, Compiler.NameSpaceSeparator, @class);
                    if (this.includeJobDictionary.ContainsKey(classNameSpace))
                    {
                        System.Diagnostics.Trace.TraceWarning("Multiple class definitions for " + classNameSpace);
                        continue;
                    }

                    this.includeJobDictionary.Add(classNameSpace, job);
                }
            }

            return job;
        }

        private void RebuildDependencyInfo()
        {
            // Reset the dependency order
            IList<CompilationJob> jobList = new List<CompilationJob>(this.jobs.Keys);
            foreach (CompilationJob job in jobList)
            {
                this.jobs[job] = 0;
            }

            IDictionary<CompilationJob, IList<CompilationJob>> jobDependencyCache = new Dictionary<CompilationJob, IList<CompilationJob>>();
            IList<CompilationJob> stackCheck = new List<CompilationJob>();
            foreach (CompilationJob job in jobList)
            {
                this.RebuildDependencyInfo(job, jobDependencyCache, stackCheck);
            }
        }

        private void RebuildDependencyInfo(CompilationJob job, IDictionary<CompilationJob, IList<CompilationJob>> cache, IList<CompilationJob> stackCheck)
        {
            if (stackCheck.Contains(job))
            {
                System.Diagnostics.Trace.TraceWarning("Potential recursive Dependencies in {0} and {1}", job.NameSpace, stackCheck.Last().NameSpace);
                return;
            }

            stackCheck.Add(job);

            // Check if we have this job in the cache
            if (cache.ContainsKey(job))
            {
                foreach (CompilationJob dependentJob in cache[job])
                {
                    this.jobs[dependentJob]++;
                    this.RebuildDependencyInfo(dependentJob, cache, stackCheck);
                }

                stackCheck.Remove(job);
                return;
            }

            // If not load the cache info and re-start the rebuild
            cache.Add(job, new List<CompilationJob>());
            TempFileLight file = JsonExtensions.LoadFromFile<TempFileLight>(job.SourceFile, job.IsCompressed);
            if (file.Includes != null)
            {
                foreach (string include in file.Includes)
                {
                    if (this.includeJobDictionary.ContainsKey(include))
                    {
                        CompilationJob dependentJob = this.includeJobDictionary[include];
                        cache[job].Add(dependentJob);
                    }
                }
            }

            stackCheck.Remove(job);
            this.RebuildDependencyInfo(job, cache, stackCheck);
        }

        private string GetNameSpace(string rootNameSpace, TempProjectFileEntry entry)
        {
            if (entry.RelativePath == null)
            {
                return rootNameSpace;
            }

            string relativePath = entry.RelativePath.GetPath();
            if (string.IsNullOrEmpty(relativePath))
            {
                return rootNameSpace;
            }

            relativePath = relativePath.ToLowerInvariant().Replace(@"\", ".").TrimEnd('.');
            return string.Format("{0}.{1}", rootNameSpace, relativePath);
        }

        private void CompileJobs()
        {
            var compileGroups = new Dictionary<int, IList<CompilationJob>>();
            foreach (KeyValuePair<CompilationJob, int> entry in this.jobs.OrderByDescending(x => x.Value))
            {
                if (!compileGroups.ContainsKey(entry.Value))
                {
                    compileGroups.Add(entry.Value, new List<CompilationJob>());
                }

                compileGroups[entry.Value].Add(entry.Key);
            }

            foreach (IList<CompilationJob> group in compileGroups.Values)
            {
                Task[] groupTasks = new Task[group.Count];
                for (var i = 0; i < group.Count; i++)
                {
                    var closure = group[i];
                    Task task = new Task(() => this.compiler.Compile(this.compilerState, closure));
                    groupTasks[i] = task;
                    task.Start();
                }

                Task.WaitAll(groupTasks);
            }
        }
    }
}
