namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;

    using SharpMC.Source;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    internal sealed class EnumEnchantmentType : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumEnchantmentType()
        {
            all = new EnumEnchantmentType(EnchantmentType.All);
            armor = new EnumEnchantmentType(EnchantmentType.Armor);
            armor_feet = new EnumEnchantmentType(EnchantmentType.ArmorFeet);
            armor_head = new EnumEnchantmentType(EnchantmentType.ArmorHead);
            armor_legs = new EnumEnchantmentType(EnchantmentType.ArmorLegs);
            armor_torso = new EnumEnchantmentType(EnchantmentType.ArmorTorso);
            digger = new EnumEnchantmentType(EnchantmentType.Digger);
            weapon = new EnumEnchantmentType(EnchantmentType.Weapon);
            bow = new EnumEnchantmentType(EnchantmentType.Bow);

            Values = new[] { all, armor, armor_feet, armor_head, armor_legs, armor_torso, digger, weapon, bow };
        }

        internal EnumEnchantmentType(EnchantmentType type)
        {
            this.Enum = type;
        }

        internal enum EnchantmentType
        {
            All,
            Armor,
            ArmorFeet,
            ArmorLegs,
            ArmorTorso,
            ArmorHead,
            Weapon,
            Digger,
            Bow
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumEnchantmentType all { get; private set; }
        public static EnumEnchantmentType armor { get; private set; }
        public static EnumEnchantmentType armor_feet { get; private set; }
        public static EnumEnchantmentType armor_legs { get; private set; }
        public static EnumEnchantmentType armor_torso { get; private set; }
        public static EnumEnchantmentType armor_head { get; private set; }
        public static EnumEnchantmentType weapon { get; private set; }
        public static EnumEnchantmentType digger { get; private set; }
        public static EnumEnchantmentType bow { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumEnchantmentType[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumEnchantmentType;
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

        public bool CanEnchantItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        // -------------------------------------------------------------------
        // Internal
        // -------------------------------------------------------------------
        internal EnchantmentType Enum { get; private set; }

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
