namespace SMC.SourceCompilation.Logic
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using SMC.SourceCompilation.Contracts;

    public class CompilationCache : ICompilationState
    {
        private readonly List<string> warnings;
        private readonly List<string> errors;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CompilationCache()
        {
            this.BaseNameSpace = "undefined";

            this.PendingJobs = new ConcurrentQueue<CompilationJob>();
            this.ProcessedJobs = new ConcurrentQueue<CompilationJob>();

            this.ClassDictionary = new Dictionary<string, CompilationJob>();

            this.IncludeDetectionCache = new ConcurrentDictionary<string, string>();

            this.warnings = new List<string>();
            this.errors = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ConcurrentQueue<CompilationJob> PendingJobs { get; private set; }
        public ConcurrentQueue<CompilationJob> ProcessedJobs { get; private set; }

        public IDictionary<string, CompilationJob> ClassDictionary { get; private set; }

        public ConcurrentDictionary<string, string> IncludeDetectionCache { get; set; }

        public IReadOnlyCollection<string> Warnings
        {
            get
            {
                return this.warnings.AsReadOnly();
            }
        }

        public IReadOnlyCollection<string> Errors
        {
            get
            {
                return this.errors.AsReadOnly();
            }
        }

        public string BaseNameSpace { get; set; }

        public void AddWarning(string message, object[] args = null)
        {
            this.warnings.Add(args == null ? message : string.Format(message, args));
        }

        public void AddError(string message, object[] args = null)
        {
            this.errors.Add(args == null ? message : string.Format(message, args));
        }
    }
}
