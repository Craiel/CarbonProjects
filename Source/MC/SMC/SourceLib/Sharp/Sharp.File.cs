namespace SMC.SourceLib.Sharp
{
    using System.Collections.Generic;
    using System.Text;

    using CarbonCore.Utils;

    public class SharpFile
    {
        private readonly IList<string> usings;

        private readonly IList<SharpClass> classes;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SharpFile()
        {
            this.usings = new List<string>();
            this.classes = new List<SharpClass>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string NameSpace { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormatLine("namespace {0}", this.NameSpace ?? "undefined");
            builder.AppendLine("{");
            foreach (string @using in this.usings)
            {
                builder.AppendFormatLine("{0}using {1};", SourceLib.Constants.SharpIndentation, @using);
            }

            if (this.usings.Count > 0)
            {
                // Add a space after the using section if we had any
                builder.AppendLine();
            }

            foreach (SharpClass @class in this.classes)
            {
                @class.ToString(builder, 1);
            }

            builder.AppendLine("}");

            return builder.ToString();
        }

        public void AddClass(SharpClass @class)
        {
            this.classes.Add(@class);
        }
    }
}
