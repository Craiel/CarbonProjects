namespace SMC.SourceLib.Sharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SharpClass
    {
        private readonly IList<string> declarations;

        private readonly IList<SharpMethod> methods;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SharpClass()
        {
            this.declarations = new List<string>();
            this.methods = new ArraySegment<SharpMethod>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool IsInterface { get; set; }

        public string Attributes { get; set; }
        public string Name { get; set; }
        public string Inheritance { get; set; }
        public string Interfaces { get; set; }

        public void ToString(StringBuilder target, int level)
        {
            string classString = this.BuildClassString();

            string indentation = Constants.GetSharpIndentationString(level);
            target.AppendLine(indentation + classString);
            target.AppendLine(indentation + "{");
            target.AppendLine(indentation + "}");
        }

        private string BuildClassString()
        {
            var classString = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Attributes))
            {
                classString.AppendFormat("{0} ", this.Attributes);
            }

            classString.AppendFormat("{0} {1}", this.IsInterface ? "interface" : "class", this.Name);

            bool hasInheritance = !string.IsNullOrEmpty(this.Inheritance);
            bool hasInterfaces = !string.IsNullOrEmpty(this.Interfaces);
            if (hasInheritance || hasInterfaces)
            {
                classString.Append(" : ");
                if (hasInheritance)
                {
                    classString.Append(this.Inheritance);
                    if(hasInterfaces)
                    {
                        classString.Append(", ");
                    }
                }

                if (hasInterfaces)
                {
                    classString.Append(this.Interfaces);
                }
            }

            return classString.ToString();
        }
    }
}
