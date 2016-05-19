namespace SMC.SourceLib.PostProcessors
{
    using System.Collections.Generic;

    using SMC.SourceLib.Patching;
    using SMC.SourceLib.PostProcessing;

    public class PatcherData : BaseProcessingData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public PatcherData()
        {
            this.Patches = new List<Patch>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<Patch> Patches { get; private set; }
    }
}
