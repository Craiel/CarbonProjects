namespace AddonCompiler.Contracts
{
    using System;

    using CarbonCore.Utils.IO;

    public interface ICompileContext
    {
        CarbonDirectory Source { get; }
        CarbonDirectory Target { get; }

        bool HasError { get; }
        string LastError { get; }
        Exception LastErrorException { get; }

        void Initialize(CarbonDirectory source, CarbonDirectory target);

        void SetError(string reason, Exception exception);
    }
}
