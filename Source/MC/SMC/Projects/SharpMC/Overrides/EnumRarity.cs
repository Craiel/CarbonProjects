namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumRarity : IJavaEnum
    {
        public int rarityColor;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumRarity()
        {
            common = new EnumRarity(RarityEnum.Common, 15, "Common");
            uncommon = new EnumRarity(RarityEnum.Uncommon, 14, "Uncommon");
            rare = new EnumRarity(RarityEnum.Rare, 11, "Rare");
            epic = new EnumRarity(RarityEnum.Epic, 13, "Epic");

            Values = new[]
                         {
                             common, uncommon, rare, epic
                         };
        }

        internal EnumRarity(RarityEnum type, int rarityColor, string name)
        {
            this.Enum = type;
        }

        internal enum RarityEnum
        {
            Common,
            Uncommon,
            Rare,
            Epic
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumRarity common { get; private set; }
        public static EnumRarity uncommon { get; private set; }
        public static EnumRarity rare{ get; private set; }
        public static EnumRarity epic { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumRarity[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumRarity;
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
        internal RarityEnum Enum { get; private set; }

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
