namespace AddonCompiler
{
    using AddonCompiler.Contracts;
    using AddonCompiler.Data;

    using CarbonCore.Utils;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    public class Config : JsonConfig<CompileConfig>, IConfig
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
        protected override CompileConfig GetDefault()
        {
            return new CompileConfig();
        }
    }
}
