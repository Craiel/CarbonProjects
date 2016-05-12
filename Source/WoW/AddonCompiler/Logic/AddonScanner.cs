namespace AddonCompiler.Logic
{
    using AddonCompiler.Contracts;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class AddonScanner : IAddonScanner
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool Run(ICompileContext context)
        {
            this.ScanSource(context);

            return true;
        }

        private void ScanSource(ICompileContext context)
        {
            CarbonDirectoryResult[] candidates = context.Source.GetDirectories();
            foreach (CarbonDirectoryResult candidate in candidates)
            {
                CarbonFileResult[] tocResults = candidate.Absolute.GetFiles("*" + Constants.ExtensionToc);
                if (tocResults == null || tocResults.Length <= 0)
                {
                    Diagnostic.Info("Skipping directory {0}, no TOC found", candidate.Absolute.DirectoryNameWithoutPath);
                    continue;
                }

                foreach (CarbonFileResult tocResult in tocResults)
                {
                    Diagnostic.Info("Scanning {0}", tocResult.Absolute.FileName);
                }
            }
        }
    }
}
