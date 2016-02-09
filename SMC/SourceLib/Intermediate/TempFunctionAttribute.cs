namespace SMC.SourceLib.Intermediate
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempFunctionAttribute : TempData
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Name { get; set; }

        [DefaultValue(null)]
        public TempDataBlock Arguments { get; set; }
    }
}
