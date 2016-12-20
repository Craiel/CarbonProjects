namespace CarbonCore.Applications.CrystalBuild.JavaScript.Logic.Processors
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors;
    using CarbonCore.CrystalBuild.Logic;
    using CarbonCore.Utils.IO;

    public class TemplateProcessor : ContentProcessor, ITemplateProcessor
    {
        private const string DataPrefix = "modules.templates = {";
        private const string DataSuffix = "};";

        private const string DataPrefixModule = @"declare(""TemplateContent"", function() { return {";
        private const string DataSuffixModule = "}; });";
        
        private static readonly char[] StripFromTemplates = { '\n', '\r', '\t' };
        
        private readonly IList<string> templateSegments;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TemplateProcessor()
        {
            this.templateSegments = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool ExportAsModule { get; set; }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void DoProcess(CarbonFile source)
        {
            string content = source.ReadAsString();
            string[] segments = content.Split(StripFromTemplates, StringSplitOptions.RemoveEmptyEntries);
            content = string.Join(" ", segments);
            this.templateSegments.Add($"{source.FileNameWithoutExtension}: '{content}'");
        }

        protected override void PreprocessData()
        {
            if (this.ExportAsModule)
            {
                this.AppendLine(DataPrefixModule);
            }
            else
            {
                this.AppendLine(DataPrefix);
            }

            for (int i = 0; i < this.templateSegments.Count; i++)
            {
                this.AppendFormat("\t{0}", this.templateSegments[i]);
                if (i < this.templateSegments.Count - 1)
                {
                    this.Append(",");
                }

                this.AppendLine();
            }
        }

        protected override string PostProcessData(string data)
        {
            if (this.ExportAsModule)
            {
                return string.Concat(data, Environment.NewLine, DataSuffixModule);
            }
            else
            {
                return string.Concat(data, Environment.NewLine, DataSuffix);
            }
        }
    }
}
