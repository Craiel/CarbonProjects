namespace SMC.SourceTranslation.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CarbonCore.GrammarParser.Contracts;
    using CarbonCore.GrammarParser.Contracts.Grammars;
    using CarbonCore.GrammarParser.Tokenize;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using Newtonsoft.Json;

    using SMC.SourceLib.Intermediate;

    using SourceTranslation.Contracts;

    public partial class Translation : ITranslation
    {
        private const string TempExtension = ".stl";

        private readonly IConfig config;

        private readonly IGrammar grammar;

        private CarbonDirectory targetDirectory;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Translation(IConfig config, IJavaGrammar grammar)
        {
            this.config = config;

            this.grammar = grammar;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public long TokenCount { get; set; }
        public long LinesTranslated { get; set; }

        public void Translate(IDictionary<CarbonFileResult, CarbonFile> files)
        {
            this.targetDirectory = this.config.Current.ProjectRoot.ToDirectory(this.config.Current.TargetDirectory ?? new CarbonDirectory(string.Empty));
            this.targetDirectory.Create();

            var project = new TempProject
                              {
                                  Name = this.config.Current.ProjectName,
                                  TemplateFile = this.config.Current.ProjectTemplate,
                                  FileSourceRoot = this.config.Current.IntermediateSubDirectory.GetPath(),
                                  FileTargetRoot = this.config.Current.TargetSubDirectory.GetPath(),
                                  RootNameSpace = this.config.Current.RootNameSpace
                              };

            IList<Task> tasks = new List<Task>();
            foreach (CarbonFileResult sourceFile in files.Keys)
            {
                CarbonFile targetFile = files[sourceFile];
                if (this.config.Current.ProcessParrallel)
                {
                    CarbonFile closure = sourceFile.Absolute;
                    tasks.Add(Task.Factory.StartNew(() => this.TranslateFile(project, closure, targetFile)));
                }
                else
                {
                    this.TranslateFile(project, sourceFile.Absolute, targetFile);
                }
            }

            Task.WaitAll(tasks.ToArray());

            this.targetDirectory.Create();
            CarbonFile outFile = this.targetDirectory.ToFile(project.Name + this.config.Current.IntermediateProjectExtension);
            JsonExtensions.SaveToFile(outFile, project, false, Formatting.Indented);
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void TranslateFile(TempProject project, CarbonFile sourceFile, CarbonFile targetFile)
        {
            CarbonDirectory targetRelativeSubDir = targetFile.GetDirectory() ?? new CarbonDirectory(string.Empty);
            CarbonDirectory fullTargetPath = this.targetDirectory.ToDirectory(this.config.Current.IntermediateSubDirectory, targetRelativeSubDir);
            CarbonFile fullTargetFile = fullTargetPath.ToFile(targetFile.FileName + TempExtension);
            CarbonFile relativeTargetFile = this.config.Current.IntermediateSubDirectory.ToDirectory(targetRelativeSubDir).ToFile(targetFile.FileName + TempExtension);

            if (this.config.Current.Verbose)
            {
                System.Diagnostics.Trace.TraceInformation("Translating {0} -> {1}", sourceFile, fullTargetFile);
            }

            IList<Token> tokens = this.TokenizeFile(sourceFile);
            if (tokens == null)
            {
                throw new InvalidDataException();
            }

            project.AddStat(TempProjectStat.Files);
            project.AddStat(TempProjectStat.Tokens, tokens.Count);
            var fileEntry = new TempProjectFileEntry
                                {
                                    IsCompressed = this.config.Current.CompressIntermediate,
                                    RelativeFile = new CarbonFile(targetFile.FileName),
                                    RelativePath = targetFile.GetDirectory(),
                                    File = relativeTargetFile,
                                };

            var data = new TranslationData(tokens)
                           {
                               ProjectData = project,
                               SourceFile = sourceFile,
                               TargetFile = fullTargetFile,
                               FileEntry = fileEntry
                           };

            // Register the file entry into the project
            project.AddFile(fileEntry);

            try
            {
                TempFileFull file = this.TranslateToTempFile(data);
                this.SaveTempFile(data, file);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("Failed to translate {0}: {1}", fileEntry.File, e);
                this.SaveTempFile(data, new TempFileFull() { Name = data.TargetFile.FileName });
            }
        }

        private void SaveTempFile(TranslationData data, TempFileFull file)
        {
            if (this.config.Current.CompressIntermediate)
            {
                JsonExtensions.SaveToFile(data.TargetFile, file);
            }
            else
            {
                JsonExtensions.SaveToFile(data.TargetFile, file, false, Formatting.Indented);
            }
        }

        private TempFileFull TranslateToTempFile(TranslationData data)
        {
            data.CurrentToken = 0;

            // Create the root data element
            var file = new TempFileFull { Name = data.TargetFile.FileName, StructureType = TempFileStructureType.Java };
            data.TranslationStack.Push(file);

            while (data.CurrentToken < data.Tokens.Count)
            {
                TempData currentData = data.TranslationStack.Peek();
                switch (currentData.Type)
                {
                    case TempDataType.File:
                        {
                            this.TranslateTempFile(data, (TempFileFull)currentData);
                            break;
                        }

                    case TempDataType.Class:
                        {
                            this.TranslateTempClass(data, (TempClass)currentData);
                            break;
                        }

                    case TempDataType.Function:
                        {
                            this.TranslateTempFunction(data, (TempFunction)currentData);
                            break;
                        }

                    case TempDataType.Member:
                        {
                            this.TranslateTempMember(data);
                            break;
                        }

                    default:
                        {
                            throw new NotImplementedException("Unknown translation data type: " + currentData.Type);
                        }
                }
            }

            return file;
        }

        private IList<Token> TokenizeFile(CarbonFile file)
        {
            IList<Token> tokens;
            var tokenizer = new Tokenizer();
            using (new ProfileRegion("Tokenize"))
            {
                using (var stream = file.OpenRead())
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8, false, 4096, true))
                    {
                        tokens = tokenizer.Tokenize(this.grammar, reader);
                    }
                }
            }

            return tokens;
        }
    }
}
