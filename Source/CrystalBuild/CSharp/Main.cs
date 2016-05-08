namespace CarbonCore.Applications.CrystalBuild.CSharp
{
    using CarbonCore.Applications.CrystalBuild.Contracts;
    using CarbonCore.Applications.CrystalBuild.CSharp.Contracts;
    using CarbonCore.CrystalBuild.Contracts;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Lua.Logic;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IConfig config;
        private readonly IBuildLogic logic;

        private readonly ICrystalBuildConfigurationRunTime configRuntime;

        private CarbonFile scriptFileName;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory)
            : base(factory)
        {
            this.config = factory.Resolve<IConfig>();
            this.logic = factory.Resolve<IBuildLogic>();
            this.configRuntime = factory.Resolve<ICrystalBuildConfigurationRunTime>();
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
            if (this.scriptFileName == null)
            {
                this.Arguments.PrintArgumentUse();
                return;
            }
            
            this.config.Load(new CarbonFile(Constants.BuildConfigFileName));

            if (!this.PrepareConfigurationRuntime())
            {
                return;
            }

            if (!this.RunBuildScript())
            {
                return;
            }

            if (!this.ExecuteBuild())
            {
                return;
            }
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("s", "script", x => this.scriptFileName = new CarbonFile(x));
            definition.RequireArgument = true;
            definition.Description = "The script to process";
            
            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private bool PrepareConfigurationRuntime()
        {
            return true;
        }

        private bool RunBuildScript()
        {
            LuaExecutionResult result = this.configRuntime.Execute(this.scriptFileName);
            return result.Success;
        }

        private bool ExecuteBuild()
        {
            return true;
            /*foreach (BuildProjectConfiguration buildProject in this.config.Current.BuildProjects)
            {
                // this.logic.BuildProjectFile();
            }*/
        }
    }
}
