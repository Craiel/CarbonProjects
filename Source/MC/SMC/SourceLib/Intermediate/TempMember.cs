namespace SourceTranslation.New.Intermediate
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempMember : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempMember()
        {
            this.Attributes = new List<JavaTermKey>();

            this.Type = TempDataType.Member;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public List<JavaTermKey> Attributes { get; set; }

        public string MemberType { get; set; }

        public string Name { get; set; }
        
        public string Initialization { get; set; }
    }
}
