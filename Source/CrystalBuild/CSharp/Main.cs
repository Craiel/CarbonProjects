namespace CarbonCore.Applications.CrystalBuild.CSharp
{
    using CarbonCore.Applications.CrystalBuild.Contracts;
    using CarbonCore.Applications.CrystalBuild.CSharp.Contracts;
    using CarbonCore.CrystalBuild.Data;
    using CarbonCore.CrystalBuild.Logic.Enums;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IConfig config;
        private readonly IBuildLogic logic;

        private CarbonFile configFileName;

        private bool makeSampleConfig;

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
            
            this.config.Load(this.configFileName);

            if (this.makeSampleConfig)
            {
                this.GenerateSampleConfig();
                return;
            }

            this.DoBuildProject();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("p", "projectFile", x => this.configFileName = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Description = "The project file to compile";

            definition = this.Arguments.Define("makeSample", x => this.makeSampleConfig = true);
            definition.Description = "Create a sample configuration !!! Note: will overwrite the -p parameter config !!!";
            
            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoBuildProject()
        {
            foreach (BuildProjectConfiguration buildProject in this.config.Current.BuildProjects)
            {
                // this.logic.BuildProjectFile();
            }
        }

        private void GenerateSampleConfig()
        {
            this.config.Reset();

            var sampleConfig = new BuildProjectConfiguration();
            var debugConfig = new BuildConfiguration { Type = BuildTargetType.Library };
            debugConfig.Defines.Add("EXAMPLE_DEFINE");
            debugConfig.AssemblyName = "Test.Project";
            debugConfig.Name = "Debug";
            debugConfig.Id = "TEST|DEBUG";
            debugConfig.TargetFramework = "v4.5";

            sampleConfig.References.Add(DefaultBuildReferences.DefaultReferenceSystem);
            sampleConfig.References.Add(DefaultBuildReferences.DefaultReferenceSystemCore);
            sampleConfig.References.Add(DefaultBuildReferences.DefaultReferenceSystemData);
            sampleConfig.References.Add(DefaultBuildReferences.DefaultReferenceSystemXml);

            sampleConfig.BuildConfigurations.Add(debugConfig);
            sampleConfig.SourceMapping.Add(new CarbonDirectoryFilter(new CarbonDirectory("."), "*.cs"), null);
            this.config.Current.BuildProjects.Add(sampleConfig);
            this.config.Save(this.configFileName);
        }
    }
}
