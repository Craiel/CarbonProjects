namespace AddonCompiler.Logic
{
    using System;

    using AddonCompiler.Contracts;

    using CarbonCore.Utils.IO;

    public class CompileContext : ICompileContext
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public CarbonDirectory Source { get; private set; }

        public CarbonDirectory Target { get; private set; }

        public bool HasError { get; private set; }

        public string LastError { get; private set; }

        public Exception LastErrorException { get; private set; }

        public void Initialize(CarbonDirectory source, CarbonDirectory target)
        {
            this.Source = source;
            this.Target = target;
        }

        public void SetError(string reason, Exception exception)
        {
            this.HasError = true;
            this.LastError = reason;
            this.LastErrorException = exception;
        }
    }
}
