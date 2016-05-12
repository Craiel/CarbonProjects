namespace MetaFileUtility.Data
{
    using CarbonCore.Utils.IO;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class MetaUtilityConfig
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MetaUtilityConfig()
        {
            this.ResetToDefault();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [JsonIgnore]
        public CarbonDirectory WorkingDirectory { get; set; }

        public bool ErrorOnMissingFileMetaFiles { get; set; }

        public bool ErrorOnMissingDirectoryMetaFiles { get; set; }

        public bool ErrorOnOrphanMetaFiles { get; set; }

        public bool ErrorOnP4Check { get; set; }

        public void ResetToDefault()
        {
            this.ErrorOnMissingFileMetaFiles = true;
            this.ErrorOnMissingDirectoryMetaFiles = true;
            this.ErrorOnOrphanMetaFiles = true;
        }
    }
}
