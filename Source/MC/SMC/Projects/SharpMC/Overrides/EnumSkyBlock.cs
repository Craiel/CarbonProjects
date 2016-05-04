namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumSkyBlock : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumSkyBlock()
        {
            Sky = new EnumSkyBlock(SkyBlock.Sky);
            Block = new EnumSkyBlock(SkyBlock.Block);

            Values = new[] { Sky, Block };
        }

        internal EnumSkyBlock(SkyBlock type)
        {
            this.Enum = type;
        }

        internal enum SkyBlock
        {
            Sky = 15,
            Block = 0,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumSkyBlock Sky { get; private set; }
        public static EnumSkyBlock Block { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumSkyBlock[] Values { get; private set; }

        public int defaultLightValue;

        public override bool Equals(object obj)
        {
            var typed = obj as EnumSkyBlock;
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
        internal SkyBlock Enum { get; private set; }

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
