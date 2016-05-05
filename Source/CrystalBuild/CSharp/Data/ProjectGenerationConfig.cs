namespace CarbonCore.Applications.CrystalBuild.CSharp.Data
{
    using System.Collections.Generic;

    using CarbonCore.Utils.IO;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class ProjectGenerationConfig
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public ProjectGenerationConfig()
        {
            this.Sources = new List<CarbonDirectoryFilter>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<CarbonDirectoryFilter> Sources { get; set; }

        public CarbonFile Template { get; set; }
    }
}
