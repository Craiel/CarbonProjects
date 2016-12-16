namespace CarbonCore.Applications.CrystalBuild.JavaScript.Logic
{
    using CarbonCore.CrystalBuild.Logic;

    public class JavaScriptBuildingContext : BuildingContext<JavaScriptBuildingCache>
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public JavaScriptBuildingContext(JavaScriptBuildingCache cache, BuildTargetPlatform targetPlatform)
            : base(cache)
        {
            this.TargetPlatform = targetPlatform;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool IsDebug { get; set; }

        public bool KeepComments { get; set; }

        public bool ExportSourceAsModule { get; set; }
        
        public BuildTargetPlatform TargetPlatform { get; private set; }
    }
}
