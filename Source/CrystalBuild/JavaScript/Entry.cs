namespace CarbonCore.Applications.CrystalBuild.JavaScript
{
    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts;
    using CarbonCore.Applications.CrystalBuild.JavaScript.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Main(string[] args)
        {
            var container = CarbonContainerAutofacBuilder.Build<CrystalBuildModule>();
            container.Resolve<IMain>().Start();

            Profiler.TraceProfilerStatistics();
        }
    }
}