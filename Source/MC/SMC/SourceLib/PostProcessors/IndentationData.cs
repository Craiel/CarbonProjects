﻿namespace SMC.SourceLib.PostProcessors
{
    using System.Collections.Generic;

    using SMC.SourceLib.PostProcessing;

    public class IndentationData : BaseProcessingData
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public int Level { get; set; }

        public HashSet<char> IncreaseAt { get; set; }
        public HashSet<char> DecreaseAt { get; set; }

        public char IndentationChar { get; set; }

        public byte IndentationAmount { get; set; }
    }
}
