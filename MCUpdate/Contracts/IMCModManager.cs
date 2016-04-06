namespace CarbonCore.Applications.MCUpdate.Contracts
{
    using System.Collections.Generic;

    using CarbonCore.Applications.MCUpdate.Data;
    using CarbonCore.Utils.IO;

    public interface IMCModManager
    {
        IList<MCMod> Mods { get; }

        void Initialize(CarbonDirectory root);

        void Reset();
        void Refresh();

        MCMod FindMod(string name);
    }
}
