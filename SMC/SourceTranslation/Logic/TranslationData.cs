namespace SMC.SourceTranslation.Logic
{
    using System.Collections.Generic;

    using CarbonCore.GrammarParser.Tokenize;
    using CarbonCore.Utils.IO;

    using SMC.SourceLib.Intermediate;

    public class TranslationData
    {
        public TranslationData(IList<Token> tokens)
        {
            this.Tokens = tokens;

            this.TranslationStack = new Stack<TempData>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<Token> Tokens { get; private set; }

        public int CurrentToken { get; set; }
        
        public Stack<TempData> TranslationStack { get; private set; }

        public TempProjectFileEntry FileEntry { get; set; }

        public CarbonFile SourceFile { get; set; }

        public CarbonFile TargetFile { get; set; }
        
        public TempProject ProjectData { get; set; }

        public TranslationDataIterator GetIterator()
        {
            return new TranslationDataIterator(this);
        }
    }
}
