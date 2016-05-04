namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Collections;
    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumChatFormatting : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumChatFormatting()
        {
            BLACK = new EnumChatFormatting(ChatFormatting.Black, '0');
            GRAY = new EnumChatFormatting(ChatFormatting.Gray, '7');
            GREEN = new EnumChatFormatting(ChatFormatting.Green, 'a');
            GOLD = new EnumChatFormatting(ChatFormatting.Gold, '6');
            RED = new EnumChatFormatting(ChatFormatting.Red, 'c');
            BLUE = new EnumChatFormatting(ChatFormatting.Blue, '9');
            AQUA = new EnumChatFormatting(ChatFormatting.Aqua, 'b');
            YELLOW = new EnumChatFormatting(ChatFormatting.Yellow, 'e');
            WHITE = new EnumChatFormatting(ChatFormatting.White, 'f');
            LIGHT_PURPLE = new EnumChatFormatting(ChatFormatting.LightPurple, 'd');
            DARK_BLUE = new EnumChatFormatting(ChatFormatting.DarkBlue, '1');
            DARK_GREEN = new EnumChatFormatting(ChatFormatting.DarkGreen, '2');
            DARK_GRAY = new EnumChatFormatting(ChatFormatting.DarkGray, '8');
            DARK_AQUA = new EnumChatFormatting(ChatFormatting.DarkAqua, '3');
            DARK_RED = new EnumChatFormatting(ChatFormatting.DarkRed, '4');
            DARK_PURPLE = new EnumChatFormatting(ChatFormatting.DarkPurple, '5');
            OBFUSCATED = new EnumChatFormatting(ChatFormatting.Obfuscated, 'k', true);
            BOLD = new EnumChatFormatting(ChatFormatting.Bold, 'l', true);
            STRIKETHROUGH = new EnumChatFormatting(ChatFormatting.StrikeThrough, 'm', true);
            UNDERLINE = new EnumChatFormatting(ChatFormatting.Underline, 'n', true);
            ITALIC = new EnumChatFormatting(ChatFormatting.Italic, 'o', true);
            RESET = new EnumChatFormatting(ChatFormatting.Reset, 'r');

            Values = new[]
                         {
                             BLACK, GRAY, GREEN, GOLD, RED, BLUE, AQUA, YELLOW, WHITE, LIGHT_PURPLE, DARK_BLUE, DARK_GREEN,
                             DARK_GRAY, DARK_AQUA, DARK_RED, DARK_PURPLE, OBFUSCATED, BOLD, STRIKETHROUGH, UNDERLINE,
                             ITALIC, RESET
                         };
        }

        internal EnumChatFormatting(ChatFormatting type, int unknown, bool unknown2 = false)
        {
            this.Enum = type;
        }

        internal enum ChatFormatting
        {
            Black,
            Gray,
            Green,
            Gold,
            Red,
            Blue,
            Aqua,
            Yellow,
            White,
            LightPurple,
            DarkBlue,
            DarkGreen,
            DarkGray,
            DarkAqua,
            DarkRed,
            DarkPurple,

            Obfuscated,
            Bold,
            StrikeThrough,
            Underline,
            Italic,
            Reset,
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumChatFormatting BLACK { get; private set; }
        public static EnumChatFormatting GRAY { get; private set; }
        public static EnumChatFormatting GREEN { get; private set; }
        public static EnumChatFormatting GOLD { get; private set; }
        public static EnumChatFormatting RED { get; private set; }
        public static EnumChatFormatting BLUE { get; private set; }
        public static EnumChatFormatting AQUA { get; private set; }
        public static EnumChatFormatting YELLOW { get; private set; }
        public static EnumChatFormatting WHITE { get; private set; }
        public static EnumChatFormatting LIGHT_PURPLE { get; private set; }
        public static EnumChatFormatting DARK_BLUE { get; private set; }
        public static EnumChatFormatting DARK_GREEN { get; private set; }
        public static EnumChatFormatting DARK_GRAY { get; private set; }
        public static EnumChatFormatting DARK_AQUA { get; private set; }
        public static EnumChatFormatting DARK_RED { get; private set; }
        public static EnumChatFormatting DARK_PURPLE { get; private set; }
        public static EnumChatFormatting OBFUSCATED { get; private set; }
        public static EnumChatFormatting BOLD { get; private set; }
        public static EnumChatFormatting STRIKETHROUGH { get; private set; }
        public static EnumChatFormatting UNDERLINE { get; private set; }
        public static EnumChatFormatting ITALIC { get; private set; }
        public static EnumChatFormatting RESET { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumChatFormatting[] Values { get; private set; }
        
        public override bool Equals(object obj)
        {
            var typed = obj as EnumChatFormatting;
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
        internal ChatFormatting Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public bool IsColor()
        {
            throw new System.NotImplementedException();
        }

        public static EnumChatFormatting Func_96300_b(string getAsString)
        {
            throw new System.NotImplementedException();
        }

        public object Func_96297_d()
        {
            throw new System.NotImplementedException();
        }

        public static string Func_110646_a(string getEntityName)
        {
            throw new System.NotImplementedException();
        }

        public static JavaCollection Func_96296_a(bool p0, bool p1)
        {
            throw new System.NotImplementedException();
        }
    }
}
