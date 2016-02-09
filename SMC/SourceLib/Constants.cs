namespace SMC.SourceLib
{
    using System.Linq;
    
    public static class Constants
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public const string SharpIndentation = "    ";

        public static string GetSharpIndentationString(int level)
        {
            return string.Concat(Enumerable.Repeat(SharpIndentation, level));
        }
    }
}
