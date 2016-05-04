namespace SMC.SourceTranslation.Contracts
{
    using System.Collections.Generic;
    
    using CarbonCore.Utils.IO;

    public interface ITranslation
    {
        long TokenCount { get; set; }
        long LinesTranslated { get; set; }

        void Translate(IDictionary<CarbonFileResult, CarbonFile> files);
    }
}
