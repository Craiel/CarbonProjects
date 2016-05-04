namespace SMC.SourceCompilation.Logic
{
    using System;
    using System.Collections.Generic;
    using SMC.SourceLib.Intermediate;
    using SMC.SourceLib.Sharp;

    public partial class Compiler
    {
        private static readonly IList<TempAttribute> ClassAttributeOrder = new List<TempAttribute>
        {
            TempAttribute.Private, TempAttribute.Public, TempAttribute.Protected,
            TempAttribute.Abstract, TempAttribute.Static
        };

        private SharpClass CompileClass(CompilationData data, TempClass @class)
        {
            var result = new SharpClass { Name = @class.Name };

            switch (@class.ClassType)
            {
                case TempClassType.Interface:
                    {
                        result.IsInterface = true;
                        break;
                    }

                case TempClassType.Class:
                    {
                        break;
                    }

                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            // Get the attributes from the intermediate
            IList<TempAttribute> attributes = this.GetAttributes(@class.Attributes);

            // Ensure the class has an accessor, public is default
            if (!attributes.Contains(TempAttribute.Private) && !attributes.Contains(TempAttribute.Protected)
                && !attributes.Contains(TempAttribute.Public))
            {
                attributes.Add(TempAttribute.Public);
            }

            // Compile the info and add it to the result
            result.Attributes = this.CompileClassAttributeString(attributes);
            if (@class.Extends != null)
            {
                result.Inheritance = this.CompileClassInheritanceString(data, @class.Extends);
            }

            if (@class.Inherits != null)
            {
                result.Interfaces = this.CompileClassInheritanceString(data, @class.Inherits);
            }

            return result;
        }

        private string CompileClassInheritanceString(CompilationData data, IList<string> types)
        {
            return string.Join(", ", types);
        }

        private string CompileClassAttributeString(IList<TempAttribute> attributes)
        {
            IList<string> attributeList = new List<string>();

            foreach (TempAttribute attribute in ClassAttributeOrder)
            {
                if (attributes.Contains(attribute))
                {
                    System.Diagnostics.Debug.Assert(SharpData.AttributeDictionary.ContainsKey(attribute), "Attribute in order list but not defined in dictionary");
                    attributeList.Add(SharpData.AttributeDictionary[attribute]);
                }
            }

            if (attributeList.Count <= 0)
            {
                return null;
            }

            return string.Join(" ", attributeList);
        }
    }
}
