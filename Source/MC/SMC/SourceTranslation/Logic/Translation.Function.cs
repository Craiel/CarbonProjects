namespace SMC.SourceTranslation.Logic
{
    using System;
    using System.IO;

    using CarbonCore.GrammarParser;
    using CarbonCore.GrammarParser.Grammars;
    using CarbonCore.GrammarParser.Tokenize;
    using CarbonCore.Utils.Diagnostics;

    using SMC.SourceLib.Intermediate;

    public partial class Translation
    {
        private bool TranslateTempFunction(TranslationData data, TempFunction functionData)
        {
            bool inThrows = false;
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                switch (token.Term.Type)
                {
                    case TermType.IdentifierKey:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.Void:
                                    {
                                        // Return type stays null
                                        continue;
                                    }

                                case JavaTermKey.Throws:
                                    {
                                        inThrows = true;
                                        continue;
                                    }

                                case JavaTermKey.Enum:
                                case JavaTermKey.Class:
                                    {
                                        // We are declaring a class within a function
                                        System.Diagnostics.Trace.TraceWarning("{0} in class, this needs testing. ({1}", key, data.SourceFile);
                                        return false;
                                    }

                                default:
                                    {
                                        TempAttribute? attribute = this.GetIntermediateAttribute(key);
                                        if (attribute != null)
                                        {
                                            functionData.AddAttribute((int)attribute);
                                            continue;
                                        }

                                        break;
                                    }
                            }

                            if (!string.IsNullOrEmpty(functionData.ReturnType))
                            {
                                throw new InvalidDataException("Identifier key found but return was set already");
                            }

                            functionData.ReturnType = token.Contents;
                            continue;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            switch (key)
                            {
                                case JavaTermKey.ParenthesisLeft:
                                    {
                                        functionData.Arguments = new TempDataBlock();
                                        this.TranslateParenthesisContent(data, functionData.Arguments);
                                        continue;
                                    }

                                case JavaTermKey.BraceLeft:
                                    {
                                        functionData.Contents = new TempDataBlock();
                                        this.TranslateBraceContent(data, functionData.Contents);
                                        return true;
                                    }

                                case JavaTermKey.BraceRight:
                                    {
                                        // We ran out of the function content scope
                                        return true;
                                    }

                                case JavaTermKey.Equal:
                                case JavaTermKey.Semicolon:
                                    {
                                        // This is not a function, rewind and return
                                        return false;
                                    }

                                case JavaTermKey.Dot:
                                    {
                                        this.HandleQualifiedTypeForClassContent(functionData, token);
                                        continue;
                                    }

                                case JavaTermKey.Comma:
                                    {
                                        if (!inThrows)
                                        {
                                            Diagnostic.Warning("Comma outside of throws, probably Enum definition");
                                            return true;
                                        }

                                        continue;
                                    }

                                case JavaTermKey.BracketLeft:
                                case JavaTermKey.BracketRight:
                                    {
                                        this.HandleBracketForClassContent(functionData, token);
                                        continue;
                                    }

                                case JavaTermKey.Less:
                                    {
                                        string templateArgument;
                                        if (this.TranslateTemplateArgument(data, out templateArgument))
                                        {
                                            functionData.TemplateArgument = templateArgument;
                                        }
                                        else
                                        {
                                            throw new InvalidDataException("Tried template argument but failed");
                                        }

                                        continue;
                                    }
                            }

                            break;
                        }

                    case TermType.Identifier:
                        {
                            this.HandleIdentifierForClassContent(functionData, token, inThrows);
                            continue;
                        }
                }

                throw new InvalidDataException("Unexpected token: " + token);
            }

            throw new InvalidOperationException("should not drop out here");
        }
    }
}
