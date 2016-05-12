namespace AddonCompiler
{
    using AddonCompiler.Contracts;
    using AddonCompiler.IoC;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Main(string[] args)
        {
            var container = CarbonContainerAutofacBuilder.Build<AddonCompilerModule>();
            container.Resolve<IMain>().Start();

            Profiler.TraceProfilerStatistics();
        }
    }
}
