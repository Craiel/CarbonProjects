namespace SMC.SourceLib.Intermediate
{
    using System.ComponentModel;
    
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempDataSegment : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempDataSegment()
        {
            this.Type = TempDataType.Function;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public int? Key { get; set; }

        [DefaultValue(null)]
        public string Contents { get; set; }

        [DefaultValue(null)]
        public TempDataBlock BlockContents { get; set; }
    }
}
