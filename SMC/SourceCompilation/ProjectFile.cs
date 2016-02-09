namespace SMC.SourceCompilation
{
    using System.Xml;
    
    using CarbonCore.Utils.IO;

    using SMC.SourceCompilation.Contracts;

    public class ProjectFile : IProjectFile
    {
        private readonly XmlDocument project;

        private XmlNode projectReferenceParent;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ProjectFile(CarbonFile template)
        {
            this.project = new XmlDocument();
            this.project.Load(template.GetPath());

            this.PrepareProject();
        }

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public void AddInclude(CarbonFile file)
        {
            System.Diagnostics.Trace.Assert(this.project.DocumentElement != null);

            XmlNode node = this.project.CreateElement("Compile", this.project.DocumentElement.NamespaceURI);
            System.Diagnostics.Trace.Assert(node.Attributes != null);

            XmlAttribute attribute = this.project.CreateAttribute("Include");
            attribute.Value = file.GetPath();
            node.Attributes.Append(attribute);

            this.projectReferenceParent.AppendChild(node);
        }

        public void Save(CarbonFile targetFile)
        {
            this.project.Save(targetFile.GetPath());
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void PrepareProject()
        {
            XmlNode childMatch = null;
            XmlNodeList nodes = this.project.GetElementsByTagName("ItemGroup");
            foreach (XmlNode node in nodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Attributes == null)
                    {
                        continue;
                    }

                    if (child.Attributes["Include"].Value.Equals("Entry" + CarbonCore.Utils.Constants.ExtensionCSharp))
                    {
                        childMatch = child;
                        break;
                    }
                }

                if (childMatch != null)
                {
                    break;
                }
            }

            if (childMatch != null)
            {
                this.projectReferenceParent = childMatch.ParentNode;
            }
        }
    }
}
