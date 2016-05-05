namespace CarbonCore.Applications.CrystalBuild.CSharp.Contracts
{
    using System.Collections.Generic;
    
    using CarbonCore.CrystalBuild.Contracts;
    using CarbonCore.Utils.IO;

    public interface IBuildLogic
    {
        void BuildProjectFile(IList<CarbonFileResult> sources, CarbonFile target, IProcessingContext context);
    }
}
