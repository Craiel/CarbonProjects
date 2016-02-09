namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public abstract class TempClassContent : TempData
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public List<int> Attributes { get; set; }

        [DefaultValue(null)]
        public List<string> Throws { get; set; }

        [DefaultValue(null)]
        public string Name { get; set; }

        [DefaultValue(null)]
        public string ReturnType { get; set; }

        public void AddAttribute(int key)
        {
            if (this.Attributes == null)
            {
                this.Attributes = new List<int>();
            }

            this.Attributes.Add(key);
        }

        public void AddThrow(string value)
        {
            if (this.Throws == null)
            {
                this.Throws = new List<string>();
            }

            this.Throws.Add(value);
        }
    }
}
