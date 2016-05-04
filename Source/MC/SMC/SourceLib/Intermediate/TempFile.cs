namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempFileFull : TempFileLight
    {
        [DefaultValue(null)]
        public List<TempClass> Classes { get; set; }

        public void AddClass(TempClass value)
        {
            if (this.Classes == null)
            {
                this.Classes = new List<TempClass>();
            }

            this.Classes.Add(value);
        }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class TempFileLight : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempFileLight()
        {
            this.Type = TempDataType.File;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Name { get; set; }

        [DefaultValue(null)]
        public string Namespace { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(TempFileStructureType.Unknown)]
        public TempFileStructureType StructureType { get; set; }

        [DefaultValue(null)]
        public List<string> Includes { get; set; }

        public void AddInclude(string value)
        {
            if (this.Includes == null)
            {
                this.Includes = new List<string>();
            }

            this.Includes.Add(value);
        }
    }
}
