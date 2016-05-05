namespace CarbonCore.Applications.CrystalBuild.CSharp.Logic
{
    using System.Collections.Generic;
    
    using CarbonCore.Applications.CrystalBuild.CSharp.Contracts;
    using CarbonCore.CrystalBuild.Contracts;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.IO;

    public class BuildLogic : IBuildLogic
    {
        private readonly IFactory factory;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public BuildLogic(IFactory factory)
        {
            this.factory = factory;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void BuildProjectFile(IList<CarbonFileResult> sources, CarbonFile target, IProcessingContext context)
        {
            Diagnostic.Info("Building {0} Sources into {2}", sources.Count, target);
        }
    }
}
