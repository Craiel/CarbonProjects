namespace CarbonCore.Applications.CrystalBuild.CSharp.Data
{
    using System.Collections.Generic;

    using CarbonCore.Utils.IO;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class BuildConfig
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public BuildConfig()
        {
            this.ProjectGeneration = new List<ProjectGenerationConfig>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [JsonIgnore]
        public CarbonDirectory ProjectRoot { get; set; }

        public IList<ProjectGenerationConfig> ProjectGeneration { get; set; }
    }
}
