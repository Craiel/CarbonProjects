namespace AddonCompiler.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using CarbonCore.Utils.IO;

    public class AddonContent
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public AddonContent(CarbonFile file, CarbonDirectory rootDirectory)
        {
            this.File = file;

            this.RootDirectory = rootDirectory;

            this.SubContent = new List<AddonContent>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public int Count => this.SubContent.Sum(x => x.Count) + 1;

        public CarbonFile File { get; private set; }

        public CarbonDirectory RootDirectory { get; private set; }

        public IList<AddonContent> SubContent { get; private set; }
    }
}
