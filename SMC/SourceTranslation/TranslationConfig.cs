namespace SMC.SourceTranslation
{
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TranslationConfig
    {
        public TranslationConfig()
        {
            this.Verbose = true;
            this.ProcessParrallel = true;
            this.CompressIntermediate = false;

            this.Filter = "*.java";

            this.TargetSubDirectory = new CarbonDirectory("Source");
            this.IntermediateSubDirectory = new CarbonDirectory("tlobj");
            this.IntermediateProjectExtension = ".jtlproj";
        }

        [JsonIgnore]
        public CarbonDirectory ProjectRoot { get; set; }

        public string ProjectName { get; set; }

        public string ProjectTemplate { get; set; }

        public string Filter { get; set; }

        public string RootNameSpace { get; set; }

        [JsonConverter(typeof(JsonCarbonDirectoryConverter))]
        public CarbonDirectory TargetSubDirectory { get; set; }

        [JsonConverter(typeof(JsonCarbonDirectoryConverter))]
        public CarbonDirectory IntermediateSubDirectory { get; set; }
    
        public string IntermediateProjectExtension { get; set; }

        [JsonConverter(typeof(JsonCarbonDirectoryConverter))]
        public CarbonDirectory TargetDirectory { get; set; }

        [JsonConverter(typeof(JsonCarbonDirectoryConverter))]
        public CarbonDirectory SourceDirectory { get; set; }

        public bool CompressIntermediate { get; set; }

        public bool Verbose { get; set; }

        public bool ProcessParrallel { get; set; }
    }
}
