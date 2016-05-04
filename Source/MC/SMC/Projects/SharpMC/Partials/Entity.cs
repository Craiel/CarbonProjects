namespace SharpMC.Source
{
    using System;

    internal partial class Entity
    {
        public bool GetDistanceSqAsBool(double par1, double par3, double par5)
        {
            return Math.Abs(this.GetDistanceSq(par1, par3, par5) - 1) < double.Epsilon;
        }
    }
}
