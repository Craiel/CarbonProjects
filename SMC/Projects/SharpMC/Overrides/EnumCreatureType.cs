namespace SharpMC.Overrides
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    using SharpMC.Source;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumCreatureType : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumCreatureType()
        {
            monster = new EnumCreatureType(CreatureType.Monster, 70, Material.air, false, false);
            creature = new EnumCreatureType(CreatureType.Creature, 10, Material.air, true, true);
            waterCreature = new EnumCreatureType(CreatureType.WaterCreature, 15, Material.air, true, false);
            ambient = new EnumCreatureType(CreatureType.Ambient, 5, Material.water, true, false);

            values = new[] { monster, creature, waterCreature, ambient };
        }

        internal EnumCreatureType(CreatureType type, int maxNumberOfCreatures, Material material, bool isPeaceful, bool isAnimal)
        {
            this.Enum = type;
            this.MaxNumberOfCreatures = maxNumberOfCreatures;
            this.Material = material;
            this.IsPeaceful = isPeaceful;
            this.IsAnimal = isAnimal;
        }

        internal enum CreatureType
        {
            Monster,
            Creature,
            WaterCreature,
            Ambient,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumCreatureType monster { get; private set; }
        public static EnumCreatureType creature { get; private set; }
        public static EnumCreatureType waterCreature { get; private set; }
        public static EnumCreatureType ambient { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumCreatureType[] values { get; private set; }

        public int MaxNumberOfCreatures { get; private set; }

        public bool IsPeaceful { get; private set; }
        public bool IsAnimal { get; private set; }

        internal Material Material { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumCreatureType;
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

        public static EnumCreatureType[] Values()
        {
            return values;
        }

        // -------------------------------------------------------------------
        // Internal
        // -------------------------------------------------------------------
        internal CreatureType Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        internal Material GetCreatureMaterial()
        {
            throw new System.NotImplementedException();
        }

        public bool GetPeacefulCreature()
        {
            throw new System.NotImplementedException();
        }

        public bool GetAnimal()
        {
            throw new System.NotImplementedException();
        }

        public Type GetCreatureClass()
        {
            throw new NotImplementedException();
        }

        public decimal GetMaxNumberOfCreature()
        {
            throw new NotImplementedException();
        }
    }
}
