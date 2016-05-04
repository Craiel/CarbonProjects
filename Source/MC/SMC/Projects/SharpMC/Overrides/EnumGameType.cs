namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;

    using SharpMC.Source;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumGameType : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumGameType()
        {
            NOT_SET = new EnumGameType(GameType.NotSet);
            SURVIVAL = new EnumGameType(GameType.Survival, "survival");
            CREATIVE = new EnumGameType(GameType.Creative, "creative");
            ADVENTURE = new EnumGameType(GameType.Adventure, "adventure");

            Values = new[] { NOT_SET, SURVIVAL, CREATIVE, ADVENTURE };
        }

        internal EnumGameType(GameType type, string name = "")
        {
            this.Enum = type;
            this.Name = name;
        }

        internal enum GameType
        {
            NotSet = -1,
            Survival = 0,
            Creative = 1,
            Adventure = 2,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumGameType NOT_SET { get; private set; }
        public static EnumGameType SURVIVAL { get; private set; }
        public static EnumGameType CREATIVE { get; private set; }
        public static EnumGameType ADVENTURE { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumGameType[] Values { get; private set; }

        public string Name { get; private set; }
        
        public override bool Equals(object obj)
        {
            var typed = obj as EnumGameType;
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
        internal GameType Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public static EnumGameType GetByID(int id)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigurePlayerCapabilities(object capabilities)
        {
            throw new System.NotImplementedException();
        }

        public static EnumGameType GetByName(string gameMode)
        {
            throw new System.NotImplementedException();
        }

        public bool IsAdventure()
        {
            throw new System.NotImplementedException();
        }

        public bool IsCreative()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSurvivalOrAdventure()
        {
            throw new System.NotImplementedException();
        }
    }
}
