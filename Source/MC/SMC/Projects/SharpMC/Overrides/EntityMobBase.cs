namespace SharpMC.Overrides
{
    using SharpMC.Source;

    internal abstract class EntityMobBase : EntityCreature, IMobMODIFIED
    {
        protected EntityMobBase(World par1World)
            : base(par1World)
        {
            mobSelector = new FilterIMob();
        }

        public IEntitySelector mobSelector { get; set; }
    }
}
