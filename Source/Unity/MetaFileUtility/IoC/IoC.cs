namespace MetaFileUtility.IoC
{
    using CarbonCore.ToolFramework.Console.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.Edge.IoC;
    using CarbonCore.Utils.IoC;

    using MetaFileUtility.Contracts;

    [DependsOnModule(typeof(UtilsEdgeModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(ToolFrameworkConsoleModule))]
    public class MetaFileUtilityModule : CarbonQuickModule
    {
        public MetaFileUtilityModule()
        {
            this.For<IMain>().Use<Main>();
            this.For<IConfig>().Use<Config>();
        }
    }
}