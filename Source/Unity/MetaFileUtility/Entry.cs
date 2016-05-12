namespace MetaFileUtility
{
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.IoC;

    using MetaFileUtility.Contracts;
    using MetaFileUtility.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static int Main(string[] args)
        {
            var container = CarbonContainerAutofacBuilder.Build<MetaFileUtilityModule>();
            var main = container.Resolve<IMain>();
            main.Start();

            Profiler.TraceProfilerStatistics();

            return main.ExitCode;
        }
    }
}
