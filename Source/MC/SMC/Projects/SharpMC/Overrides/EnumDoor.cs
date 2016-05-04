namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumDoor : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumDoor()
        {
            OPENING = new EnumDoor(DoorEnum.Opening);
            WOOD_DOOR = new EnumDoor(DoorEnum.WoodDoor);
            GRATES = new EnumDoor(DoorEnum.Grates);
            IRON_DOOR = new EnumDoor(DoorEnum.IronDoor);

            Values = new[]
                         {
                             OPENING, WOOD_DOOR, GRATES, IRON_DOOR
                         };

            // Todo: Unchecked

            doorEnum = new[]
                           {
                               (int)DoorEnum.Opening, (int)DoorEnum.WoodDoor, (int)DoorEnum.Grates, (int)DoorEnum.IronDoor
                           };
        }

        internal EnumDoor(DoorEnum type)
        {
            this.Enum = type;
        }

        internal enum DoorEnum
        {
            Opening,
            WoodDoor,
            Grates,
            IronDoor,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumDoor OPENING { get; private set; }
        public static EnumDoor WOOD_DOOR { get; private set; }
        public static EnumDoor GRATES { get; private set; }
        public static EnumDoor IRON_DOOR { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumDoor[] Values { get; private set; }
        public static int[] doorEnum { get; private set; }
        
        public override bool Equals(object obj)
        {
            var typed = obj as EnumDoor;
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
        internal DoorEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public static EnumDoor ValueOf(string value)
        {
            throw new System.NotImplementedException();
        }

        public string Name()
        {
            throw new System.NotImplementedException();
        }

        public int Ordinal()
        {
            throw new System.NotImplementedException();
        }
    }
}
