namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempDataBlock : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempDataBlock()
        {
            this.Type = TempDataType.Function;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Arguments { get; set; }

        [DefaultValue(null)]
        public List<TempDataSegment> Contents { get; set; }

        public void AddContent(TempDataSegment value)
        {
            if (this.Contents == null)
            {
                this.Contents = new List<TempDataSegment>();
            }

            this.Contents.Add(value);
        }
    }
}
