namespace CarbonCore.Applications.CrystalBuild.CSharp.Data
{
    using System.Collections.Generic;

    using CarbonCore.CrystalBuild.Data;
    using CarbonCore.Utils.IO;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class CSharpProjectConfig
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CSharpProjectConfig()
        {
            this.BuildProjects = new List<BuildProjectConfiguration>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [JsonIgnore]
        public CarbonDirectory ProjectRoot { get; set; }

        public IList<BuildProjectConfiguration> BuildProjects { get; set; }
    }
}
