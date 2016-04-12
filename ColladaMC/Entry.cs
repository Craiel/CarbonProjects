namespace ColladaMC
{
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IoC;

    using ColladaMC.Contracts;
    using ColladaMC.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Main(string[] args)
        {
            var container = CarbonContainerBuilder.BuildQuick<ColladaMCModule>();
            container.Resolve<IColladaMinecraft>().Start();

            Profiler.TraceProfilerStatistics();
        }
    }
}
