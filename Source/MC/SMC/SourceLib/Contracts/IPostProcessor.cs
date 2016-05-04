namespace SMC.SourceLib.Contracts
{
    using System.Collections.Generic;

    using SMC.SourceLib.PostProcessing;

    public interface IPostProcessor
    {
        IList<string> Process(ProcessingInstruction instruction);
    }
}
