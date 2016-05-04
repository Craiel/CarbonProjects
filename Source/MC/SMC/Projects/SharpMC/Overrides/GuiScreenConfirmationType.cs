namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class GuiScreenConfirmationType : IJavaEnum
    {
        public string field_140072_d;

        public int field_140075_c;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static GuiScreenConfirmationType()
        {
            Warning = new GuiScreenConfirmationType(GuiScreenConfirmationTypeEnum.Warning, "Warning!", 16711680);
            Info = new GuiScreenConfirmationType(GuiScreenConfirmationTypeEnum.Info, "Info!", 8226750);

            Values = new[]
                         {
                             Warning, Info
                         };
        }

        internal GuiScreenConfirmationType(GuiScreenConfirmationTypeEnum type, string name, int unknown)
        {
            this.Enum = type;
        }

        internal enum GuiScreenConfirmationTypeEnum
        {
            Warning,
            Info,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static GuiScreenConfirmationType Warning { get; private set; }
        public static GuiScreenConfirmationType Info { get; private set; }
        // ReSharper restore InconsistentNaming

        public static GuiScreenConfirmationType[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as GuiScreenConfirmationType;
            if (typed == null)
            {
                return false;
            }

            return typed.Enum == this.Enum;
        }

        public override int GetHashCode()
        {
            return this.Enum.GetHashCode();
        }

        // -------------------------------------------------------------------
        // Internal
        // -------------------------------------------------------------------
        internal GuiScreenConfirmationTypeEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }
    }
}
