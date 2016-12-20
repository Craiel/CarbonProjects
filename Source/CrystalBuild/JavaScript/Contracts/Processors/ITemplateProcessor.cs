namespace CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors
{
    using CarbonCore.CrystalBuild.Contracts;

    public interface ITemplateProcessor : IContentProcessor
    {
        bool ExportAsModule { get; set; }
    }
}