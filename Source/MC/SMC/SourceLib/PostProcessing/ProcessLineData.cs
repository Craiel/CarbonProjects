﻿namespace SMC.SourceLib.PostProcessing
{
    using SMC.SourceLib.Contracts;

    public abstract class ProcessLineData<T> : IProcessLineData
        where T : class, ILine
    {
        private readonly T lineData;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected ProcessLineData(string name, T line)
        {
            this.Name = name;
            this.lineData = line;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; set; }

        public TX GetLineData<TX>()
            where TX : class, ILine
        {
            return this.lineData as TX;
        }
    }
}
