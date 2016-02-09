namespace SMC.SourceCompilation.Contracts
{
    using CarbonCore.Utils.IO;

    public interface IProjectFile
    {
        void AddInclude(CarbonFile file);

        void Save(CarbonFile targetFile);
    }
}