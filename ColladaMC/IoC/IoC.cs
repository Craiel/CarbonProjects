namespace ColladaMC.IoC
{
    using CarbonCore.GrammarParser.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.IoC;

    using ColladaMC.Contracts;

    [DependsOnModule(typeof(UtilsModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(GrammarParserModule))]
    public class ColladaMCModule : CarbonQuickModule
    {
        public ColladaMCModule()
        {
            this.For<IColladaMinecraft>().Use<ColladaMinecraft>();
        }
    }
}