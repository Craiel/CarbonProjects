namespace SMC.SourceLib.Contracts
{
    using System.Collections.Generic;

    public interface IProcessingData
    {
        IList<string> Processed { get; }
    }
}
