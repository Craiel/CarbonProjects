namespace CarbonCore.Applications.CrystalBuild.CSharp
{
    using CarbonCore.Applications.CrystalBuild.CSharp.Contracts;
    using CarbonCore.Applications.CrystalBuild.CSharp.Data;
    using CarbonCore.Utils;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    public class Config : JsonConfig<BuildConfig>, IConfig
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override bool Load(CarbonFile file)
        {
            bool result = base.Load(file);

            // Set the project root
            this.Current.ProjectRoot = file.GetDirectory();
            if (this.Current.ProjectRoot == null || this.Current.ProjectRoot.IsNull)
            {
                this.Current.ProjectRoot = RuntimeInfo.WorkingDirectory;
            }

            return result;
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override BuildConfig GetDefault()
        {
            return new BuildConfig();
        }
    }
}
