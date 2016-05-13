namespace AddonCompiler.Logic
{
    using System;
    using System.Collections.Generic;
    
    using AddonCompiler.Data;
    
    using CarbonCore.Utils.IO;

    public class CompileContext
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public CarbonDirectory Source { get; private set; }

        public CarbonDirectory Target { get; private set; }

        public bool HasError { get; private set; }

        public string LastError { get; private set; }

        public Exception LastErrorException { get; private set; }

        public IDictionary<string, AddonEntry> ScannedEntries { get; private set; }

        public AddonEntry CurrentScanEntry { get; set; }

        public List<AddonEntry> EntriesOrderedByPriority { get; set; }

        public IList<CompileContent> FullContentList { get; set; }

        public void Initialize(CarbonDirectory source, CarbonDirectory target)
        {
            this.Source = source;
            this.Target = target;

            this.ScannedEntries = new Dictionary<string, AddonEntry>();
            this.FullContentList = new List<CompileContent>();
        }

        public void SetError(string reason, Exception exception)
        {
            this.HasError = true;
            this.LastError = reason;
            this.LastErrorException = exception;
        }

        public void FinalizeScannedEntry()
        {
            if (this.CurrentScanEntry == null)
            {
                throw new InvalidOperationException("Current Scan entry was null, could not finalize");
            }

            this.ScannedEntries.Add(this.CurrentScanEntry.Name, this.CurrentScanEntry);
            this.CurrentScanEntry = null;
        }
    }
}
