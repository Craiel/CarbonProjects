namespace SMC.SourceCompilation.IoC
{
    using CarbonCore.ToolFramework.Console.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.Edge.IoC;
    using CarbonCore.Utils.IoC;

    using SMC.SourceCompilation.Contracts;
    using SMC.SourceCompilation.Logic;

    [DependsOnModule(typeof(UtilsEdgeModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(ToolFrameworkConsoleModule))]
    public class SourceCompilationModule : CarbonQuickModule
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public SourceCompilationModule()
        {
            this.For<IMain>().Use<Main>();

            this.For<ICompiler>().Use<Compiler>().Singleton();
            this.For<ICompilationState>().Use<CompilationCache>().Singleton();
        }
    }
}