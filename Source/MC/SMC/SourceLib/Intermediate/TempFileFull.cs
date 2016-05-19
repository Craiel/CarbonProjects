namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Newtonsoft.Json;

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
}
