namespace CarbonCore.Applications.CrystalBuild.CSharp
{
    using CarbonCore.Applications.CrystalBuild.Contracts;
    using CarbonCore.Applications.CrystalBuild.CSharp.Contracts;
    using CarbonCore.Applications.CrystalBuild.CSharp.Data;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.I18N;
    using CarbonCore.Utils.IO;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IConfig config;
        private readonly IBuildLogic logic;

        private CarbonFile configFileName;
        
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
            // Set the default locale to english
            Localization.CurrentCulture = LocaleConstants.LocaleEnglishUS;

            if (this.configFileName == null)
            {
                this.Arguments.PrintArgumentUse();
                return;
            }
            
            this.config.Load(this.configFileName);
            this.DoBuildProject();

            Localization.SaveDictionaries();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("p", "projectFile", x => this.configFileName = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Description = "The project file to compile";
            
            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoBuildProject()
        {
            foreach (ProjectGenerationConfig generationConfig in this.config.Current.ProjectGeneration)
            {
                // this.logic.BuildProjectFile();
            }
        }
    }
}
