namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempFunction : TempClassContent
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempFunction()
        {
            this.Type = TempDataType.Function;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public TempDataBlock Arguments { get; set; }

        [DefaultValue(null)]
        public TempDataBlock Contents { get; set; }

        [DefaultValue(null)]
        public List<TempFunctionAttribute> FunctionAttributes { get; set; }

        public void AddFunctionAttribute(TempFunctionAttribute value)
        {
            if (this.FunctionAttributes == null)
            {
                this.FunctionAttributes = new List<TempFunctionAttribute>();
            }

            this.FunctionAttributes.Add(value);
        }
    }
}
