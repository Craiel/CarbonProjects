namespace SMC.SourceLib.PostProcessing
{
    using System.Collections.Generic;

    using SMC.SourceLib.Contracts;

    public class BaseProcessingData : IProcessingData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public BaseProcessingData()
        {
            this.Processed = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<string> Processed { get; private set; }

        /*public int? InitializerPosition { get; set; }
        public int Level { get; set; }

        public virtual void ResolveLineStacks()
        {
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected void ResolveLineStack<T>(Stack<T> stack)
            where T : IProcessLineData
        {
            while (stack.Count > 0)
            {
                if (stack.Peek().Level >= this.Level)
                {
                    stack.Pop();
                }
                else
                {
                    break;
                }
            }
        }*/
    }
}
