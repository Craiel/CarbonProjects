namespace SMC.SourceTranslation.Logic
{
    using System;

    using CarbonCore.GrammarParser;
    using CarbonCore.GrammarParser.Tokenize;

    using SMC.SourceLib.Intermediate;

    public sealed class TranslationDataIterator : IDisposable
    {
        private readonly TranslationData data;

        public TranslationDataIterator(TranslationData data)
        {
            this.data = data;
        }

        public void Dispose()
        {
        }

        public bool Next(out Token target)
        {
            while (this.data.CurrentToken < this.data.Tokens.Count)
            {
                Token token = this.data.Tokens[this.data.CurrentToken++];
                if (token.Term == null)
                {
                    var entry = new TempUnknown { Content = token.Contents };
                    this.data.TranslationStack.Peek().AddUnknown(entry);
                    continue;
                }

                if (token.Term.Type == TermType.Comment)
                {
                    var entry = new TempComment { Content = token.Contents };
                    this.data.TranslationStack.Peek().AddComment(entry);
                    continue;
                }

                target = token;
                return true;
            }

            throw new IndexOutOfRangeException("Reached end of tokens");
        }
    }
}
