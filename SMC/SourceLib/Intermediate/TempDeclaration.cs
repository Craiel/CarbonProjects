namespace SMC.SourceLib.Intermediate
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempDeclaration : TempClassContent
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempDeclaration()
        {
            this.Type = TempDataType.Declaration;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(false)]
        public bool IsFunction { get; set; }

        [DefaultValue(null)]
        public TempDataBlock Arguments { get; set; }

        [DefaultValue(null)]
        public TempDataBlock Contents { get; set; }
    }
}
