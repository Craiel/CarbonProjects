namespace SMC.SourceTranslation
{
    using System.Collections.Generic;
    using System.IO;

    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;

    using SourceTranslation.Contracts;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IFactory factory;
        private readonly IConfig config;
        
        private CarbonDirectory projectDirectory;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory)
            : base(factory)
        {
            this.factory = factory;
            this.config = factory.Resolve<IConfig>();
        }
        
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "SMC.SourceTranslation";

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void StartFinished()
        {
            this.config.Load(this.projectDirectory.ToFile(Constants.ConfigFileName));
            this.DoTranslate();
            this.config.Save();
        }

        protected override bool RegisterCommandLineArguments()
        {
            // -I Bridge -I Partials -I Overrides -o ..\..\..\ -t ..\..\..\SMC.template -p ..\..\..\SharpMC.jtlproj
            ICommandLineSwitchDefinition definition = this.Arguments.Define("p", "projectLocation", x => this.projectDirectory = new CarbonDirectory(x));
            definition.Required = true;
            definition.RequireArgument = true;
            definition.Description = "The project location containing " + Constants.ConfigFileName;

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoTranslate()
        {
            CarbonDirectory sourceDirectory = this.config.Current.SourceDirectory;
            if (this.config.Current.SourceDirectory.IsRelative)
            {
                sourceDirectory = this.config.Current.ProjectRoot.ToDirectory(sourceDirectory);
            }

            CarbonFileResult[] sources = sourceDirectory.GetFiles(this.config.Current.Filter, SearchOption.AllDirectories);
            System.Diagnostics.Trace.TraceInformation("Found {0} files to translate", sources.Length);

            IDictionary<CarbonFileResult, CarbonFile> files = new Dictionary<CarbonFileResult, CarbonFile>();
            for (int i = 0; i < sources.Length; i++)
            {
                CarbonFileResult source = sources[i];

#if DEBUG
                /*if (!source.Contains(@"\BaseMetadataSectionSerializer."))
                {
                    continue;
                }*/
                /*if (!source.Contains(@"\Render.") && !source.Contains(@"\RenderEnderman.") && !source.Contains(@"\RendererLivingEntity.") && !source.Contains(@"\RenderLiving."))
                {
                    continue;
                }*/
#endif

                bool ignore = false;
                foreach (string ignorePattern in MCData.IgnoreList)
                {
                    if (source.Relative.Contains(ignorePattern))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                {
                    continue;
                }

                CarbonFile targetFile = new CarbonFile(source.Relative.FileName).ChangeExtension(CarbonCore.Utils.Constants.ExtensionCSharp);
                var subDirectory = new CarbonDirectory(string.Empty);
                foreach (string pattern in MCData.StructureMapping.Keys)
                {
                    if (source.Relative.StartsWith(pattern))
                    {
                        subDirectory = new CarbonDirectory(MCData.StructureMapping[pattern]);
                        break;
                    }
                }

                targetFile = subDirectory.ToFile(targetFile);
                files.Add(source, targetFile);
            }

            var translation = this.factory.Resolve<ITranslation>();
            translation.Translate(files);

            System.Diagnostics.Trace.TraceInformation(" Report: ");
            System.Diagnostics.Trace.TraceInformation("   Tokens: {0}", translation.TokenCount);
            System.Diagnostics.Trace.TraceInformation("   after Translation: {0}", translation.LinesTranslated);
        }
    }
}
