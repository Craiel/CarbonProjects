namespace SMC.SourceLib.Intermediate
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempUnknown : TempData
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Content { get; set; }
    }
}
