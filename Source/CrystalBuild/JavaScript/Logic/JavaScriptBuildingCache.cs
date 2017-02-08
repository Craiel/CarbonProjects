namespace CarbonCore.Applications.CrystalBuild.JavaScript.Logic
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CarbonCore.CrystalBuild.Logic;

    public class JavaScriptBuildingCache : BuildingCache
    {
        private readonly IDictionary<string, string> images;
        private readonly IDictionary<string, long> imageUseCount;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public JavaScriptBuildingCache()
        {
            this.images = new Dictionary<string, string>();
            this.imageUseCount = new Dictionary<string, long>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IReadOnlyDictionary<string, string> Images => new ReadOnlyDictionary<string, string>(this.images);

        public IReadOnlyDictionary<string, long> ImageUseCount => new ReadOnlyDictionary<string, long>(this.imageUseCount);

        public void RegisterImage(string key, string value)
        {
            this.images.Add(key, value);
            this.imageUseCount.Add(key, 0);
        }

        public void RegisterImageUse(string key)
        {
            this.imageUseCount[key]++;
        }
    }
}
