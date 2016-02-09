namespace SMC.SourceTranslation.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using CarbonCore.GrammarParser;
    using CarbonCore.GrammarParser.Grammars;
    using CarbonCore.GrammarParser.Tokenize;

    using SMC.SourceLib.Intermediate;

    public partial class Translation
    {
        private bool TranslateTempClass(TranslationData data, TempClass classData)
        {
            bool declaringImplements = false;
            bool declaringExtends = false;
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                switch (token.Term.Type)
                {
                    case TermType.Identifier:
                        {
                            if (declaringImplements)
                            {
                                classData.AddInherit(token.Contents);
                                continue;
                            }

                            if (declaringExtends)
                            {
                                classData.AddExtend(token.Contents);
                                continue;
                            }

                            if (!string.IsNullOrEmpty(classData.Name))
                            {
                                // The class declaration does not add up, proably is not a class
                                return false;
                            }

                            classData.Name = token.Contents;
                            continue;
                        }

                    case TermType.IdentifierKey:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.Implements:
                                    {
                                        declaringImplements = true;
                                        continue;
                                    }

                                case JavaTermKey.Extends:
                                    {
                                        declaringExtends = true;
                                        continue;
                                    }

                                case JavaTermKey.Class:
                                    {
                                        classData.ClassType = TempClassType.Class;
                                        continue;
                                    }

                                case JavaTermKey.Enum:
                                    {
                                        classData.ClassType = TempClassType.Enum;
                                        continue;
                                    }

                                case JavaTermKey.Interface:
                                    {
                                        classData.ClassType = TempClassType.Interface;
                                        continue;
                                    }

                                default:
                                    {
                                        TempAttribute? attribute = this.GetIntermediateAttribute(key);
                                        if (attribute != null)
                                        {
                                            classData.AddAttribute((int)attribute);
                                            continue;
                                        }

                                        break;
                                    }
                            }

                            break;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.BraceLeft:
                                    {
                                        this.TranslateTempClassContent(data, classData);
                                        data.ProjectData.AddStat(TempProjectStat.Classes);
                                        data.FileEntry.AddClass(classData.Name);
                                        return true;
                                    }

                                case JavaTermKey.BraceRight:
                                    {
                                        throw new InvalidDataException("Brace found in class scope");
                                    }

                                case JavaTermKey.Comma:
                                    {
                                        if (!declaringImplements && !declaringExtends)
                                        {
                                            throw new InvalidDataException("Comma found but not within declaring!");
                                        }

                                        continue;
                                    }

                                case JavaTermKey.Semicolon:
                                    {
                                        // We just ignore extra semicolons for the moment, they usually appear at the end of {} declarations
                                        continue;
                                    }
                            }

                            break;
                        }
                }

                // The class token does not match up, not a class or unknown format
                return false;
            }

            return false;
        }

        private void TranslateTempClassContent(TranslationData data, TempClass classData)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            var pendingAttributes = new List<TempFunctionAttribute>();
            while (iterator.Next(out token))
            {
                if (token.Term.Type == TermType.Key && this.GetTermKey(token) == JavaTermKey.BraceRight)
                {
                    // We are dropping out of the content scope
                    return;
                }

                data.CurrentToken--;

                switch (classData.ClassType)
                {
                    case TempClassType.Class:
                    case TempClassType.Enum:
                        {
                            int startOffset = data.CurrentToken;

                            // See if we are dealing with a function Attribute
                            var functionAttribute = new TempFunctionAttribute();
                            data.TranslationStack.Push(functionAttribute);
                            if (this.TranslateTempFunctionAttribute(data, functionAttribute))
                            {
                                data.TranslationStack.Pop();
                                pendingAttributes.Add(functionAttribute);
                                continue;
                            }

                            // Rewind
                            data.TranslationStack.Pop();
                            data.CurrentToken = startOffset;

                            // See if we have a function
                            var function = new TempFunction();
                            data.TranslationStack.Push(function);
                            if (this.TranslateTempFunction(data, function))
                            {
                                data.TranslationStack.Pop();
                                classData.AddFunction(function);
                                foreach (TempFunctionAttribute attribute in pendingAttributes)
                                {
                                    function.AddFunctionAttribute(attribute);
                                }

                                pendingAttributes.Clear();
                                data.ProjectData.AddStat(TempProjectStat.Functions);
                                continue;
                            }

                            // Rewind
                            data.TranslationStack.Pop();
                            data.CurrentToken = startOffset;

                            // See if we have an inline class or enum
                            var inlineClass = new TempClass();
                            data.TranslationStack.Push(inlineClass);
                            if (TranslateTempClass(data, inlineClass))
                            {
                                data.TranslationStack.Pop();
                                classData.AddClass(inlineClass);
                                data.ProjectData.AddStat(TempProjectStat.Classes);
                                continue;
                            }

                            // Rewind
                            data.TranslationStack.Pop();
                            data.CurrentToken = startOffset;

                            // We are probably dealing with a declaration by this point
                            var declaration = new TempDeclaration();
                            data.TranslationStack.Push(declaration);
                            if (this.TranslateTempDeclaration(data, declaration))
                            {
                                data.TranslationStack.Pop();
                                classData.AddDeclaration(declaration);
                                data.ProjectData.AddStat(TempProjectStat.Members);
                                continue;
                            }

                            throw new InvalidDataException("Unhandled class content");
                        }

                    case TempClassType.Interface:
                        {
                            var declaration = new TempDeclaration();
                            data.TranslationStack.Push(declaration);
                            if (this.TranslateTempDeclaration(data, declaration))
                            {
                                continue;
                            }

                            break;
                        }

                    default:
                        throw new NotImplementedException(classData.ClassType.ToString());
                }
            }
        }

        private bool TranslateTempFunctionAttribute(TranslationData data, TempFunctionAttribute attributeData)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            bool confirmedAttribute = false;
            while (iterator.Next(out token))
            {
                switch (token.Term.Type)
                {
                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.At:
                                    {
                                        confirmedAttribute = true;
                                        continue;
                                    }

                                case JavaTermKey.ParenthesisLeft:
                                    {
                                        if (!confirmedAttribute)
                                        {
                                            return false;
                                        }

                                        attributeData.Arguments = new TempDataBlock();
                                        this.TranslateParenthesisContent(data, attributeData.Arguments);
                                        return true;
                                    }
                            }

                            break;
                        }

                    case TermType.Identifier:
                        {
                            if (!confirmedAttribute)
                            {
                                return false;
                            }

                            attributeData.Name = token.Contents;
                            continue;
                        }
                }

                // We got a token that we did not expect
                if (!string.IsNullOrEmpty(attributeData.Name))
                {
                    // We are done with the attribute, no extra parameters
                    return true;
                }

                return false;
            }

            throw new InvalidOperationException("should not drop out here");
        }

        private bool TranslateTempDeclaration(TranslationData data, TempDeclaration declarationData)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            bool inThrows = false;
            while (iterator.Next(out token))
            {
                switch (token.Term.Type)
                {
                    case TermType.IdentifierKey:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.Throws:
                                    {
                                        inThrows = true;
                                        continue;
                                    }

                                default:
                                    {
                                        TempAttribute? attribute = this.GetIntermediateAttribute(key);
                                        if (attribute != null)
                                        {
                                            declarationData.AddAttribute((int)attribute);
                                            continue;
                                        }

                                        break;
                                    }
                            }

                            if (!string.IsNullOrEmpty(declarationData.ReturnType))
                            {
                                throw new InvalidDataException("Identifier key found but return was set already");
                            }
                            
                            declarationData.ReturnType = token.Contents;
                            continue;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);

                            switch (key)
                            {
                                case JavaTermKey.Semicolon:
                                    {
                                        return true;
                                    }

                                case JavaTermKey.Equal:
                                    {
                                        declarationData.Contents = new TempDataBlock();
                                        this.TranslateStatementContent(data, declarationData.Contents);
                                        return true;
                                    }

                                case JavaTermKey.ParenthesisLeft:
                                    {
                                        declarationData.IsFunction = true;
                                        declarationData.Arguments = new TempDataBlock();
                                        this.TranslateParenthesisContent(data, declarationData.Arguments);
                                        continue;
                                    }

                                case JavaTermKey.BraceRight:
                                    {
                                        // We are leaving the scope, exit
                                        data.CurrentToken--;
                                        return false;
                                    }

                                case JavaTermKey.BracketLeft:
                                case JavaTermKey.BracketRight:
                                    {
                                        HandleBracketForClassContent(declarationData, token);
                                        continue;
                                    }

                                case JavaTermKey.Dot:
                                    {
                                        this.HandleQualifiedTypeForClassContent(declarationData, token);
                                        continue;
                                    }

                                case JavaTermKey.Comma:
                                    {
                                        if (inThrows)
                                        {
                                            continue;
                                        }

                                        // Fall through to the exception, comma was not expected here
                                        break;
                                    }
                            }

                            throw new InvalidDataException("Unhandled key: " + token);
                        }

                    case TermType.Identifier:
                        {
                            HandleIdentifierForClassContent(declarationData, token, inThrows);
                            continue;
                        }
                }
            }

            throw new InvalidOperationException("should not drop out here");
        }
    }
}
