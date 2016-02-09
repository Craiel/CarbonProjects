namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumMovingObjectType : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumMovingObjectType()
        {
            TILE = new EnumMovingObjectType(MovingObjectTypeEnum.Title);
            ENTITY = new EnumMovingObjectType(MovingObjectTypeEnum.Entity);

            Values = new[]
                         {
                             TILE, ENTITY
                         };
        }

        internal EnumMovingObjectType(MovingObjectTypeEnum type)
        {
            this.Enum = type;
        }

        internal enum MovingObjectTypeEnum
        {
            Title,
            Entity
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumMovingObjectType TILE { get; private set; }
        public static EnumMovingObjectType ENTITY { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumMovingObjectType[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumMovingObjectType;
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
        internal MovingObjectTypeEnum Enum { get; private set; }

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
