namespace AddonCompiler.Data
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class AddonEntry
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public AddonEntry(CarbonFile tocFile)
        {
            this.Name = tocFile.FileNameWithoutExtension;
            this.TocFile = tocFile;
            this.RootDirectory = tocFile.GetDirectory();

            this.Meta = new Dictionary<string, string>();

            this.Contents = new Dictionary<CarbonFile, AddonContent>();

            this.Resources = new List<CarbonFile>();

            this.Dependencies = new List<AddonEntry>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; private set; }

        public int Order { get; set; }

        public CarbonFile TocFile { get; private set; }

        public CarbonDirectory RootDirectory { get; private set; }

        public IDictionary<string, string> Meta { get; private set; }

        public IDictionary<CarbonFile, AddonContent> Contents { get; private set; }

        public IList<CarbonFile> Resources { get; private set; }

        public IList<AddonEntry> Dependencies { get; private set; }

        public void AddMeta(string key, string value)
        {
            string existingMeta;
            if (this.Meta.TryGetValue(key, out existingMeta))
            {
                if (existingMeta.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                Diagnostic.Warning("Overriding TOC Meta {0} with {1} (was {2})", key, value, existingMeta);
                this.Meta[key] = value;
                return;
            }

            this.Meta.Add(key, value);
        }

        public void AddContent(AddonContent content)
        {
            if (this.Contents.ContainsKey(content.File))
            {
                Diagnostic.Warning("File {0} was already in contents of addon {1}", content.File, this.Name);
                return;
            }

            this.Contents.Add(content.File, content);
        }

        public void AddResource(CarbonFile file)
        {
            if (this.Resources.Contains(file))
            {
                Diagnostic.Warning("File {0} was already in contents of addon {1}", file, this.Name);
                return;
            }

            this.Resources.Add(file);
        }

        public void AddDependency(AddonEntry dependencyEntry)
        {
            if (this.Dependencies.Contains(dependencyEntry))
            {
                Diagnostic.Warning("Addon {0} already contains dependency on {1}, ignoring", this.Name, dependencyEntry.Name);
                return;
            }

            this.Dependencies.Add(dependencyEntry);
        }

        public void ModifyOrder(int modifier)
        {
            this.Order += modifier;
            foreach (AddonEntry dependency in this.Dependencies)
            {
                // Cascade the modifier through the dependencies
                dependency.ModifyOrder(modifier);
            }
        }
    }
}
