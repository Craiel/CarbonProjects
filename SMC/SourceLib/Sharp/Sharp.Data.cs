namespace SMC.SourceLib.Sharp
{
    using SMC.SourceLib.Intermediate;
    using System.Collections.Generic;

    public static class SharpData
    {
        public static readonly IDictionary<TempAttribute, string> AttributeDictionary = new Dictionary<TempAttribute, string>
        {
            { TempAttribute.Private, "private" },
            { TempAttribute.Public, "public" },
            { TempAttribute.Protected, "protected" },
            { TempAttribute.Abstract, "abstract" },
            { TempAttribute.Static, "static" },
        };
    }
}
