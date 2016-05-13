namespace AddonCompiler.Contracts
{
    using AddonCompiler.Logic;

    public interface ICompiler
    {
        bool Run(CompileContext context);
    }
}
