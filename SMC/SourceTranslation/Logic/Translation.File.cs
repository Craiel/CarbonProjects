namespace SMC.SourceTranslation.Logic
{
    using System.IO;
    using System.Text;

    using CarbonCore.GrammarParser;
    using CarbonCore.GrammarParser.Grammars;
    using CarbonCore.GrammarParser.Tokenize;

    using SMC.SourceLib.Intermediate;

    public partial class Translation
    {
        private void TranslateTempFile(TranslationData data, TempFileFull fileData)
        {
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
                                case JavaTermKey.Package:
                                    {
                                        this.TranslateTempFilePackage(data, fileData);
                                        continue;
                                    }

                                case JavaTermKey.Import:
                                    {
                                        this.TranslateTempFileImport(data, fileData);
                                        continue;
                                    }

                                default:
                                    {
                                        // Rewind to current token and read the possible section
                                        data.CurrentToken--;
                                        this.TranslateTempFileContent(data, fileData);
                                        return;
                                    }
                            }
                        }
                }

                throw new InvalidDataException("Unexpected token: " + token);
            }
        }

        private void TranslateTempFilePackage(TranslationData data, TempFileFull fileData)
        {
            if (!string.IsNullOrEmpty(fileData.Namespace))
            {
                throw new InvalidDataException("Possibly duplicate namespace!");
            }

            var builder = new StringBuilder();
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                if (token.Term.Type == TermType.Key)
                {
                    JavaTermKey key = this.GetTermKey(token);
                    if (key == JavaTermKey.Semicolon)
                    {
                        fileData.Namespace = builder.ToString();
                        return;
                    }
                }

                builder.Append(token.Contents);
            }
        }

        private void TranslateTempFileImport(TranslationData data, TempFileFull fileData)
        {
            var builder = new StringBuilder();
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            while (iterator.Next(out token))
            {
                if (token.Term.Type == TermType.Key)
                {
                    JavaTermKey key = this.GetTermKey(token);
                    if (key == JavaTermKey.Semicolon)
                    {
                        fileData.AddInclude(builder.ToString());
                        return;
                    }
                }

                builder.Append(token.Contents);
            }
        }

        private void TranslateTempFileContent(TranslationData data, TempFileFull fileData)
        {
            int tokenStart = data.CurrentToken;
            TranslationDataIterator iterator = data.GetIterator();
            Token token;
            TempClass contentData = null;
            while (iterator.Next(out token))
            {
                if (token.Term.Type != TermType.IdentifierKey && token.Term.Type != TermType.Key)
                {
                    continue;
                }

                JavaTermKey key = this.GetTermKey(token);
                switch (key)
                {
                    case JavaTermKey.Class:
                    case JavaTermKey.Interface:
                    case JavaTermKey.Enum:
                        {
                            contentData = new TempClass();
                            break;
                        }

                    case JavaTermKey.BraceRight:
                        {
                            return;
                        }
                }

                if (contentData != null)
                {
                    break;
                }
            }

            if (contentData != null)
            {
                data.CurrentToken = tokenStart;
                data.TranslationStack.Push(contentData);
                fileData.AddClass(contentData);
                return;
            }

            throw new InvalidDataException("Unexpected token list: ");
        }
    }
}
