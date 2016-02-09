namespace SourceTranslation.New.Intermediate
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class TempFunctionContent : TempData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public TempFunctionContent()
        {
            this.Contents = new List<TempFunctionContent>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Content { get; set; }

        public List<TempFunctionContent> Contents { get; set; }
    }
}
