namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumEntitySize : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumEntitySize()
        {
            SIZE_1 = new EnumEntitySize(EntitySizeEnum.Size1);
            SIZE_2 = new EnumEntitySize(EntitySizeEnum.Size2);
            SIZE_3 = new EnumEntitySize(EntitySizeEnum.Size3);
            SIZE_4 = new EnumEntitySize(EntitySizeEnum.Size4);
            SIZE_5 = new EnumEntitySize(EntitySizeEnum.Size5);
            SIZE_6 = new EnumEntitySize(EntitySizeEnum.Size6);

            Values = new[]
                         {
                             SIZE_1, SIZE_2, SIZE_3, SIZE_4, SIZE_5, SIZE_6
                         };
        }

        internal EnumEntitySize(EntitySizeEnum type)
        {
            this.Enum = type;
        }

        internal enum EntitySizeEnum
        {
            Size1,
            Size2,
            Size3,
            Size4,
            Size5,
            Size6,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumEntitySize SIZE_1 { get; private set; }
        public static EnumEntitySize SIZE_2 { get; private set; }
        public static EnumEntitySize SIZE_3 { get; private set; }
        public static EnumEntitySize SIZE_4 { get; private set; }
        public static EnumEntitySize SIZE_5 { get; private set; }
        public static EnumEntitySize SIZE_6 { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumEntitySize[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumEntitySize;
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
        internal EntitySizeEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public int MultiplyBy32AndRound(double value)
        {
            throw new System.NotImplementedException();
        }
    }
}
