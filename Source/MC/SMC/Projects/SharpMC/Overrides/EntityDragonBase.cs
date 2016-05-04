namespace SharpMC.Overrides
{
    using SharpMC.Source;

    internal abstract class EntityDragonBase : EntityLiving, IMobMODIFIED
    {
        protected EntityDragonBase(World par1World)
            : base(par1World)
        {
            mobSelector = new FilterIMob();
        }

        public IEntitySelector mobSelector { get; set; }
    }
}
