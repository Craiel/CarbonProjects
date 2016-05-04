namespace SharpMC.Overrides
{
    using SharpMC.Source;

    internal abstract class EntitySlimeBase : EntityLiving, IMobMODIFIED
    {
        protected EntitySlimeBase(World par1World)
            : base(par1World)
        {
            mobSelector = new FilterIMob();
        }

        public IEntitySelector mobSelector { get; set; }
    }
}
