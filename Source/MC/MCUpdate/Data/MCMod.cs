namespace CarbonCore.Applications.MCUpdate.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using ICSharpCode.SharpZipLib.Zip;

    public class MCMod
    {
        private MCModInfo info;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MCMod(CarbonFile file)
        {
            this.File = file;
            
            try
            {
                this.ReloadModInfo();
            }
            catch (Exception e)
            {
                Diagnostic.Error("Failed to read Mod info for {0}: {1}", file, e);
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public CarbonFile File { get; }

        public string Id => this.info?.ModId;

        public string Name => this.info?.Name;

        public string Description => this.info?.Description;

        public string Version => this.info?.Version;

        public string MCVersion => this.info?.MCVersion;

        public string Url => this.info?.Url;

        public string UpdateUrl => this.info?.UpdateUrl;

        public IList<string> Authors => this.info?.AuthorList;

        public IList<string> Dependencies => this.info?.Dependencies;

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void ReloadModInfo()
        {
            if (this.File == null || !this.File.Exists)
            {
                return;
            }

            string modInfoContents = this.ReadModInfo();
            if (string.IsNullOrEmpty(modInfoContents))
            {
                Diagnostic.Error("Mod Info was not retrieved for {0}", this.File);
                return;
            }

            var infos = JsonExtensions.LoadFromData<MCModInfo[]>(modInfoContents);
            this.info = infos.FirstOrDefault();
        }

        private string ReadModInfo()
        {
            using (var stream = this.File.OpenRead())
            {
                using (var zip = new ZipFile(stream))
                {
                    int index = zip.FindEntry(Constants.MCModInfoFile, true);
                    if (index == -1)
                    {
                        Diagnostic.Error("Could not find {0} in {1}", Constants.MCModInfoFile, this.File);
                        return null;
                    }

                    using (var entryStream = zip.GetInputStream(index))
                    {
                        using (var reader = new StreamReader(entryStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /*"modid": "SolarFlux",
  "name": "Solar Flux",
  "description": "Solar Panels producing Redstone Flux",
  "version": "1.7.10-0.8b",
  "mcversion": "1.7.10",
  "url": "",
  "updateUrl": "",
  "authorList": ["Nauktis"],
  "logoFile": "",
  "screenshots": [],
  "dependencies": []*/
    }
}
