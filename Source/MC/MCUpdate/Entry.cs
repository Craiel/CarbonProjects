namespace CarbonCore.Applications.MCUpdate
{
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.IoC;
    
    using MCUpdate.Contracts;
    using MCUpdate.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Main(string[] args)
        {
            var container = CarbonContainerAutofacBuilder.Build<MCUpdateModule>();
            container.Resolve<IMain>().Start();

            Profiler.TraceProfilerStatistics();
        }
    }
}
