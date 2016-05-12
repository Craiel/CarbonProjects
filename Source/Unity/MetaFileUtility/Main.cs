namespace MetaFileUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;

    using MetaFileUtility.Contracts;
    using MetaFileUtility.Logic.Enums;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IFactory factory;
        private readonly IConfig config;

        private readonly IList<CarbonFile> existingMetaFiles;

        private CarbonDirectory projectDirectory;

        private CarbonDirectory assetDirectory;
        
        private MetaUtilityMode mode;

        private CarbonFile configFile;

        private bool enableP4Check;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory) : base(factory)
        {
            this.factory = factory;

            this.config = factory.Resolve<IConfig>();

            this.existingMetaFiles = new List<CarbonFile>();

            this.configFile = new CarbonFile(Constants.ConfigFileName);

            this.mode = MetaUtilityMode.Check;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "MetaFileUtility";

        protected override void StartFinished()
        {
            if (this.projectDirectory == null)
            {
                this.Arguments.PrintArgumentUse();
                return;
            }

            if (!this.projectDirectory.Exists)
            {
                Diagnostic.Error("Directory does not exist: {0}", this.projectDirectory);
                return;
            }
            
            this.config.Load(this.configFile);

            this.assetDirectory = this.projectDirectory.ToDirectory(Constants.DirectoryAssets);

            switch (this.mode)
            {
                case MetaUtilityMode.Config:
                    {
                        this.config.Save(this.configFile);
                        Diagnostic.Info("Config Generated as {0}", this.configFile);
                        break;
                    }

                case MetaUtilityMode.Info:
                    {
                        this.PrintInfo();
                        break;
                    }

                case MetaUtilityMode.Check:
                    {
                        this.CheckMetaFiles();
                        break;
                    }

                default:
                case MetaUtilityMode.Help:
                    {
                        this.Arguments.PrintArgumentUse();
                        break;
                    }
            }
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("d", "directory", x => this.projectDirectory = new CarbonDirectory(x));
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "The directory to run on";
            
            definition = this.Arguments.Define("c", "configFile", x => this.configFile = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Description = "Specify a custom MetaFileUtility config";
            
            definition = this.Arguments.Define("m", "mode", x => CarbonCore.Utils.Edge.EnumExtensions.TryParseInvariant(x, out this.mode));
            definition.RequireArgument = true;
            definition.Description = "Show info about the folder and the environment";

            definition = this.Arguments.Define("p4", x => this.enableP4Check = true);
            definition.Description = "Enable check of meta files against p4";

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void PrintInfo()
        {
            Diagnostic.Info("Working Directory: {0}", this.config.Current.WorkingDirectory);
            Diagnostic.Info("Project Directory: {0}", this.projectDirectory);
            Diagnostic.Info("Asset Directory: {0}", this.assetDirectory);

            Console.WriteLine();
            Diagnostic.Info("  -----  Environment Variables -----");
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine("  {0} = {1}", de.Key, de.Value);
            }

            Console.WriteLine();
            Console.WriteLine();

            Directory.SetCurrentDirectory(this.projectDirectory.GetPath());
            Console.Write(this.ExecuteP4("set", false));

            Console.WriteLine();
            
            Directory.SetCurrentDirectory(this.config.Current.WorkingDirectory.GetPath());
        }

        private string ExecuteP4(string args, bool silent = true)
        {
            try
            {
                // Start the child process.
                using (Process process = new Process())
                {
                    // Redirect the output stream of the child process.
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = "p4";
                    process.StartInfo.Arguments = args;

                    if (!silent)
                    {
                        Diagnostic.Info(
                            "Executing \"{0} {1}\"",
                            process.StartInfo.FileName,
                            process.StartInfo.Arguments);
                    }

                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output;
                }
            }
            catch (Exception e)
            {
                Diagnostic.Error("Execute of P4 failed: {0}", e);
                return e.ToString();
            }
        }

        private void CheckMetaFiles()
        {
            this.ScanMetaFiles();

            Diagnostic.Info("Found {0} Meta Files", this.existingMetaFiles.Count);

            if (!this.CheckDirectoryWithoutMeta() && this.config.Current.ErrorOnMissingDirectoryMetaFiles)
            {
                this.ExitCode = -1;
                return;
            }

            if (!this.CheckFileWithoutMeta() && this.config.Current.ErrorOnMissingFileMetaFiles)
            {
                this.ExitCode = -2;
                return;
            }

            if (!this.CheckMetaWithoutFile() && this.config.Current.ErrorOnOrphanMetaFiles)
            {
                this.ExitCode = -3;
                return;
            }

            if (this.enableP4Check && !this.CheckMetaFileInP4() && this.config.Current.ErrorOnP4Check)
            {
                Diagnostic.Error("Not all Meta files where in p4, check the log for details!");
                this.ExitCode = -4;
            }

            Diagnostic.Info("Finished with Exit Code {0}\n", this.ExitCode);
        }

        private bool CheckMetaFileInP4()
        {
            bool result = true;

            const int batchSize = 200;
            const string baseCommand = "files ";

            Directory.SetCurrentDirectory(this.projectDirectory.GetPath());
            using (new ProfileRegion("CheckMetaFileInP4"))
            {
                StringBuilder batchBuilder = new StringBuilder(baseCommand);
                int batchCounter = 0;
                foreach (CarbonFile file in this.existingMetaFiles)
                {
                    if (file.GetPath().Contains("@") || file.GetPath().Contains("#"))
                    {
                        // Skip these for now, the p4 command will fail
                        continue;
                    }

                    batchBuilder.Append($"\"{file.GetPath()}\" ");
                    batchCounter++;
                    if (batchCounter == batchSize - 1)
                    {
                        if (!this.ExecuteP4Batch(batchBuilder.ToString(), batchSize))
                        {
                            result = false;
                        }

                        batchBuilder.Clear();
                        batchBuilder.Append(baseCommand);
                        batchCounter = 0;
                    }
                }
            }

            Directory.SetCurrentDirectory(this.config.Current.WorkingDirectory.GetPath());

            return result;
        }

        private bool ExecuteP4Batch(string batch, int expected)
        {
            string status = this.ExecuteP4(batch);
            string[] lines = status.Split('\n');
            return lines.Length == expected;
        }

        private void ScanMetaFiles()
        {
            CarbonFileResult[] metaResults = this.assetDirectory.GetFiles("*" + Constants.ExtensionMeta, SearchOption.AllDirectories);

            this.existingMetaFiles.Clear();
            foreach (CarbonFileResult result in metaResults)
            {
                this.existingMetaFiles.Add(result.Absolute);
            }
        }

        private bool CheckFileWithoutMeta()
        {
            bool result = true;
            using (new ProfileRegion("CheckFileWithoutMeta"))
            {
                CarbonFileResult[] files = this.assetDirectory.GetFiles(options: SearchOption.AllDirectories);
                Diagnostic.Info("Checking {0} Files for existence of Meta files...", files.Length);
                foreach (CarbonFileResult fileResult in files)
                {
                    if (fileResult.Absolute.Extension.Equals(Constants.ExtensionMeta))
                    {
                        continue;
                    }

                    CarbonFile expectedMetaFile = fileResult.Absolute.GetDirectory().ToFile(fileResult.Absolute.FileName + Constants.ExtensionMeta);
                    if (expectedMetaFile.Exists)
                    {
                        continue;
                    }

                    Diagnostic.Error("Missing Meta file for {0}", fileResult.Absolute);
                    result = false;
                }
            }

            return result;
        }

        private bool CheckDirectoryWithoutMeta()
        {
            bool result = true;

            using (new ProfileRegion("CheckDirectoryWithoutMeta"))
            {
                CarbonDirectoryResult[] directoryResults =
                    this.assetDirectory.GetDirectories(options: SearchOption.AllDirectories);

                Diagnostic.Info("Checking Meta files for {0} Directories...", directoryResults.Length);
                foreach (CarbonDirectoryResult directory in directoryResults)
                {
                    CarbonFile directoryMeta;
                    CarbonDirectory parentDirectory = directory.Absolute.GetParent();
                    if (parentDirectory != null)
                    {
                        directoryMeta =
                            parentDirectory.ToFile(
                                directory.Absolute.DirectoryNameWithoutPath + Constants.ExtensionMeta);
                    }
                    else
                    {
                        directoryMeta =
                            new CarbonFile(directory.Relative.DirectoryNameWithoutPath + Constants.ExtensionMeta);
                    }

                    if (!directoryMeta.Exists)
                    {
                        Diagnostic.Error("Missing Meta file for Directory {0}", directory.Relative);
                        result = false;
                    }
                }
            }

            return result;
        }

        private bool CheckMetaWithoutFile()
        {
            Diagnostic.Info("Checking for orphan Meta files");

            using (new ProfileRegion("CheckMetaWithoutFile"))
            {
                bool result = true;
                foreach (CarbonFile file in this.existingMetaFiles)
                {
                    CarbonFile sourceFile = file.GetDirectory().ToFile(file.FileNameWithoutExtension);
                    if (sourceFile.Exists)
                    {
                        continue;
                    }

                    CarbonDirectory sourceDirectory = file.GetDirectory().ToDirectory(file.FileNameWithoutExtension);
                    if (sourceDirectory.Exists)
                    {
                        continue;
                    }

                    Diagnostic.Error("Meta file without corresponding file or directory: {0}", file);
                    result = false;
                }

                return result;
            }
        }
    }
}
