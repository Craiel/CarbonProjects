namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumMobType : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumMobType()
        {
            everything = new EnumMobType(MobTypeEnum.Everything);
            mobs = new EnumMobType(MobTypeEnum.Mobs);
            players = new EnumMobType(MobTypeEnum.Players);

            Values = new[]
                         {
                             everything, mobs, players
                         };
        }

        internal EnumMobType(MobTypeEnum type)
        {
            this.Enum = type;
        }

        internal enum MobTypeEnum
        {
            Everything,
            Mobs,
            Players,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumMobType everything { get; private set; }
        public static EnumMobType mobs { get; private set; }
        public static EnumMobType players { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumMobType[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumMobType;
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
        internal MobTypeEnum Enum { get; private set; }

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
