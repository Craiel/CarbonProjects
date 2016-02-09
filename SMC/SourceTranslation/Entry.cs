namespace SMC.SourceTranslation
{
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.IoC;

    using SourceTranslation.Contracts;
    using SourceTranslation.IoC;

    public static class Entry
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Main(string[] args)
        {
            var container = CarbonContainerAutofacBuilder.Build<SourceTranslationModule>();
            container.Resolve<IMain>().Start();

            Profiler.TraceProfilerStatistics();
        }
    }
}
