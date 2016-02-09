namespace SharpMC.Overrides
{
    using System.Diagnostics.CodeAnalysis;

    using CarbonCore.JSharpBridge.Contracts;
    
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class EnumOptions : IJavaEnum
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static EnumOptions()
        {
            MUSIC = new EnumOptions(OptionsEnum.Music, "options.music", true, false);
            SOUND = new EnumOptions(OptionsEnum.Sound, "options.sound", true, false);
            INVERT_MOUSE = new EnumOptions(OptionsEnum.InvertMouse, "options.invertMouse", false, true);
            SENSITIVITY = new EnumOptions(OptionsEnum.Sensitivity, "options.sensitivity", true, false);
            FOV = new EnumOptions(OptionsEnum.FieldOfView, "options.fov", true, false);
            GAMMA = new EnumOptions(OptionsEnum.Gamma, "options.gamma", true, false);
            RENDER_DISTANCE = new EnumOptions(OptionsEnum.RenderDistance, "options.renderDistance", false, false);
            VIEW_BOBBING = new EnumOptions(OptionsEnum.ViewBobbing, "options.viewBobbing", false, true);
            ANAGLYPH = new EnumOptions(OptionsEnum.Anaglyph, "options.anaglyph", false, true);
            ADVANCED_OPENGL = new EnumOptions(OptionsEnum.AdvancedOpenGL, "options.advancedOpengl", false, true);
            FRAMERATE_LIMIT = new EnumOptions(OptionsEnum.FramerateLimit, "options.framerateLimit", false, false);
            DIFFICULTY = new EnumOptions(OptionsEnum.Difficulty, "options.difficulty", false, false);
            GRAPHICS = new EnumOptions(OptionsEnum.Graphics, "options.graphics", false, false);
            AMBIENT_OCCLUSION = new EnumOptions(OptionsEnum.AmbientOcclusion, "options.ao", false, false);
            GUI_SCALE = new EnumOptions(OptionsEnum.GuiScale, "options.guiScale", false, false);
            RENDER_CLOUDS = new EnumOptions(OptionsEnum.RenderClouds, "options.renderClouds", false, true);
            PARTICLES = new EnumOptions(OptionsEnum.Particles, "options.particles", false, false);
            CHAT_VISIBILITY = new EnumOptions(OptionsEnum.ChatVisibility, "options.chat.visibility", false, false);
            CHAT_COLOR = new EnumOptions(OptionsEnum.ChatColor, "options.chat.color", false, true);
            CHAT_LINKS = new EnumOptions(OptionsEnum.ChatLinks, "options.chat.links", false, true);
            CHAT_OPACITY = new EnumOptions(OptionsEnum.ChatOpacity, "options.chat.opacity", true, false);
            CHAT_LINKS_PROMPT = new EnumOptions(OptionsEnum.ChatLinksPrompt, "options.chat.links.prompt", false, true);
            USE_SERVER_TEXTURES = new EnumOptions(OptionsEnum.UseServerTextures, "options.serverTextures", false, true);
            SNOOPER_ENABLED = new EnumOptions(OptionsEnum.SnooperEnabled, "options.snooper", false, true);
            USE_FULLSCREEN = new EnumOptions(OptionsEnum.UseFullscreen, "options.fullscreen", false, true);
            ENABLE_VSYNC = new EnumOptions(OptionsEnum.EnableVSync, "options.vsync", false, true);
            SHOW_CAPE = new EnumOptions(OptionsEnum.ShowCape, "options.showCape", false, true);
            TOUCHSCREEN = new EnumOptions(OptionsEnum.TouchScreen, "options.touchscreen", false, true);
            CHAT_SCALE = new EnumOptions(OptionsEnum.ChatScale, "options.chat.scale", true, false);
            CHAT_WIDTH = new EnumOptions(OptionsEnum.ChatWidth, "options.chat.width", true, false);
            CHAT_HEIGHT_FOCUSED = new EnumOptions(OptionsEnum.ChatHeightFocused, "options.chat.height.focused", true, false);
            CHAT_HEIGHT_UNFOCUSED = new EnumOptions(OptionsEnum.ChatHeightUnfocused, "options.chat.height.unfocused", true, false);

            Values = new[]
                         {
                             MUSIC, SOUND, INVERT_MOUSE, SENSITIVITY, FOV, GAMMA, RENDER_DISTANCE, VIEW_BOBBING,
                             ANAGLYPH, ADVANCED_OPENGL, FRAMERATE_LIMIT, DIFFICULTY, GRAPHICS, AMBIENT_OCCLUSION,
                             GUI_SCALE, RENDER_CLOUDS, PARTICLES, CHAT_VISIBILITY, CHAT_COLOR, CHAT_LINKS, CHAT_OPACITY,
                             CHAT_LINKS_PROMPT, USE_SERVER_TEXTURES, SNOOPER_ENABLED, USE_FULLSCREEN, ENABLE_VSYNC,
                             SHOW_CAPE, TOUCHSCREEN, CHAT_SCALE, CHAT_WIDTH, CHAT_HEIGHT_FOCUSED, CHAT_HEIGHT_UNFOCUSED
                         };
        }

        internal EnumOptions(OptionsEnum type, string name, bool unknown, bool unknown2)
        {
            this.Enum = type;
        }

        internal enum OptionsEnum
        {
            Music,
            Sound,
            InvertMouse,
            Sensitivity,
            FieldOfView,
            Gamma,
            RenderDistance,
            ViewBobbing,
            Anaglyph,
            AdvancedOpenGL,
            FramerateLimit,
            Difficulty,
            Graphics,
            AmbientOcclusion,
            GuiScale,
            RenderClouds,
            Particles,
            ChatVisibility,
            ChatColor,
            ChatLinks,
            ChatOpacity,
            ChatLinksPrompt,
            UseServerTextures,
            SnooperEnabled,
            UseFullscreen,
            EnableVSync,
            ShowCape,
            TouchScreen,
            ChatScale,
            ChatWidth,
            ChatHeightFocused,
            ChatHeightUnfocused
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        // ReSharper disable InconsistentNaming
        public static EnumOptions OPENING { get; private set; }
        public static EnumOptions MUSIC { get; private set; }
        public static EnumOptions SOUND { get; private set; }
        public static EnumOptions INVERT_MOUSE { get; private set; }
        public static EnumOptions SENSITIVITY { get; private set; }
        public static EnumOptions FOV { get; private set; }
        public static EnumOptions GAMMA { get; private set; }
        public static EnumOptions RENDER_DISTANCE { get; private set; }
        public static EnumOptions VIEW_BOBBING { get; private set; }
        public static EnumOptions ANAGLYPH { get; private set; }
        public static EnumOptions ADVANCED_OPENGL { get; private set; }
        public static EnumOptions FRAMERATE_LIMIT { get; private set; }
        public static EnumOptions DIFFICULTY { get; private set; }
        public static EnumOptions GRAPHICS { get; private set; }
        public static EnumOptions AMBIENT_OCCLUSION { get; private set; }
        public static EnumOptions GUI_SCALE { get; private set; }
        public static EnumOptions RENDER_CLOUDS { get; private set; }
        public static EnumOptions PARTICLES { get; private set; }
        public static EnumOptions CHAT_VISIBILITY { get; private set; }
        public static EnumOptions CHAT_COLOR { get; private set; }
        public static EnumOptions CHAT_LINKS { get; private set; }
        public static EnumOptions CHAT_OPACITY { get; private set; }
        public static EnumOptions CHAT_LINKS_PROMPT { get; private set; }
        public static EnumOptions USE_SERVER_TEXTURES { get; private set; }
        public static EnumOptions SNOOPER_ENABLED { get; private set; }
        public static EnumOptions USE_FULLSCREEN { get; private set; }
        public static EnumOptions ENABLE_VSYNC { get; private set; }
        public static EnumOptions SHOW_CAPE { get; private set; }
        public static EnumOptions TOUCHSCREEN { get; private set; }
        public static EnumOptions CHAT_SCALE { get; private set; }
        public static EnumOptions CHAT_WIDTH { get; private set; }
        public static EnumOptions CHAT_HEIGHT_FOCUSED { get; private set; }
        public static EnumOptions CHAT_HEIGHT_UNFOCUSED { get; private set; }
        // ReSharper restore InconsistentNaming

        public static EnumOptions[] Values { get; private set; }
        public static int[] doorEnum { get; private set; }

        public override bool Equals(object obj)
        {
            var typed = obj as EnumOptions;
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
        internal OptionsEnum Enum { get; private set; }

        public string GetName()
        {
            return this.Enum.ToString();
        }

        public int GetID()
        {
            return (int)this.Enum;
        }

        public string GetEnumString()
        {
            throw new System.NotImplementedException();
        }

        public int Ordinal()
        {
            throw new System.NotImplementedException();
        }

        public bool GetEnumFloat()
        {
            throw new System.NotImplementedException();
        }

        public bool GetEnumBoolean()
        {
            throw new System.NotImplementedException();
        }

        public static EnumOptions GetEnumOptions(int id)
        {
            throw new System.NotImplementedException();
        }

        public int ReturnEnumOrdinal()
        {
            throw new System.NotImplementedException();
        }
    }
}
