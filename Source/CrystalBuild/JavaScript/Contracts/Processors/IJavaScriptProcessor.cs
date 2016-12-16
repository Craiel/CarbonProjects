namespace CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors
{
    using CarbonCore.CrystalBuild.Contracts;

    public interface IJavaScriptProcessor : IContentProcessor
    {
        bool KeepComments { get; set; }
    }
}
