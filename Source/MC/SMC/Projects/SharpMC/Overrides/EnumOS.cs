namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumOS : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumOS()
        {
            LINUX = new EnumOS(OSEnum.Linux);
            SOLARIS = new EnumOS(OSEnum.Solaris);
            WINDOWS = new EnumOS(OSEnum.Windows);
            MACOS = new EnumOS(OSEnum.MacOS);
            UNKNOWN = new EnumOS(OSEnum.Unknown);

            Values = new[]
                         {
                             LINUX, SOLARIS, WINDOWS, MACOS, UNKNOWN
                         };
        }

        internal EnumOS(OSEnum type)
        {
            this.Enum = type;
        }

        internal enum OSEnum
        {
            Unknown,
            Linux,
            Solaris,
            Windows,
            MacOS,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumOS LINUX { get; private set; }
        public static EnumOS SOLARIS { get; private set; }
        public static EnumOS WINDOWS { get; private set; }
        public static EnumOS MACOS { get; private set; }
        public static EnumOS UNKNOWN { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumOS[] Values { get; private set; }
        public static int[] doorEnum { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumOS;
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
        internal OSEnum Enum { get; private set; }

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
