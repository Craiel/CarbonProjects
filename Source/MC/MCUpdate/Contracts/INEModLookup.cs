namespace CarbonCore.Applications.MCUpdate.Contracts
{
    using System.Collections.Generic;

    using CarbonCore.Applications.MCUpdate.Data;
    using CarbonCore.Applications.MCUpdate.Logic.Enums;

    public interface INEModLookup
    {
        IList<NEModInfo> Mods { get; }

        void Initialize(MinecraftVersion version);

        void Reset();
        void Refresh();

        NEModInfo FindMod(string name);
    }
}
