namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumStatus : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumStatus()
        {
            OK = new EnumStatus(StatusEnum.Ok);
            NOT_POSSIBLE_HERE = new EnumStatus(StatusEnum.NotPossibleHere);
            NOT_POSSIBLE_NOW = new EnumStatus(StatusEnum.NotPossibleNow);
            TOO_FAR_AWAY = new EnumStatus(StatusEnum.TooFarAway);
            OTHER_PROBLEM = new EnumStatus(StatusEnum.OtherProblem);
            NOT_SAFE = new EnumStatus(StatusEnum.NotSafe);

            Values = new[]
                         {
                             OK, NOT_POSSIBLE_HERE, NOT_POSSIBLE_NOW, TOO_FAR_AWAY, OTHER_PROBLEM, NOT_SAFE
                         };
        }

        internal EnumStatus(StatusEnum type)
        {
            this.Enum = type;
        }

        internal enum StatusEnum
        {
            Ok,
            NotPossibleHere,
            NotPossibleNow,
            TooFarAway,
            OtherProblem,
            NotSafe
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumStatus OK { get; private set; }
        public static EnumStatus NOT_POSSIBLE_HERE { get; private set; }
        public static EnumStatus NOT_POSSIBLE_NOW { get; private set; }
        public static EnumStatus TOO_FAR_AWAY { get; private set; }
        public static EnumStatus OTHER_PROBLEM { get; private set; }
        public static EnumStatus NOT_SAFE { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumStatus[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumStatus;
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
        internal StatusEnum Enum { get; private set; }

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
