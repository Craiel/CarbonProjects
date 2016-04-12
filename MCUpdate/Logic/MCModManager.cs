namespace CarbonCore.Applications.MCUpdate.Logic
{
    using System.Collections.Generic;
    using System.IO;

    using CarbonCore.Applications.MCUpdate.Contracts;
    using CarbonCore.Applications.MCUpdate.Data;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class MCModManager : IMCModManager
    {
        private readonly IDictionary<string, MCMod> nameLookup;

        private CarbonDirectory root;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MCModManager()
        {
            this.Mods = new List<MCMod>();

            this.nameLookup = new Dictionary<string, MCMod>();
        }


        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<MCMod> Mods { get; }

        public void Initialize(CarbonDirectory root)
        {
            this.root = root;
            this.Refresh();
        }

        public void Reset()
        {
            this.root = null;
            this.Mods.Clear();
        }

        public void Refresh()
        {
            this.nameLookup.Clear();
            this.Mods.Clear();

            if (this.root == null || !this.root.Exists)
            {
                Diagnostic.Warning("Mod Manager is not initialized or the directory is invalid, skipping Refresh");
                return;
            }

            CarbonFileResult[] jarResults = this.root.GetFiles("*.jar", SearchOption.AllDirectories);
            foreach (CarbonFileResult result in jarResults)
            {
                var mod = new MCMod(result.Absolute);
                if (string.IsNullOrEmpty(mod.Id))
                {
                    Diagnostic.Warning("Skipping Unidentified mod: {0}", result.Absolute);
                    continue;
                }

                MCMod existingMod;
                if (this.nameLookup.TryGetValue(mod.Id.ToLowerInvariant(), out existingMod))
                {
                    Diagnostic.Error("Duplicate Mod Detected: {0}({1}) - {2}({3}", mod.Id, mod.Version, existingMod.Id, existingMod.Version);
                    continue;
                }

                this.Mods.Add(mod);
                this.nameLookup.Add(mod.Id.ToLowerInvariant(), mod);
            }
        }

        public MCMod FindMod(string name)
        {
            MCMod existingMod;
            if (this.nameLookup.TryGetValue(name.ToLowerInvariant(), out existingMod))
            {
                return existingMod;
            }

            return null;
        }
    }
}
