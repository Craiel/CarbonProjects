namespace SMC.SourceTranslation.IoC
{
    using CarbonCore.GrammarParser.IoC;
    using CarbonCore.ToolFramework.Console.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.Edge.IoC;
    using CarbonCore.Utils.IoC;

    using SMC.SourceTranslation.Logic;

    using SourceTranslation.Contracts;

    [DependsOnModule(typeof(UtilsEdgeModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(GrammarParserModule))]
    [DependsOnModule(typeof(ToolFrameworkConsoleModule))]
    public class SourceTranslationModule : CarbonQuickModule
    {
        public SourceTranslationModule()
        {
            this.For<IMain>().Use<Main>();

            this.For<IConfig>().Use<Config>().Singleton();

            this.For<ITranslation>().Use<Translation>();
        }
    }
}