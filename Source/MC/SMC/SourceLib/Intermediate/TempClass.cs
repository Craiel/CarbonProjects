namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public enum TempClassType
    {
        Unknown,
        Class,
        Interface,
        Enum
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class TempClass : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempClass()
        {
            this.Type = TempDataType.Class;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public List<int> Attributes { get; set; }

        [DefaultValue(null)]
        public string Name { get; set; }

        [DefaultValue(null)]
        public List<string> Extends { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TempClassType ClassType { get; set; }

        [DefaultValue(null)]
        public List<string> Inherits { get; set; }

        [DefaultValue(null)]
        public List<TempFunction> Functions { get; set; }

        [DefaultValue(null)]
        public List<TempClass> Classes { get; set; }
        
        [DefaultValue(null)]
        public List<TempDeclaration> Declarations { get; set; }

        public void AddAttribute(int key)
        {
            if (this.Attributes == null)
            {
                this.Attributes = new List<int>();
            }

            this.Attributes.Add(key);
        }

        public void AddInherit(string value)
        {
            if (this.Inherits == null)
            {
                this.Inherits = new List<string>();
            }

            this.Inherits.Add(value);
        }

        public void AddExtend(string value)
        {
            if (this.Extends == null)
            {
                this.Extends = new List<string>();
            }

            this.Extends.Add(value);
        }

        public void AddFunction(TempFunction value)
        {
            if (this.Functions == null)
            {
                this.Functions = new List<TempFunction>();
            }

            this.Functions.Add(value);
        }

        public void AddClass(TempClass value)
        {
            if (this.Classes == null)
            {
                this.Classes = new List<TempClass>();
            }

            this.Classes.Add(value);
        }

        public void AddDeclaration(TempDeclaration value)
        {
            if (this.Declarations == null)
            {
                this.Declarations = new List<TempDeclaration>();
            }

            this.Declarations.Add(value);
        }
    }
}
