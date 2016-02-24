namespace SMC.SourceTranslation.Logic
{
    using System;
    using System.IO;

    using CarbonCore.GrammarParser;
    using CarbonCore.GrammarParser.Grammars;
    using CarbonCore.GrammarParser.Tokenize;

    using SMC.SourceLib.Intermediate;

    public partial class Translation
    {
        private JavaTermKey GetTermKey(Token token)
        {
            if (token.Term == null || token.Term.Tag == null || token.Term.Tag.GetType() != typeof(JavaTermKey))
            {
                throw new ArgumentException("Invalid token: " + token);
            }

            return (JavaTermKey)token.Term.Tag;
        }

        private TempAttribute? GetIntermediateAttribute(JavaTermKey attribute)
        {
            switch (attribute)
            {
                case JavaTermKey.Public:
                    {
                        return TempAttribute.Public;
                    }

                case JavaTermKey.Protected:
                    {
                        return TempAttribute.Protected;
                    }

                case JavaTermKey.Private:
                    {
                        return TempAttribute.Private;
                    }

                case JavaTermKey.Final:
                    {
                        return TempAttribute.Final;
                    }

                case JavaTermKey.Abstract:
                    {
                        return TempAttribute.Abstract;
                    }

                case JavaTermKey.Static:
                    {
                        return TempAttribute.Static;
                    }

                case JavaTermKey.Volatile:
                    {
                        return TempAttribute.Volatile;
                    }

                case JavaTermKey.Transient:
                    {
                        return TempAttribute.Transient;
                    }

                case JavaTermKey.Synchronized:
                    {
                        return TempAttribute.Synchronized;
                    }
            }

            return null;
        }

        private void HandleBracketForClassContent(TempClassContent target, Token token)
        {
            if (string.IsNullOrEmpty(target.ReturnType) && string.IsNullOrEmpty(target.Name))
            {
                throw new InvalidDataException("Got brackets without type");
            }

            // Move the name over to type since this cannot be a name
            if (string.IsNullOrEmpty(target.ReturnType))
            {
                target.ReturnType = target.Name;
                target.Name = null;
            }

            target.ReturnType += token.Contents;
        }

        private bool TranslateTemplateArgument(TranslationData data, out string argumentString)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            argumentString = string.Empty;
            while (iterator.Next(out token))
            {
                var segment = new TempDataSegment();
                
                switch (token.Term.Type)
                {
                    case TermType.Identifier:
                        {
                            argumentString += token.Contents;
                            break;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            segment.Key = (int)key;
                            switch (key)
                            {
                                case JavaTermKey.Dot:
                                    {
                                        argumentString += token.Contents;
                                        break;
                                    }

                                case JavaTermKey.Greater:
                                    {
                                        return true;
                                    }

                                default:
                                    {
                                        throw new InvalidDataException("Unpexpected token in Template Argument: " + token.Contents);
                                    }
                            }

                            break;
                        }
                }
            }

            return false;
        }

        private void HandleIdentifierForClassContent(TempClassContent target, Token token, bool inThrows)
        {
            if (inThrows)
            {
                target.AddThrow(token.Contents);
                return;
            }

            bool hasReturnValue = !string.IsNullOrEmpty(target.ReturnType);

            if (!string.IsNullOrEmpty(target.Name) && hasReturnValue)
            {
                throw new InvalidDataException("Name already set, unknown token at this point");
            }

            // Check if we are within a qualified type declaration
            if (hasReturnValue && target.ReturnType.EndsWith("."))
            {
                target.ReturnType += token.Contents;
                return;
            }

            // Move the name over to return type
            if (!string.IsNullOrEmpty(target.Name))
            {
                target.ReturnType = target.Name;
            }

            target.Name = token.Contents;
        }

        private void HandleQualifiedTypeForClassContent(TempClassContent target, Token token)
        {
            bool hasReturnValue = !string.IsNullOrEmpty(target.ReturnType);
            bool hasName = !string.IsNullOrEmpty(target.Name);

            if (hasName && hasReturnValue)
            {
                throw new InvalidDataException("Name already set, unknown token at this point");
            }

            if (!hasReturnValue)
            {
                // Move the name over since we are starting a qualified type
                if (hasName)
                {
                    target.ReturnType = target.Name;
                    target.Name = null;
                }
                else
                {
                    throw new InvalidDataException("Qualified type was expected");
                }
            }

            target.ReturnType += token.Contents;
        }

        private void TranslateBraceContent(TranslationData data, TempDataBlock dataBlock)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                var segment = new TempDataSegment();

                switch (token.Term.Type)
                {
                    case TermType.IdentifierKey:
                        {
                            segment.Key = (int)this.GetTermKey(token);
                            break;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            segment.Key = (int)key;
                            switch (key)
                            {
                                case JavaTermKey.BraceLeft:
                                    {
                                        segment.Key = null;
                                        segment.BlockContents = new TempDataBlock();
                                        this.TranslateBraceContent(data, segment.BlockContents);
                                        dataBlock.AddContent(segment);
                                        continue;
                                    }

                                case JavaTermKey.BraceRight:
                                    {
                                        return;
                                    }
                            }

                            break;
                        }
                }

                // Only need the content if we don't know what this token is
                if (segment.Key == null)
                {
                    segment.Contents = token.Contents;
                }

                dataBlock.AddContent(segment);
            }
        }

        private void TranslateStatementContent(TranslationData data, TempDataBlock dataBlock)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                var segment = new TempDataSegment();

                switch (token.Term.Type)
                {
                    case TermType.IdentifierKey:
                        {
                            segment.Key = (int)this.GetTermKey(token);
                            break;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            segment.Key = (int)key;
                            switch (key)
                            {
                                case JavaTermKey.Semicolon:
                                    {
                                        return;
                                    }
                            }
                            
                            break;
                        }
                }

                if (segment.Key == null)
                {
                    segment.Contents = token.Contents;
                }

                dataBlock.AddContent(segment);
            }
        }

        private void TranslateParenthesisContent(TranslationData data, TempDataBlock dataBlock)
        {
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            int level = 0;
            while (iterator.Next(out token))
            {
                var segment = new TempDataSegment();

                switch (token.Term.Type)
                {
                    case TermType.IdentifierKey:
                        {
                            segment.Key = (int)this.GetTermKey(token);
                            break;
                        }

                    case TermType.Key:
                        {
                            JavaTermKey key = this.GetTermKey(token);
                            segment.Key = (int)key;
                            switch (key)
                            {
                                case JavaTermKey.ParenthesisLeft:
                                    {
                                        level++;
                                        break;
                                    }

                                case JavaTermKey.ParenthesisRight:
                                    {
                                        level--;
                                        if (level <= 0)
                                        {
                                            return;
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                }

                if (segment.Key == null)
                {
                    segment.Contents = token.Contents;
                }

                dataBlock.AddContent(segment);
            }
        }
    }
}
