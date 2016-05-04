namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumArt : IJavaEnum
    {
        public static int maxArtTitleLength;

        public int offsetX;

        public int offsetY;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumArt()
        {
            Kebab = new EnumArt(ArtEnum.Kebab, "Kebab", 16, 16, 0, 0);
            Aztec = new EnumArt(ArtEnum.Aztec, "Aztec", 16, 16, 16, 0);
            Alban = new EnumArt(ArtEnum.Alban, "Alban", 16, 16, 32, 0);
            Aztec2 = new EnumArt(ArtEnum.Aztec2, "Aztec2", 16, 16, 48, 0);
            Bomb = new EnumArt(ArtEnum.Bomb, "Bomb", 16, 16, 64, 0);
            Plant = new EnumArt(ArtEnum.Plant, "Plant", 16, 16, 80, 0);
            Wasteland = new EnumArt(ArtEnum.Wasteland, "Wasteland", 16, 16, 96, 0);
            Pool = new EnumArt(ArtEnum.Pool, "Pool", 32, 16, 0, 32);
            Courbet = new EnumArt(ArtEnum.Courbet, "Courbet", 32, 16, 32, 32);
            Sea = new EnumArt(ArtEnum.Sea, "Sea", 32, 16, 64, 32);
            Sunset = new EnumArt(ArtEnum.Sunset, "Sunset", 32, 16, 96, 32);
            Creebet = new EnumArt(ArtEnum.Creebet, "Creebet", 32, 16, 128, 32);
            Wanderer = new EnumArt(ArtEnum.Wanderer, "Wanderer", 16, 32, 0, 64);
            Graham = new EnumArt(ArtEnum.Graham, "Graham", 16, 32, 16, 64);
            Match = new EnumArt(ArtEnum.Match, "Match", 32, 32, 0, 128);
            Bust = new EnumArt(ArtEnum.Bust, "Bust", 32, 32, 32, 128);
            Stage = new EnumArt(ArtEnum.Stage, "Stage", 32, 32, 64, 128);
            Void = new EnumArt(ArtEnum.Void, "Void", 32, 32, 96, 128);
            SkullAndRoses = new EnumArt(ArtEnum.SkullAndRoses, "SkullAndRoses", 32, 32, 128, 128);
            Wither = new EnumArt(ArtEnum.Wither, "Wither", 32, 32, 160, 128);
            Fighters = new EnumArt(ArtEnum.Fighters, "Fighters", 64, 32, 0, 96);
            Pointer = new EnumArt(ArtEnum.Pointer, "Pointer", 64, 64, 0, 192);
            Pigscene = new EnumArt(ArtEnum.Pigscene, "Pigscene", 64, 64, 64, 192);
            BurningSkull = new EnumArt(ArtEnum.BurningSkull, "BurningSkull", 64, 64, 128, 192);
            Skeleton = new EnumArt(ArtEnum.Skeleton, "Skeleton", 64, 48, 192, 64);
            DonkeyKong = new EnumArt(ArtEnum.DonkeyKong, "DonkeyKong", 64, 48, 192, 112);

            Values = new[]
                         {
                             Kebab, Aztec, Alban, Aztec2, Bomb, Plant, Wasteland, Pool, Courbet, Sea,
                             Sunset, Creebet, Wanderer, Graham, Match, Bust, Stage, Void, SkullAndRoses,
                             Wither, Fighters, Pointer, Pigscene, BurningSkull, Skeleton, DonkeyKong
                         };
        }

        internal EnumArt(ArtEnum type, string title, int sizeX, int sizeY, int offsetX, int offsetY)
        {
            this.Enum = type;
            this.title = title;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        internal enum ArtEnum
        {
            Kebab,
            Aztec,
            Alban,
            Aztec2,
            Bomb,
            Plant,
            Wasteland,
            Pool,
            Courbet,
            Sea,
            Sunset,
            Creebet,
            Wanderer,
            Graham,
            Match,
            Bust,
            Stage,
            Void,
            SkullAndRoses,
            Wither,
            Fighters,
            Pointer,
            Pigscene,
            BurningSkull,
            Skeleton,
            DonkeyKong
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumArt Kebab { get; private set; }
        public static EnumArt Aztec { get; private set; }
        public static EnumArt Alban { get; private set; }
        public static EnumArt Aztec2 { get; private set; }
        public static EnumArt Bomb { get; private set; }
        public static EnumArt Plant { get; private set; }
        public static EnumArt Wasteland { get; private set; }
        public static EnumArt Pool { get; private set; }
        public static EnumArt Courbet { get; private set; }
        public static EnumArt Sea { get; private set; }
        public static EnumArt Sunset { get; private set; }
        public static EnumArt Creebet { get; private set; }
        public static EnumArt Wanderer { get; private set; }
        public static EnumArt Graham { get; private set; }
        public static EnumArt Match { get; private set; }
        public static EnumArt Bust { get; private set; }
        public static EnumArt Stage { get; private set; }
        public static EnumArt Void { get; private set; }
        public static EnumArt SkullAndRoses { get; private set; }
        public static EnumArt Wither { get; private set; }
        public static EnumArt Fighters { get; private set; }
        public static EnumArt Pointer { get; private set; }
        public static EnumArt Pigscene { get; private set; }
        public static EnumArt BurningSkull { get; private set; }
        public static EnumArt Skeleton { get; private set; }
        public static EnumArt DonkeyKong { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumArt[] Values { get; private set; }

        public string title { get; private set; }
        public int sizeX { get; private set; }
        public int sizeY { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumArt;
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
        internal ArtEnum Enum { get; private set; }

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
