namespace AddonCompiler.Contracts
{
    public interface ICompiler
    {
        bool Run(ICompileContext context);
    }
}
