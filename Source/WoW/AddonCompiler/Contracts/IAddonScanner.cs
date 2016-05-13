namespace AddonCompiler.Contracts
{
    using AddonCompiler.Logic;

    public interface IAddonScanner
    {
        bool Run(CompileContext context);
    }
}
