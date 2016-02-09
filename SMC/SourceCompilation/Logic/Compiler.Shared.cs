namespace SMC.SourceCompilation.Logic
{
    using System.Collections.Generic;
    using SMC.SourceLib.Intermediate;

    public partial class Compiler
    {
        private IList<TempAttribute> GetAttributes(IEnumerable<int> attributes)
        {
            IList<TempAttribute> results = new List<TempAttribute>();
            if (attributes == null)
            {
                return results;
            }

            foreach (int i in attributes)
            {
                results.Add((TempAttribute)i);
            }

            return results;
        }
    }
}
