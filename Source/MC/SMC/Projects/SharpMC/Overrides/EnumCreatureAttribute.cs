namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumCreatureAttribute : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumCreatureAttribute()
        {
            UNDEFINED = new EnumCreatureAttribute(CreatureAttribute.Undefined);
            UNDEAD = new EnumCreatureAttribute(CreatureAttribute.Undead);
            ARTHROPOD = new EnumCreatureAttribute(CreatureAttribute.Arthropod);

            Values = new[]
                         {
                             UNDEFINED, UNDEAD, ARTHROPOD
                         };
        }

        internal EnumCreatureAttribute(CreatureAttribute type)
        {
            this.Enum = type;
        }

        internal enum CreatureAttribute
        {
            Undefined,
            Undead,
            Arthropod,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumCreatureAttribute UNDEFINED { get; private set; }
        public static EnumCreatureAttribute UNDEAD { get; private set; }
        public static EnumCreatureAttribute ARTHROPOD { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumCreatureAttribute[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumCreatureAttribute;
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
        internal CreatureAttribute Enum { get; private set; }

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
