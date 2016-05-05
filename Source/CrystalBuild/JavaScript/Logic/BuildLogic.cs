namespace CarbonCore.Applications.CrystalBuild.JavaScript.Logic
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts;
    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors;
    using CarbonCore.CrystalBuild.Contracts;
    using CarbonCore.CrystalBuild.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class BuildLogic : IBuildLogic
    {
        private readonly IFactory factory;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public BuildLogic(IFactory factory)
        {
            this.factory = factory;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void Build(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context)
        {
            if (context.ExportSourceAsModule)
            {
                // Todo
            }

            Diagnostic.Info("Building {0} {1} into {2}", sources.Count, "Sources", target);

            var processor = this.factory.Resolve<IJavaScriptProcessor>();
            Diagnostic.Assert(processor != null);

            processor.SetContext(context);
            foreach (CarbonFileResult file in sources)
            {
                Diagnostic.Info("  {0}", file.Absolute.FileName);
                processor.Process(file.Absolute);
            }

            CarbonDirectory targetDirectory = target.GetDirectory();
            if (!targetDirectory.IsNull && !targetDirectory.Exists)
            {
                targetDirectory.Create();
            }

            using (var stream = target.OpenCreate())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 4096, true))
                {
                    if (context.ExportSourceAsModule)
                    {
                        writer.Write("declare('{0}', function() {{\n", context.Name);
                    }

                    writer.Write(processor.GetData());

                    if (context.ExportSourceAsModule)
                    {
                        writer.Write("});");
                    }
                }
            }

            BuildingUtils.TraceProcessorResult(processor, "Building Sources");
        }

        public void BuildTemplates(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context)
        {
            this.DoBuildMultipleToOne<ITemplateProcessor>("Templates", sources, target, context);
        }

        public void BuildData(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context)
        {
            this.DoBuildMultipleToOne<IExcelProcessor>("Data", sources, target, context, useTempFileForProcessing: true);
        }

        public void BuildStyleSheets(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context)
        {
            this.DoBuildMultipleToOne<ICssProcessor>("Style-sheets", sources, target, context);
        }

        public void BuildImages(IList<CarbonFileResult> sources, JavaScriptBuildingContext context)
        {
            this.DoBuildMultiple<IImageProcessor>("Images", sources, context);
        }

        public void CopyContents(IList<CarbonFileResult> sources, CarbonDirectory target)
        {
            Diagnostic.Info("Copying {0} Content into {1}", sources.Count, target);

            foreach (CarbonFileResult source in sources)
            {
                source.Absolute.CopyTo(target.ToFile(source.Relative), true);
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoBuildMultipleToOne<T>(string buildName, IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context, bool useTempFileForProcessing = false)
            where T : IContentProcessor
        {
            Diagnostic.Info("Building {0} {1} into {2}", sources.Count, buildName, target);

            var processor = this.factory.Resolve(typeof(T)) as IContentProcessor;
            Diagnostic.Assert(processor != null);

            processor.SetContext(context);
            foreach (CarbonFileResult file in sources)
            {
                Diagnostic.Info("  {0}", file.Absolute.FileName);
                if (useTempFileForProcessing)
                {
                    CarbonFile tempFile = CarbonFile.GetTempFile();
                    tempFile.DeleteIfExists();

                    file.Absolute.CopyTo(tempFile);
                    processor.Process(tempFile);
                }
                else
                {
                    processor.Process(file.Absolute);
                }
            }

            CarbonDirectory targetDirectory = target.GetDirectory();
            if (!targetDirectory.IsNull && !targetDirectory.Exists)
            {
                targetDirectory.Create();
            }

            using (var stream = target.OpenCreate())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 4096, true))
                {
                    writer.Write(processor.GetData());
                }
            }

            BuildingUtils.TraceProcessorResult(processor, $"Building {buildName}");
        }

        private void DoBuildMultiple<T>(string buildName, IList<CarbonFileResult> sources, JavaScriptBuildingContext context)
            where T : IContentProcessor
        {
            Diagnostic.Info("Building {0} files for {1}", sources.Count, buildName);

            var processor = this.factory.Resolve(typeof(T)) as IContentProcessor;
            Diagnostic.Assert(processor != null);

            processor.SetContext(context);
            foreach (CarbonFileResult file in sources)
            {
                Diagnostic.Info("  {0}", file.Absolute.FileName);
                processor.Process(file.Absolute);
            }
            
            BuildingUtils.TraceProcessorResult(processor, $"Building {buildName}");
        }
    }
}
