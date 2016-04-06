namespace CarbonCore.Applications.MCUpdate.Data
{
    using CarbonCore.Utils.Json;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class MCModInfo
    {
        public string ModId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        [JsonConverter(typeof(JsonStringConverter))]
        public string MCVersion { get; set; }

        public string Url { get; set; }

        public string UpdateUrl { get; set; }

        public string[] AuthorList { get; set; }

        public string LogoFile { get; set; }

        public string[] Screenshots { get; set; }

        public string[] Dependencies { get; set; }
    }
}
