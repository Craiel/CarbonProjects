namespace CarbonCore.Applications.CrystalBuild.JavaScript
{
    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts;
    using CarbonCore.Utils;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using Constants = CarbonCore.Applications.CrystalBuild.JavaScript.Constants;

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
            return new BuildConfig
                       {
                           Name = JavaScript.Constants.DefaultProjectName,
                           Templates = new[] { new CarbonDirectoryFilter(JavaScript.Constants.DataTemplateDirectory, JavaScript.Constants.FilterTemplates) },
                           Sources = new[] { new CarbonDirectoryFilter(JavaScript.Constants.SourceDirectory, JavaScript.Constants.FilterSource) },
                           Data = new[] { new CarbonDirectoryFilter(JavaScript.Constants.DataDirectory, JavaScript.Constants.FilterData) },
                           StyleSheets = new[] {new CarbonDirectoryFilter(JavaScript.Constants.DataCssDirectory, JavaScript.Constants.FilterStyleSheet) },
                           Contents = new[] { new CarbonDirectoryFilter(JavaScript.Constants.ContentDirectory, JavaScript.Constants.FilterContent) },
                           Images = new[] { new CarbonDirectoryFilter(JavaScript.Constants.DataImagesDirectory, JavaScript.Constants.FilterImages) },
                           SourceTarget = JavaScript.Constants.OutputDirectory.ToFile(JavaScript.Constants.DefaultProjectTarget),
                           TemplateTarget = JavaScript.Constants.SourceDataGeneratedDirectory.ToFile(JavaScript.Constants.DefaultTemplateTarget),
                           DataTarget = JavaScript.Constants.SourceDataGeneratedDirectory.ToFile(JavaScript.Constants.DefaultDataTarget),
                           StyleSheetTarget = JavaScript.Constants.OutputDirectory.ToFile(JavaScript.Constants.DefaultStyleSheetTarget),
                           ImageRoot = JavaScript.Constants.ContentDirectory.ToDirectory(JavaScript.Constants.DefaultImageRoot),
                           ContentTarget = JavaScript.Constants.OutputDirectory
                       };
        }
    }
}
