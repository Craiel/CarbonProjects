namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumFacing : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumFacing()
        {
            DOWN = new EnumFacing(FacingEnum.Down, 0, 1, 0, -1, 0);
            UP = new EnumFacing(FacingEnum.Up, 1, 0, 0, 1, 0);
            NORTH = new EnumFacing(FacingEnum.North, 2, 3, 0, 0, -1);
            SOUTH = new EnumFacing(FacingEnum.South, 3, 2, 0, 0, 1);
            EAST = new EnumFacing(FacingEnum.East, 4, 5, -1, 0, 0);
            WEST = new EnumFacing(FacingEnum.West, 5, 4, 1, 0, 0);

            Values = new[]
                         {
                             DOWN, UP, NORTH, SOUTH, EAST, WEST
                         };
        }

        internal EnumFacing(FacingEnum type, int orderA, int orderB, int frontOffsetX, int frontOffsetY, int frontOffsetZ)
        {
            this.Enum = type;
            this.OrderA = orderA;
            this.OrderB = orderB;
            this.FrontOffsetX = frontOffsetX;
            this.FrontOffsetY = frontOffsetY;
            this.FrontOffsetZ = frontOffsetZ;
        }

        internal enum FacingEnum
        {
            Down,
            Up,
            North,
            South,
            East,
            West
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumFacing DOWN { get; private set; }
        public static EnumFacing UP { get; private set; }
        public static EnumFacing NORTH { get; private set; }
        public static EnumFacing SOUTH { get; private set; }
        public static EnumFacing EAST { get; private set; }
        public static EnumFacing WEST { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumFacing[] Values { get; private set; }
        
        public int OrderA { get; private set; }
        public int OrderB { get; private set; }
        public int FrontOffsetX { get; private set; }
        public int FrontOffsetY { get; private set; }
        public int FrontOffsetZ { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumFacing;
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
        internal FacingEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public int GetFrontOffsetX()
        {
            return this.FrontOffsetX;
        }

        public int GetFrontOffsetY()
        {
            return this.FrontOffsetY;
        }

        public int GetFrontOffsetZ()
        {
            return this.FrontOffsetZ;
        }

        public static EnumFacing GetFront(int i)
        {
            throw new System.NotImplementedException();
        }
    }
}
