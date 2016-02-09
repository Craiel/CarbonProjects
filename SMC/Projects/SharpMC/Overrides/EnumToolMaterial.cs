namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumToolMaterial : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumToolMaterial()
        {
            WOOD = new EnumToolMaterial(ToolMaterial.Wood, 0, 59, 2.0F, 0.0F, 15);
            STONE = new EnumToolMaterial(ToolMaterial.Stone, 1, 131, 4.0F, 1.0F, 5);
            IRON = new EnumToolMaterial(ToolMaterial.Iron, 2, 250, 6.0F, 2.0F, 14);
            EMERALD = new EnumToolMaterial(ToolMaterial.Emerald, 3, 1561, 8.0F, 3.0F, 10);
            GOLD = new EnumToolMaterial(ToolMaterial.Gold, 0, 32, 12.0F, 0.0F, 22);

            Values = new[]
                         {
                             WOOD, STONE, IRON, EMERALD, GOLD
                         };
        }

        internal EnumToolMaterial(ToolMaterial type, int harvestLevel, int maxUses, float efficiencyOnProperMaterial, float damageVsEntity, int enchantability)
        {
            this.Enum = type;
        }

        internal enum ToolMaterial
        {
            Wood,
            Stone,
            Iron,
            Emerald,
            Gold
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumToolMaterial WOOD { get; private set; }
        public static EnumToolMaterial STONE { get; private set; }
        public static EnumToolMaterial IRON { get; private set; }
        public static EnumToolMaterial EMERALD { get; private set; }
        public static EnumToolMaterial GOLD { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumToolMaterial[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumToolMaterial;
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
        internal ToolMaterial Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public int GetMaxUses()
        {
            throw new System.NotImplementedException();
        }

        public float GetDamageVsEntity()
        {
            throw new System.NotImplementedException();
        }

        public float GetEfficiencyOnProperMaterial()
        {
            throw new System.NotImplementedException();
        }

        public int GetEnchantability()
        {
            throw new System.NotImplementedException();
        }

        public int GetToolCraftingMaterial()
        {
            throw new System.NotImplementedException();
        }

        public int GetHarvestLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}
