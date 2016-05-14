namespace AddonCompiler
{
    public static class Constants
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public const string ConfigFileName = "AddonCompiler.General.config";

        public const string ExtensionToc = ".toc";
        public const string ExtensionXml = ".xml";
        public const string ExtensionLua = ".lua";
        public const string ExtensionTga = ".tga";
        public const string ExtensionBlp = ".blp";
        public const string ExtensionTtf = ".ttf";
        public const string ExtensionOgg = ".ogg";

        public const string XmlNodeScript = "Script";
        public const string XmlNodeInclude = "Include";
        public const string XmlScriptFileAttribute = "file";
        public const string XmlIncludeFileAttribute = "file";

        public const string MetaDependencies = "Dependencies";

        public const string BlizzardAddonPrefix = "Blizzard_";

        public static readonly string[] ResourceExtensions = { ExtensionTga, ExtensionBlp, ExtensionTtf, ExtensionOgg };
    }
}
