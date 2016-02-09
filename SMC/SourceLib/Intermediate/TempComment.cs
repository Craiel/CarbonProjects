namespace SMC.SourceLib.Intermediate
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempComment : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempComment()
        {
            this.Type = TempDataType.Comment;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Content { get; set; }
    }
}
