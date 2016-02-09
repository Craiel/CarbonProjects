namespace SMC.SourceCompilation.Contracts
{
    using CarbonCore.Utils.IO;

    using SMC.SourceCompilation.Logic;

    public interface ICompiler
    {
        string GetNameSpace(CarbonDirectory relativePath);

        bool Compile(ICompilationState state, CompilationJob job);
    }
}
