namespace AddonCompiler.Data
{
    using CarbonCore.Utils.IO;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class CompileConfig
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [JsonIgnore]
        public CarbonDirectory WorkingDirectory { get; set; }
    }
}
