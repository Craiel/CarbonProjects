namespace AddonCompiler.Data
{
    using CarbonCore.Utils.IO;

    public class CompileContent
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CompileContent(AddonEntry addon, CarbonFile absolute, CarbonFile relative)
        {
            this.Addon = addon;
            this.AbsoluteFile = absolute;
            this.RelativeFile = relative;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public AddonEntry Addon { get; private set; }

        public CarbonFile AbsoluteFile { get; private set; }

        public CarbonFile RelativeFile { get; private set; }

        public string Md5 { get; set; }
    }
}
