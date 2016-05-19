namespace CarbonCore.Applications.MCUpdate.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using CarbonCore.Applications.MCUpdate.Data;
    using CarbonCore.Applications.MCUpdate.Logic.Enums;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Json;

    public class INEModLookup : Contracts.INEModLookup
    {
        private readonly IDictionary<string, NEModInfo> nameLookup;

        private readonly WebClient client;

        private MinecraftVersion version;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public INEModLookup()
        {
            this.Mods = new List<NEModInfo>();

            this.nameLookup = new Dictionary<string, NEModInfo>();

            this.client = new WebClient();
            this.client.Headers.Add("User-Agent: Other");
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<NEModInfo> Mods { get; }

        public void Initialize(MinecraftVersion version)
        {
            this.version = version;
            this.Refresh();
        }

        public void Reset()
        {
            this.version = MinecraftVersion.Unknown;
            this.Mods.Clear();
        }

        public void Refresh()
        {
            this.nameLookup.Clear();
            this.Mods.Clear();

            if (this.version == MinecraftVersion.Unknown)
            {
                Diagnostic.Warning("NE Mod Manager is not initialized, skipping Refresh");
                return;
            }

            string url;
            switch (this.version)
            {
                    case MinecraftVersion.V1_7_10:
                    {
                        url = Constants.ServerAddress + string.Format(Constants.ServerFilePattern, "1.7.10");
                        break;
                    }

                    case MinecraftVersion.V1_8_9:
                    {
                        url = Constants.ServerAddress + string.Format(Constants.ServerFilePattern, "1.8.9");
                        break;
                    }

                    case MinecraftVersion.V1_9:
                    {
                        url = Constants.ServerAddress + string.Format(Constants.ServerFilePattern, "1.9");
                        break;
                    }

                default:
                    {
                        Diagnostic.Error("Unsupported version in Mod Lookup: {0}", this.version);
                        return;
                    }
            }

            string data;
            try
            {
                data = this.client.DownloadString(url);
                if (string.IsNullOrEmpty(data))
                {
                    Diagnostic.Error("Download of mod info returned no data");
                    return;
                }
            }
            catch (Exception e)
            {
                Diagnostic.Error("Failed to download mod Info: {0}", e);
                return;
            }

            try
            {
                NEModInfo[] entries = JsonExtensions.LoadFromData<NEModInfo[]>(data);
                foreach (NEModInfo entry in entries)
                {
                    if (string.IsNullOrEmpty(entry.ModId))
                    {
                        continue;
                    }

                    if (entry.Version.ToLowerInvariant().Contains("dev-only"))
                    {
                        // Ignore dev only versions for our case
                        continue;
                    }

                    NEModInfo existingEntry;
                    if (this.nameLookup.TryGetValue(entry.ModId.ToLowerInvariant(), out existingEntry))
                    {
                        if (existingEntry.LastUpdated < entry.LastUpdated)
                        {
                            Diagnostic.Warning("Duplicate Mod Info Entry: {0}({1}) - {2}({3}), Replacing with new entry", entry.ModId, entry.Version, existingEntry.ModId, existingEntry.Version);
                            this.Mods.Remove(existingEntry);
                            this.nameLookup.Remove(existingEntry.ModId.ToLowerInvariant());
                        }
                        else
                        {
                            Diagnostic.Warning("Duplicate Mod Info Entry: {0}({1}) - {2}({3}), keeping old entry", entry.ModId, entry.Version, existingEntry.ModId, existingEntry.Version);
                            continue;
                        }
                    }

                    this.Mods.Add(entry);
                    this.nameLookup.Add(entry.ModId.ToLowerInvariant(), entry);
                }
            }
            catch (Exception e)
            {
                Diagnostic.Error("Failed to load mod info data: {0}", e);
            }
        }

        public NEModInfo FindMod(string name)
        {
            NEModInfo existingMod;
            if (this.nameLookup.TryGetValue(name.ToLowerInvariant(), out existingMod))
            {
                return existingMod;
            }

            return null;
        }
    }
}
