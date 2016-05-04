namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    
    using CarbonCore.Utils.IO;

    public class TempProjectFileEntry
    {
        [DefaultValue(false)]
        public bool IsCompressed { get; set; }

        [DefaultValue(TempClassType.Unknown)]
        public TempClassType Type { get; set; }

        [DefaultValue(null)]
        public CarbonFile File { get; set; }

        [DefaultValue(null)]
        public CarbonFile RelativeFile { get; set; }

        [DefaultValue(null)]
        public CarbonDirectory RelativePath { get; set; }

        [DefaultValue(null)]
        public IList<string> Classes { get; set; }

        public void AddClass(string name)
        {
            if (this.Classes == null)
            {
                this.Classes = new List<string>();
            }

            if (!this.Classes.Contains(name))
            {
                this.Classes.Add(name);
            }
        }
    }
}
