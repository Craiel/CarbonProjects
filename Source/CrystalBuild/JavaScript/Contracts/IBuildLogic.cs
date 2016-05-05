namespace CarbonCore.Applications.CrystalBuild.JavaScript.Contracts
{
    using System.Collections.Generic;

    using CarbonCore.Applications.CrystalBuild.JavaScript.Logic;
    using CarbonCore.Utils.IO;

    public interface IBuildLogic
    {
        void Build(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context);

        void BuildTemplates(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context);

        void BuildData(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context);

        void BuildStyleSheets(IList<CarbonFileResult> sources, CarbonFile target, JavaScriptBuildingContext context);

        void BuildImages(IList<CarbonFileResult> sources, JavaScriptBuildingContext context);

        void CopyContents(IList<CarbonFileResult> sources, CarbonDirectory target);
    }
}
