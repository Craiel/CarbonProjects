namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumArmorMaterial : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumArmorMaterial()
        {
            CLOTH = new EnumArmorMaterial(ArmorMaterialEnum.Cloth, 5, new int[] { 1, 3, 2, 1 }, 15);
            CHAIN = new EnumArmorMaterial(ArmorMaterialEnum.Chain, 15, new int[] { 2, 5, 4, 1 }, 12);
            IRON = new EnumArmorMaterial(ArmorMaterialEnum.Iron, 15, new int[] { 2, 6, 5, 2 }, 9);
            GOLD = new EnumArmorMaterial(ArmorMaterialEnum.Gold, 7, new int[] { 2, 5, 3, 1 }, 25);
            DIAMOND = new EnumArmorMaterial(ArmorMaterialEnum.Diamond, 33, new int[] { 3, 8, 6, 3 }, 10);

            Values = new[]
                         {
                             CLOTH, CHAIN, IRON, GOLD, DIAMOND
                         };
        }

        internal EnumArmorMaterial(ArmorMaterialEnum type, int maxDamageFactor, int[] damageReductionAmount, int enchantability)
        {
            this.Enum = type;
        }

        internal enum ArmorMaterialEnum
        {
            Cloth,
            Chain,
            Iron,
            Gold,
            Diamond
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumArmorMaterial CLOTH { get; private set; }
        public static EnumArmorMaterial CHAIN { get; private set; }
        public static EnumArmorMaterial IRON { get; private set; }
        public static EnumArmorMaterial GOLD { get; private set; }
        public static EnumArmorMaterial DIAMOND { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumArmorMaterial[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumArmorMaterial;
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
        internal ArmorMaterialEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public int GetArmorCraftingMaterial()
        {
            throw new System.NotImplementedException();
        }

        public int GetDamageReductionAmount(int par4)
        {
            throw new System.NotImplementedException();
        }

        public int GetDurability(int par4)
        {
            throw new System.NotImplementedException();
        }

        public int GetEnchantability()
        {
            throw new System.NotImplementedException();
        }
    }
}
