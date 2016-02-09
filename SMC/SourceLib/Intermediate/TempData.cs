namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    public abstract class TempData
    {
        private static int nextOrder;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected TempData()
        {
            this.Order = nextOrder++;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [JsonIgnore]
        public TempDataType Type { get; protected set; }

        public int Order { get; set; }

        [DefaultValue(null)]
        public List<TempUnknown> Unknowns { get; set; }

        [DefaultValue(null)]
        public List<TempComment> Comments { get; set; }

        public void AddUnknown(TempUnknown value)
        {
            if (this.Unknowns == null)
            {
                this.Unknowns = new List<TempUnknown>();
            }

            this.Unknowns.Add(value);
        }

        public void AddComment(TempComment value)
        {
            if (this.Comments == null)
            {
                this.Comments = new List<TempComment>();
            }

            this.Comments.Add(value);
        }
    }
}
