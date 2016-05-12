namespace MetaFileUtility
{
    using CarbonCore.Utils;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using MetaFileUtility.Contracts;
    using MetaFileUtility.Data;

    public class Config : JsonConfig<MetaUtilityConfig>, IConfig
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override bool Load(CarbonFile file)
        {
            bool result = base.Load(file);

            // Set the project root
            this.Current.WorkingDirectory = file.GetDirectory();
            if (this.Current.WorkingDirectory == null || this.Current.WorkingDirectory.IsNull)
            {
                this.Current.WorkingDirectory = RuntimeInfo.WorkingDirectory;
            }

            return result;
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override MetaUtilityConfig GetDefault()
        {
            return new MetaUtilityConfig();
        }
    }
}
