namespace SMC.SourceLib.Sharp
{
    using System.Collections.Generic;
    using SMC.SourceLib.Intermediate;

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
