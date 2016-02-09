namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumAction : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumAction()
        {
            none = new EnumAction(ActionEnum.None);
            eat = new EnumAction(ActionEnum.Eat);
            drink = new EnumAction(ActionEnum.Drink);
            block = new EnumAction(ActionEnum.Block);
            bow = new EnumAction(ActionEnum.Bow);

            Values = new[]
                         {
                             none, eat, drink, block, bow
                         };
        }

        internal EnumAction(ActionEnum type)
        {
            this.Enum = type;
        }

        internal enum ActionEnum
        {
            None,
            Eat,
            Drink,
            Block,
            Bow
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumAction none { get; private set; }
        public static EnumAction eat { get; private set; }
        public static EnumAction drink { get; private set; }
        public static EnumAction block { get; private set; }
        public static EnumAction bow { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumAction[] Values { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumAction;
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
        internal ActionEnum Enum { get; private set; }

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
