namespace AddonCompiler.Logic
{
    using System.Xml;

    using CarbonCore.Utils.IO;

    public static class AddonXmlUtils
    {
        private static readonly XmlReaderSettings XmlSettings = new XmlReaderSettings { NameTable = new NameTable() };
        private static readonly XmlNamespaceManager XmlNamespace = new XmlNamespaceManager(XmlSettings.NameTable);

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static AddonXmlUtils()
        {
            XmlNamespace.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        }

        public static XmlDocument ReadAddonXml(CarbonFile file)
        {
            var document = new XmlDocument();

            XmlParserContext context = new XmlParserContext(null, XmlNamespace, string.Empty, XmlSpace.Default);
            XmlReader reader = XmlReader.Create(file.GetPath(), XmlSettings, context);

            document.Load(reader);
            return document;
        }
    }
}
