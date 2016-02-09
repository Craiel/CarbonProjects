namespace SMC.SourceLib.Sharp
{
    using System.Collections.Generic;
    using System.Text;

    public class SharpMethod
    {
        private readonly IList<string> contents;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SharpMethod()
        {
            this.contents = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void ToString(StringBuilder target, int level)
        {
            // Todo
        }
    }
}
