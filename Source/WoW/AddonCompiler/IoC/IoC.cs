namespace AddonCompiler.IoC
{
    using AddonCompiler.Contracts;
    using AddonCompiler.Logic;

    using CarbonCore.ToolFramework.Console.IoC;
    using CarbonCore.Utils.Edge.CommandLine.IoC;
    using CarbonCore.Utils.Edge.IoC;
    using CarbonCore.Utils.IoC;

    [DependsOnModule(typeof(UtilsEdgeModule))]
    [DependsOnModule(typeof(UtilsCommandLineModule))]
    [DependsOnModule(typeof(ToolFrameworkConsoleModule))]
    public class AddonCompilerModule : CarbonQuickModule
    {
        public AddonCompilerModule()
        {
            this.For<IMain>().Use<Main>();
            this.For<IConfig>().Use<Config>();

            this.For<IAddonScanner>().Use<AddonScanner>().Singleton();
            this.For<ICompiler>().Use<Compiler>().Singleton();

            this.For<ICompileContext>().Use<CompileContext>();
        }
    }
}