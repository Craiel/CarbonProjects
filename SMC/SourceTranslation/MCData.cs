namespace SMC.SourceTranslation
{
    using System.Collections.Generic;

    using SMC.SourceLib.Patching;

    // ReSharper disable once InconsistentNaming
    public static class MCData
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public const string RootNameSpace = "SMC.Source";

        // MC related patches
        public static readonly IList<Patch> Patches = new List<Patch>
        {
            // Generic
            new Patch("(byte) - 1", "byte.MaxValue"),
            
            new Patch("public void WriteEntityToNBT(", "protected void WriteEntityToNBT("),
            new Patch("public void ReadEntityFromNBT(", "protected void ReadEntityFromNBT("),

            new PatchExpression(@"((byte[\s]+[\w_]+[\s]+=[\s]+)-1;)", "{0}byte.MaxValue;") { ReplacementGroup = 2 },

            // File specific patches
            new Patch(
                "? this.worldGeneratorBigTree",
                "? (WorldGenerator)this.worldGeneratorBigTree",
                fileFilters: new[] { "BiomeGenForest.java" }),

            new Patch(
                ": this.worldGeneratorTrees",
                ": (WorldGenerator)this.worldGeneratorTrees",
                fileFilters: new[] { "BiomeGenForest.java" }),

            new Patch(
                "? new WorldGenHugeTrees",
                "? (WorldGenerator)new WorldGenHugeTrees",
                fileFilters: new[] { "BiomeGenJungle.java" }),

            new Patch(
                ": new WorldGenTrees",
                ": (WorldGenerator)new WorldGenTrees",
                fileFilters: new[] { "BiomeGenJungle.java" }),
                
            new Patch("EnumDoorHelper.", "EnumDoor."),

            new PatchExpression(
                @"(\(bool\)(par1EntityPlayer.GetDistanceSq\(.*?<=[\s]+[0-9]+\.0D))",
                "({0})",
                fileFilters: new[]
                    {
                        "ContainerEnchantment.java",
                        "ContainerWorkbench.java",
                        "ContainerRepair.java"
                    })
                {
                    ReplacementGroup = 2,
                    QuickTestString = ".GetDistanceSq("
                },

            new Patch(
                "(bool)par2Random.NextInt(par1 + 1) > 0",
                "(par2Random.NextInt(par1 + 1) > 0)",
                fileFilters: new[] { "EnchantmentDurability.java" }),

            new Patch(
                "(bool)this.protectionType == 2 || var2.protectionType == 2",
                "(this.protectionType == 2 || var2.protectionType == 2)",
                fileFilters: new[] { "EnchantmentProtection.java" }),

            new Patch(
                "(bool)par1Random.NextFloat() < 0.15F * (float)par0", 
                "(par1Random.NextFloat() < 0.15F * (float)par0)",
                fileFilters: new[] { "EnchantmentThorns.java" }),

            new Patch(
                "float var6 = (par0Random.NextFloat() +",
                "float var6 = (float)(par0Random.NextFloat() +",
                fileFilters: new[] { "EnchantmentHelper.java" }),

            new Patch(
                "(bool)this.field_75384_e > 0 && this.HasPlayerGotBoneInHand(this.thePlayer)",
                "(this.field_75384_e > 0 && this.HasPlayerGotBoneInHand(this.thePlayer))",
                fileFilters: new[] { "EntityAIBeg.java" }),

            new Patch(
                "(bool)this.theWorld.GetBlockId(var1, var2 - 1, var3) == Block.grass.blockID",
                "(this.theWorld.GetBlockId(var1, var2 - 1, var3) == Block.grass.blockID)",
                fileFilters: new[] { "EntityAIEatGrass.java" }),

            new Patch(
                "(bool)this.leaper.GetRNG().NextInt(5) == 0",
                "(this.leaper.GetRNG().NextInt(5) == 0)",
                fileFilters: new[] { "EntityAILeapAtTarget.java" }),

            new PatchExpression(
                @"(\(bool\)(\(double\)this.frontDoor.GetInsideDistanceSquare\(.*?<[\s]+[0-9]+\.[0-9]+D))",
                "({0})",
                fileFilters: new[] { "EntityAIRestrictOpenDoor.java" })
                {
                    ReplacementGroup = 2,
                    QuickTestString = ".GetInsideDistanceSquare("
                },

            new Patch(
                "(bool)var1.itemID == this.breedingFood",
                "(var1.itemID == this.breedingFood)",
                fileFilters: new[] { "EntityAITempt.java" }),

            new Patch(
                "(bool)var1.openContainer is Container",
                "(var1.openContainer is Container)",
                fileFilters: new[] { "EntityAITradePlayer.java" }),

            new Patch(
                ": (bool)this.lookTime > 0",
                ": (this.lookTime > 0)",
                fileFilters: new[] { "EntityAIWatchClosest.java" }),

            new Patch(
                "(bool)this.homePosition.GetDistanceSquared(par1, par2, par3) < this.maximumHomeDistance * this.maximumHomeDistance",
                "(this.homePosition.GetDistanceSquared(par1, par2, par3) < this.maximumHomeDistance * this.maximumHomeDistance)",
                fileFilters: new[] { "EntityCreature.java" }),

            new Patch(
                "var2 &= -2;",
                "var2 = (byte)(var2 & -2);",
                fileFilters: new[] { "EntityBlaze.java", "EntitySpider.java" }),
                
            new Patch(
                "par0 ^= par0 >>> 20 ^ par0 >>> 12;",
                "par0 ^= (int)((uint)par0 >> 20 ^ (uint)par0 >> 12);",
                fileFilters: new[] { "IntHashMap.java", "LongHashMap.java" }),

            new Patch(
                "return par0 ^ par0 >>> 7 ^ par0 >>> 4;",
                "return (int)(par0 ^ (uint)par0 >> 7 ^ (uint)par0 >> 4);",
                fileFilters: new[] { "IntHashMap.java", "LongHashMap.java" }),

            new Patch(
                "return Hash((int)(par0 ^ par0 >>> 32));",
                "return Hash((int)(par0 ^ (uint)par0 >> 32));",
                fileFilters: new[] { "LongHashMap.java" }),

            new Patch(
                "var11 = -1;",
                "var11 = byte.MaxValue;",
                fileFilters: new[] { "MinecraftServer.java" }),

            new Patch(
                "var16 = -10;",
                "var16 = byte.MaxValue - 10;",
                fileFilters: new[] { "RenderLivingEntity.java" }),

            new Patch(
                "EntityDragon : EntityLiving , IBossDisplayData, IEntityMultiPart, IMob",
               "EntityDragon : EntityDragonBase, IBossDisplayData, IEntityMultiPart",
               fileFilters: new[] { "EntityDragon.java" }),

            new Patch(
                "EntityGhast : EntityFlying , IMob",
               "EntityGhast : EntityGhastBase",
               fileFilters: new[] { "EntityGhast.java" }),

            new Patch(
                "class EntityMob : EntityCreature , IMob",
               "class EntityMob : EntityMobBase",
               fileFilters: new[] { "EntityMob.java" }),

            new Patch(
                "EntitySlime : EntityLiving , IMob",
               "EntitySlime : EntitySlimeBase",
               fileFilters: new[] { "EntitySlime.java" }),

            new Patch(
                "var3 = -1;",
                "var3 = byte.MaxValue;",
                fileFilters: new[] { "EntityMinecart.java" }),

            new Patch(
                ": (bool)par1EntityPlayer.GetDistanceSqToEntity(this) <= 64.0D",
                ": (par1EntityPlayer.GetDistanceSqToEntity(this) <= 64.0D)",
                fileFilters: new[] { "EntityMinecartContainer.java" }),

            new Patch(
                "switch (par1)",
                "switch ((byte)par1)",
                fileFilters: new[] { "GuiTextField.java" }),

            new Patch(
                "switch (var8)",
                "switch ((byte)var8)",
                fileFilters: new[] { "FontRenderer.java" }),

            new Patch(
                "public void Func_110354_a",
                "protected void Func_110354_a",
                fileFilters: new[] { "GuiScreenCreateOnlineWorld.java" }),
        };
        
        public static readonly IDictionary<string, string> StructureMapping = new Dictionary<string, string>
        {
            { "Anvil", @"Item\Anvil" },
            { "block", @"World\Blocks" },
            { "world", "World" },
            { "Callable", "Callable" },
            { "Chunk", "Chunk" },
            { "Command", "Command" },
            { "Component", "Component" },
            { "Container", "Container" },
            { "Biome", "Biome" },
            { "CreativeTab", "CreativeTab" },
            { "DedicatedServer", @"Network\Server" },
            { "DispenserBehavior", "DispenserBehavior" },
            { "Enchantment", "Enchantment" },
            { "EntityAI", @"Entity\AI" },
            { "Entity", "Entity" },
            { "Gui", "Gui" },
            { "GenLayer", @"World\GenLayer" },
            { "Hopper", "Item" },
            { "Item", "Item" },
            { "Map", "Map" },
            { "Model", "Model" },
            { "NBT", "NBT" },
            { "Packet", @"Network\Packets" },
            { "Potion", @"Item\Potion" },
            { "Recipes", @"Item\Recipes" },
            { "Render", @"Graphics\Render" },
            { "Resource", "Resource" },
            { "Score", "Score" },
            { "Structure", @"World\Structure" },
            { "Slot", @"Item\Slot" },
            { "Sound", "Sound" },
            { "Stat", "Stat" },
            { "Server", @"Network\Server" },
            { "Texture", @"Graphics\Texture" },
            { "TileEntity", @"World\TileEntities" },
            { "Thread", "Thread" },
            { "Tcp", "Network" },
            { "Village", @"World\Village" },
        };

        public static readonly IList<string> Usings = new List<string>
        {
            "System",
            "System.Collections",
            "System.Collections.Generic",
            "System.IO",
            "System.Runtime.CompilerServices",
            "System.Text",
            "System.Threading",
            
            RootNameSpace,
            "CarbonCore.JSharpBridge",
            "CarbonCore.JSharpBridge.Core",
            "CarbonCore.JSharpBridge.Crypto",
            "CarbonCore.JSharpBridge.Collections",
            "CarbonCore.JSharpBridge.GL",
            "CarbonCore.JSharpBridge.IO",
            "CarbonCore.JSharpBridge.Json",
            "CarbonCore.JSharpBridge.Log",
            "CarbonCore.JSharpBridge.Net",
            "CarbonCore.JSharpBridge.Sound",
            "SMC.Overrides",
        };

        public static readonly IList<string> IgnoreList = new List<string>
        {
            @"\Enum",
            "GuiScreenConfirmationType.java",
        };

        public static readonly IList<string> TargetIncludes = new List<string> { "Bridge", "Partials", "Overrides" };
    }
}
