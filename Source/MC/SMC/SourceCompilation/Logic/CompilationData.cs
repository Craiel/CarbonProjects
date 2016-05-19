namespace SMC.SourceCompilation.Logic
{
    using System.Collections.Generic;
    using SMC.SourceCompilation.Contracts;
    using SMC.SourceLib.Intermediate;
    using SMC.SourceLib.Sharp;

    public class CompilationData
    {
        public CompilationData()
        {
            // This will be filled during compilation, includes are processed last
            this.IncludeList = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ICompilationState State { get; set; }

        public TempFileFull SourceContent { get; set; }
        public SharpFile TargetContent { get; set; }

        public IList<string> IncludeList { get; private set; }
    }
}
