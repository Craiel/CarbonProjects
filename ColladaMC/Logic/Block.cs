namespace ColladaMC.Logic
{
    using System;
    using System.Collections.Generic;

    internal class Block
    {
        private readonly List<string> materials;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Block()
        {
            this.materials = new List<string>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public IList<string> Materials => this.materials;

        public override int GetHashCode()
        {
            return Tuple.Create(this.X, this.Y, this.Z).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var typed = obj as Block;
            if (typed == null)
            {
                return false;
            }

            return typed.X == this.X && typed.Y == this.Y && typed.Z == this.Z;
        }
    }
}
