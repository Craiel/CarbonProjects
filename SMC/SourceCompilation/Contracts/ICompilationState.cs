namespace SMC.SourceCompilation.Contracts
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using SMC.SourceCompilation.Logic;

    public interface ICompilationState
    {
        ConcurrentQueue<CompilationJob> PendingJobs { get; }
        ConcurrentQueue<CompilationJob> ProcessedJobs { get; }
        
        IDictionary<string, CompilationJob> ClassDictionary { get; }

        ConcurrentDictionary<string, string> IncludeDetectionCache { get; }

        IReadOnlyCollection<string> Warnings { get; }
        IReadOnlyCollection<string> Errors { get; }

        string BaseNameSpace { get; set; }

        void AddWarning(string message, object[] args = null);
        void AddError(string message, object[] args = null);
    }
}
