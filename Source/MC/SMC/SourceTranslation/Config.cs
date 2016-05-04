namespace SMC.SourceTranslation
{
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using SMC.SourceTranslation.Contracts;

    public class Config : JsonConfig<TranslationConfig>, IConfig
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override bool Load(CarbonFile file)
        {
            bool result = base.Load(file);

            // Set the project root
            this.Current.ProjectRoot = file.GetDirectory();

            return result;
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override TranslationConfig GetDefault()
        {
            // Constants.ConfigFileName
            return new TranslationConfig
            {
                SourceDirectory = new CarbonDirectory("<source>"),
                TargetDirectory = new CarbonDirectory("<target>")
            };
        }
    }
}
