namespace AddonCompiler.Contracts
{
    public interface IAddonScanner
    {
        bool Run(ICompileContext context);
    }
}
