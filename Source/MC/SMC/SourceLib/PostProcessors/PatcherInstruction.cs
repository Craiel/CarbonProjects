namespace SMC.SourceLib.PostProcessors
{
    using System.Collections.Generic;

    using SMC.SourceLib.Patching;
    using SMC.SourceLib.PostProcessing;

    public class PatcherInstruction : ProcessingInstruction
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public PatcherInstruction(string fileName, IList<string> data, IList<Patch> patches)
            : base(fileName, data)
        {
            this.Patches = patches;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<Patch> Patches { get; private set; }
    }
}
