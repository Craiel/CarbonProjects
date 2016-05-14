namespace AddonCompiler.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    using AddonCompiler.Contracts;
    using AddonCompiler.Data;

    using CarbonCore.Utils;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    using Constants = AddonCompiler.Constants;

    public class Compiler : ICompiler
    {
        private static readonly Regex AddonInitParamRegex = new Regex(@"^(local\s.*?=.*?)(\.\.\.)(.*)$", RegexOptions.IgnoreCase);

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool Run(CompileContext context)
        {
            Diagnostic.Info(string.Empty);
            Diagnostic.Info("Starting compilation of {0} Addons", context.ScannedEntries.Count);

            ResolveAddonDependency(context);
            AssembleContent(context);
            PurgeDuplicateContent(context);
            WriteContentToTarget(context);

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private static void ResolveAddonDependency(CompileContext context)
        {
            foreach (AddonEntry entry in context.ScannedEntries.Values)
            {
                string dependencyValues;
                if(entry.Meta.TryGetValue(Constants.MetaDependencies, out dependencyValues))
                {
                    string[] dependencies = dependencyValues.Split(',');
                    foreach (string dependency in dependencies)
                    {
                        if (dependency.Contains(Constants.BlizzardAddonPrefix))
                        {
                            continue;
                        }

                        AddonEntry dependencyEntry;
                        if (context.ScannedEntries.TryGetValue(dependency.Trim(), out dependencyEntry))
                        {
                            entry.AddDependency(dependencyEntry);
                            dependencyEntry.ModifyOrder(1);
                        }
                        else
                        {
                            Diagnostic.Warning("Could not resolve dependency {0} of {1}", dependency, entry.Name);
                        }
                    }
                }
            }

            context.EntriesOrderedByPriority = context.ScannedEntries.Values.OrderByDescending(x => x.Order).ThenBy(x => x.Name).ToList();
        }

        private static void AssembleContent(CompileContext context)
        {
            context.Target.Create();

            foreach (AddonEntry entry in context.EntriesOrderedByPriority)
            {
                foreach (AddonContent content in entry.Contents.Values)
                {
                    AssembleContentRecursive(context, entry, content);
                }
            }

            Diagnostic.Info("Found {0} Content files to compile", context.FullContentList.Count);
        }

        private static void AssembleContentRecursive(CompileContext context, AddonEntry addon, AddonContent currentContent)
        {
            foreach (AddonContent subContent in currentContent.SubContent)
            {
                AssembleContentRecursive(context, addon, subContent);
            }


            CarbonFile absoluteFile = currentContent.RootDirectory.ToFile(currentContent.File);
            if (!absoluteFile.Exists)
            {
                Diagnostic.Warning("Content File {0} does not exist! ({1})", absoluteFile, currentContent.File);
                return;
            }

            var content = new CompileContent(addon, absoluteFile, absoluteFile.ToRelative<CarbonFile>(context.Source.ToDirectory(addon.Name)));
            using (var stream = absoluteFile.OpenRead())
            {
                byte[] md5 = HashUtils.GetMd5(stream);
                content.Md5 = HashUtils.Md5ToString(md5);
            }
            
            context.FullContentList.Add(content);
        }

        private static void PurgeDuplicateContent(CompileContext context)
        {
            int duplicateFileCount = 0;
            IDictionary<string, CompileContent> duplicateCheck = new Dictionary<string, CompileContent>();
            foreach (CompileContent content in new List<CompileContent>(context.FullContentList))
            {
                if (!content.AbsoluteFile.Extension.Equals(Constants.ExtensionLua))
                {
                    // This check only applies to .lua scripts
                    continue;
                }

                CompileContent existingContent;
                if (duplicateCheck.TryGetValue(content.Md5, out existingContent))
                {
                    duplicateFileCount++;
                    context.FullContentList.Remove(content);
                    continue;
                }

                duplicateCheck.Add(content.Md5, content);
            }

            Diagnostic.Info("Removed {0} Duplicate Content Files, {1} left", duplicateFileCount, context.FullContentList.Count);
        }

        private static void WriteContentToTarget(CompileContext context)
        {
            StringBuilder contentTocBuilder = new StringBuilder();
            contentTocBuilder.AppendLine("## Interface: 60200");
            contentTocBuilder.AppendLine("## Title: Built");
            contentTocBuilder.AppendLine("## Notes: Built");
            contentTocBuilder.AppendLine("## Author: AddonCompiler");
            contentTocBuilder.AppendLine("## Version: 6.0.001");

            int filesCopied = 0;
            int resourcesCopied = 0;
            foreach (CompileContent content in context.FullContentList)
            {
                CarbonFile targetRelativeFile = new CarbonDirectory(content.Addon.Name).ToFile(content.RelativeFile);
                CarbonFile targetAbsoluteFile = context.Target.ToFile(targetRelativeFile);

                if (content.AbsoluteFile.Extension == Constants.ExtensionXml)
                {
                    if (!WriteXmlContentToTarget(content.AbsoluteFile, targetAbsoluteFile))
                    {
                        continue;
                    }

                    filesCopied++;
                }
                else
                {
                    if (!WriteScriptContentToTarget(content, targetAbsoluteFile))
                    {
                        continue;
                    }

                    filesCopied++;
                }

                // Register the content in the TOC
                contentTocBuilder.AppendLine(targetRelativeFile.GetPath());
            }

            foreach (AddonEntry addon in context.EntriesOrderedByPriority)
            {
                foreach (CarbonFile resource in addon.Resources)
                {
                    CarbonFile targetAbsoluteResourceFile = context.Target.ToDirectory(addon.Name).ToFile(resource);
                    CarbonFile sourceAbsoluteResourceFile = addon.RootDirectory.ToFile(resource);
                    sourceAbsoluteResourceFile.CopyTo(targetAbsoluteResourceFile, true);
                    resourcesCopied++;
                }
            }

            CarbonFile builtTocFile = context.Target.ToFile("Built.toc");
            builtTocFile.WriteAsString(contentTocBuilder.ToString());

            Diagnostic.Info("Finished Compile with {0} content files and {1} resources", filesCopied, resourcesCopied);
        }

        private static bool WriteScriptContentToTarget(CompileContent content, CarbonFile target)
        {
            IList<string> contents = new List<string>();
            using (var stream = content.AbsoluteFile.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        contents.Add(line);
                    }
                }
            }

            for(int i = 0; i < contents.Count; i++)
            {
                string line = contents[i];
                string trimmedLine = line.Trim().TrimStart('\t');
                if (trimmedLine.StartsWith("--"))
                {
                    continue;
                }

                Match match = AddonInitParamRegex.Match(line);
                if (match.Success)
                {
                    string replacementLine = String.Concat(match.Groups[1].Value, $"GetAddonGlobalArgs(\"{content.Addon.Name}\")", match.Groups[3].Value);
                    contents[i] = replacementLine;

                    Diagnostic.Warning("Adjusting lua parameters: {0}, line {1}\n{2}\n{3}", content.AbsoluteFile, i, line, replacementLine);
                }
            }

            target.WriteAsString(string.Join(Environment.NewLine, contents));

            return true;
        }

        private static bool WriteXmlContentToTarget(CarbonFile source, CarbonFile target)
        {
            bool isEmpty = true;
            XmlDocument document = AddonXmlUtils.ReadAddonXml(source);
            XmlNode root = document.DocumentElement;
            IList<XmlNode> deleteList = new List<XmlNode>();
            foreach (XmlNode childNode in root.ChildNodes)
            {
                if (childNode.Name.Equals(Constants.XmlNodeScript, StringComparison.OrdinalIgnoreCase) || childNode.Name.Equals(Constants.XmlNodeInclude, StringComparison.OrdinalIgnoreCase) || childNode.NodeType == XmlNodeType.Comment)
                {
                    deleteList.Add(childNode);
                    continue;
                }

                isEmpty = false;
            }
            
            if (isEmpty)
            {
                return false;
            }

            foreach (XmlNode node in deleteList)
            {
                root.RemoveChild(node);
            }

            target.GetDirectory().Create();
            document.Save(target.GetPath());
            return true;
        }
    }
}
