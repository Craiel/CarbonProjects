namespace SharpMC.Overrides
{
    using SharpMC.Source;

    internal abstract class EntityGhastBase : EntityFlying, IMobMODIFIED
    {
        protected EntityGhastBase(World par1World)
            : base(par1World)
        {
            mobSelector = new FilterIMob();
        }

        public IEntitySelector mobSelector { get; set; }
    }
}
