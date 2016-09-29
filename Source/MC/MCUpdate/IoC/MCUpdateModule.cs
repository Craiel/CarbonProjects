namespace CarbonCore.Applications.MCUpdate.IoC
{
    using CarbonCore.Applications.MCUpdate.Logic;
    using CarbonCore.ToolFramework.Console.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.Edge.IoC;
    using CarbonCore.Utils.IoC;

    using MCUpdate.Contracts;

    using NEModLookup = CarbonCore.Applications.MCUpdate.Logic.NEModLookup;

    [DependsOnModule(typeof(UtilsEdgeModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(ToolFrameworkConsoleModule))]
    public class MCUpdateModule : CarbonQuickModule
    {
        public MCUpdateModule()
        {
            this.For<IMain>().Use<Main>();

            this.For<IMCModManager>().Use<MCModManager>();
            this.For<Contracts.INEModLookup>().Use<NEModLookup>();
        }
    }
}