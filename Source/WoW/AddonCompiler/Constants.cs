namespace AddonCompiler
{
    public static class Constants
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public const string ConfigFileName = "AddonCompiler.General.config";

        public const string ExtensionToc = ".toc";
        public const string ExtensionXML = ".xml";
        public const string ExtensionLUA = ".lua";
        public const string ExtensionTGA = ".tga";
        public const string ExtensionBLP = ".blp";
        public const string ExtensionTTF = ".ttf";
        public const string ExtensionOGG = ".ogg";

        public const string XmlNodeScript = "Script";
        public const string XmlNodeInclude = "Include";
        public const string XmlScriptFileAttribute = "file";
        public const string XmlIncludeFileAttribute = "file";

        public const string MetaDependencies = "Dependencies";

        public const string BlizzardAddonPrefix = "Blizzard_";

        public static readonly string[] ResourceExtensions = { ExtensionTGA, ExtensionBLP, ExtensionTTF, ExtensionOGG };
    }
}
