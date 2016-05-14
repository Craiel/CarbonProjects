namespace AddonCompiler.Logic
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using AddonCompiler.Contracts;
    using AddonCompiler.Data;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class AddonScanner : IAddonScanner
    {
        private static readonly Regex TocMetaRegex = new Regex(@"\s*##\s*(.*?):\s*(.*)", RegexOptions.Compiled);

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool Run(CompileContext context)
        {
            ScanSource(context);

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private static void ScanSource(CompileContext context)
        {
            context.ScannedEntries.Clear();

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
                    AddonEntry entry = ReadToc(context, tocResult.Absolute);

                    // Now scan for resources
                    CarbonFileResult[] fileResults = candidate.Absolute.GetFiles(options: SearchOption.AllDirectories);
                    foreach (CarbonFileResult fileResult in fileResults)
                    {
                        if (Constants.ResourceExtensions.Contains(fileResult.Absolute.Extension.ToLowerInvariant()))
                        {
                            entry.AddResource(fileResult.Relative);
                        }
                    }

                    continue;
                }
            }
        }

        private static AddonEntry ReadToc(CompileContext context, CarbonFile file)
        {
            var entry = new AddonEntry(file);
            context.CurrentScanEntry = entry;
            using (var stream = file.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        ParseTocLine(context, line);
                    }
                }
            }

            context.FinalizeScannedEntry();
            return entry;
        }

        private static void ParseTocLine(CompileContext context, string line)
        {
            string trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine))
            {
                // skip emptry lines
                return;
            }

            Match match = TocMetaRegex.Match(line);
            if (match.Success)
            {
                string key = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();
                context.CurrentScanEntry.AddMeta(key, value);
                return;
            }

            if (trimmedLine.StartsWith("#"))
            {
                // Ignore comments
                return;
            }

            if (trimmedLine.EndsWith(Constants.ExtensionLua))
            {
                context.CurrentScanEntry.AddContent(new AddonContent(new CarbonFile(trimmedLine), context.CurrentScanEntry.RootDirectory));
                return;
            }

            if (trimmedLine.EndsWith(Constants.ExtensionXml))
            {
                AddonContent xmlContent = ReadXMLContent(new CarbonFile(line), context.CurrentScanEntry.RootDirectory);
                if (xmlContent != null)
                {
                    context.CurrentScanEntry.AddContent(xmlContent);
                }

                return;
            }

            Diagnostic.Warning("Unhandled Toc Entry: {0}", trimmedLine);
        }

        private static AddonContent ReadXMLContent(CarbonFile file, CarbonDirectory rootDirectory)
        {
            CarbonFile absoluteFile = rootDirectory.ToFile(file);
            if (!absoluteFile.Exists)
            {
                Diagnostic.Warning("Could not find content file {0}", absoluteFile);
                return null;
            }

            try
            {
                var result = new AddonContent(file, rootDirectory);

                XmlDocument document = AddonXmlUtils.ReadAddonXml(absoluteFile);

                XmlNode root = document.DocumentElement;
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name.Equals(Constants.XmlNodeScript, StringComparison.OrdinalIgnoreCase))
                    {
                        string nestedContent = node.Attributes[Constants.XmlScriptFileAttribute].Value;
                        CarbonDirectory nestedRoot = file.GetDirectory() == null
                                                             ? rootDirectory
                                                             : rootDirectory.ToDirectory(file.GetDirectory());
                        result.SubContent.Add(new AddonContent(new CarbonFile(nestedContent), nestedRoot));
                        continue;
                    }

                    if (node.Name.Equals(Constants.XmlNodeInclude, StringComparison.OrdinalIgnoreCase))
                    {
                        string nestedContent = node.Attributes[Constants.XmlIncludeFileAttribute].Value;
                        CarbonFile nestedContentFile = new CarbonFile(nestedContent.Trim());
                        if (nestedContentFile.Extension == Constants.ExtensionLua)
                        {
                            CarbonDirectory nestedRoot = file.GetDirectory() == null
                                                             ? rootDirectory
                                                             : rootDirectory.ToDirectory(file.GetDirectory());
                            result.SubContent.Add(new AddonContent(nestedContentFile, nestedRoot));
                            continue;
                        }

                        AddonContent nestedEntry = ReadXMLContent(
                            new CarbonFile(nestedContent),
                            absoluteFile.GetDirectory());
                        if (nestedEntry != null)
                        {
                            result.SubContent.Add(nestedEntry);
                        }
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Diagnostic.Error("Could not read content XML {0}\n{1}", file, e);
                return null;
            }
        }
    }
}
