namespace AddonCompiler
{
    using AddonCompiler.Contracts;
    using AddonCompiler.Logic;

    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IFactory factory;
        private readonly IConfig config;

        private CarbonDirectory sourceDirectory;
        private CarbonDirectory targetDirectory;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory) : base(factory)
        {
            this.factory = factory;

            this.config = factory.Resolve<IConfig>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "AddonCompiler";

        protected override void StartFinished()
        {
            this.Compile();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("s", "source", x => this.sourceDirectory = new CarbonDirectory(x));
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "The source directory to compile";

            definition = this.Arguments.Define("t", "target", x => this.targetDirectory = new CarbonDirectory(x));
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "The target directory";

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void Compile()
        {
            if (this.sourceDirectory == null || this.targetDirectory == null)
            {
                this.Arguments.PrintArgumentUse();
                return;
            }

            if (!this.sourceDirectory.Exists)
            {
                Diagnostic.Error("Source directory does not exist: {0}", this.sourceDirectory);
                return;
            }

            this.config.Load(new CarbonFile(Constants.ConfigFileName));

            var context = new CompileContext();
            context.Initialize(this.sourceDirectory, this.targetDirectory);

            var scanner = this.factory.Resolve<IAddonScanner>();
            if (!scanner.Run(context) || context.HasError)
            {
                Diagnostic.Error("Scanning Addons failed: {0}\n{1}", context.LastError, context.LastErrorException?.ToString() ?? "N/A");
                return;
            }
            
            var compiler = this.factory.Resolve<ICompiler>();
            if (!compiler.Run(context) || context.HasError)
            {
                Diagnostic.Error("Compilation failed: {0}\n{1}", context.LastError, context.LastErrorException?.ToString() ?? "N/A");
            }
        }
        
    }
}
