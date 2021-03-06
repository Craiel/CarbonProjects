﻿namespace CarbonCore.Applications.CrystalBuild.JavaScript
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts;
    using CarbonCore.Applications.CrystalBuild.JavaScript.Logic;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.I18N;
    using CarbonCore.Utils.IO;

    public class Main : ConsoleApplicationBase, IMain
    {
        // --compilation_level ADVANCED_OPTIMIZATIONS
        public const string ClosureCompilerCommand = @"-jar ""{0}compiler.jar""  --js ""{1}"" --js_output_file {2} {3} --externs ""{4}""";
        
        private readonly IConfig config;
        private readonly IBuildLogic logic;

        private CarbonFile configFileName;

        private bool useDebug;
        private bool useClosure;

        private string targetLanguage;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory)
            : base(factory)
        {
            this.config = factory.Resolve<IConfig>();
            this.logic = factory.Resolve<IBuildLogic>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "CrystalBuild";

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void StartFinished()
        {
            if (this.configFileName == null)
            {
                this.Arguments.PrintArgumentUse();
                return;
            }

            if (!string.IsNullOrEmpty(this.targetLanguage))
            {
                Localization.CurrentCulture = LocaleConstants.GetCulture(this.targetLanguage);
            }

            this.config.Load(this.configFileName);
            this.DoBuildProject();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("p", "projectFile", x => this.configFileName = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Description = "The project file to compile";

            definition = this.Arguments.Define("d", "debug", x => this.useDebug = true);
            definition.Description = "Build with debug info enabled";

            definition = this.Arguments.Define("c", "closure", x => this.useClosure = true);
            definition.Description = "Run closure on the target script file";

            definition = this.Arguments.Define("l", "language", x => this.targetLanguage = x);
            definition.RequireArgument = true;
            definition.Description = "Set the language to build (en, fr, de ...)";

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoBuildProject()
        {
            var cache = new JavaScriptBuildingCache();

            if (this.config.Current.Images != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.Images;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                if (files.Count > 0)
                {
                    var context = new JavaScriptBuildingContext(cache, this.config.Current.TargetPlatform) { Root = this.config.Current.ImageRoot };

                    this.logic.BuildImages(files, context);
                }
                else
                {
                    Diagnostic.Warning("No resources to register!");
                }
            }

            if (this.config.Current.Data != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.Data;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                if (files != null && files.Count > 0)
                {
                    this.logic.BuildData(files, this.config.Current.ProjectRoot.ToFile(this.config.Current.DataTarget), new JavaScriptBuildingContext(cache, this.config.Current.TargetPlatform));
                }
                else
                {
                    Diagnostic.Warning("No data found to build!");
                }
            }

            if (this.config.Current.Templates != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.Templates;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                if (files != null && files.Count > 0)
                {
                    this.logic.BuildTemplates(files, this.config.Current.ProjectRoot.ToFile(this.config.Current.TemplateTarget), new JavaScriptBuildingContext(cache, this.config.Current.TargetPlatform));
                }
                else
                {
                    Diagnostic.Warning("No templates found to build!");
                }
            }

            if (this.config.Current.Sources != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.Sources;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                CarbonFileResult mainFile = null;

                // Check if we have a specific main file
                if (this.config.Current.SourceMain != null)
                {
                    // Now we try to find the entry point file
                    foreach (var file in files)
                    {
                        if (file.Absolute.GetPath()
                            .EndsWith(this.config.Current.SourceMain.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            mainFile = file;
                            break;
                        }
                    }

                    if (mainFile == null)
                    {
                        Diagnostic.Error(
                            "Could not find entry point: {0} ({1} files)",
                            this.config.Current.SourceMain,
                            files.Count);
                        return;
                    }

                    // re-add the main file so it ends up as last in the block
                    files.Remove(mainFile);
                    files.Add(mainFile);
                }

                if (files.Count > 0)
                {
                    CarbonFile targetFile = this.config.Current.ProjectRoot.ToFile(this.config.Current.SourceTarget);
                    CarbonFile targetFileClosure = targetFile;
                    if (this.useClosure)
                    {
                        targetFile = targetFile.GetDirectory().ToFile(targetFile.FileNameWithoutExtension + "_raw.js");
                    }

                    this.logic.Build(
                        files,
                        targetFile,
                        new JavaScriptBuildingContext(cache, this.config.Current.TargetPlatform)
                        {
                            Name = this.config.Current.Name,
                            KeepComments = this.config.Current.KeepComments,
                            IsDebug = this.useDebug,
                            ExportSourceAsModule = this.config.Current.ExportSourceAsModule
                        });

                    if (this.useClosure)
                    {
                        string closureArgs = "--language_in=ECMASCRIPT5";
                        if (!string.IsNullOrEmpty(this.config.Current.CustomClosureCompilerCommands))
                        {
                            closureArgs = this.config.Current.CustomClosureCompilerCommands;
                        }

                        var info = new ProcessStartInfo("java.exe", string.Format(ClosureCompilerCommand, RuntimeInfo.Assembly.GetDirectory().ToRelative<CarbonDirectory>(RuntimeInfo.WorkingDirectory), targetFile, targetFileClosure, closureArgs, RuntimeInfo.WorkingDirectory.ToFile("externs.js")))
                                       {
                                           UseShellExecute = false,
                                           WorkingDirectory = RuntimeInfo.WorkingDirectory.ToString()
                                       };
                        var proc = Process.Start(info);
                        proc.WaitForExit();
                    }
                }
                else
                {
                    Diagnostic.Warning("No source files found to build!");
                }
            }

            if (this.config.Current.StyleSheets != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.StyleSheets;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                if (files != null && files.Count > 0)
                {
                    this.logic.BuildStyleSheets(files, this.config.Current.ProjectRoot.ToFile(this.config.Current.StyleSheetTarget), new JavaScriptBuildingContext(cache, this.config.Current.TargetPlatform));
                }
                else
                {
                    Diagnostic.Warning("No templates found to build!");
                }
            }

            if (this.config.Current.Contents != null)
            {
                IList<CarbonDirectoryFilter> filters = this.config.Current.Contents;
                IList<CarbonFileResult> files = CarbonDirectory.GetFiles(filters);
                if (files.Count > 0)
                {
                    this.logic.CopyContents(files, this.config.Current.ProjectRoot.ToDirectory(this.config.Current.ContentTarget));
                }
                else
                {
                    Diagnostic.Warning("No content files found to copy!");
                }
            }

            foreach (string key in cache.ImageUseCount.Keys)
            {
                if (cache.ImageUseCount[key] <= 0)
                {
                    Diagnostic.Warning("Unused image: {0}", key);
                }
            }
        }
    }
}
