namespace CarbonCore.Applications.MCUpdate.Data
{
    using Newtonsoft.Json;

    public class NEModInfo
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }

        public string[] Aliases { get; set; }

        public string Comment { get; set; }

        public string ModId { get; set; }

        public string Dev { get; set; }

        public string Author { get; set; }

        [JsonProperty("spAnser")]
        public string Sponsor { get; set; }

        public long LastUpdated { get; set; }

        public string PrevVersion { get; set; }

        public string[] Dependencies { get; set; }

        public string License { get; set; }

        public string Repo { get; set; }

        public string[] Tags { get; set; }

        [JsonProperty("added_at")]
        public long AddedAt { get; set; }
    }
}
