namespace SMC.SourceCompilation.Logic
{
    using SMC.SourceLib.Intermediate;
    using SMC.SourceLib.Sharp;

    public partial class Compiler
    {
        private void CompileFile(CompilationData data)
        {
            if (data.SourceContent.Classes != null)
            {
                foreach (TempClass @class in data.SourceContent.Classes)
                {
                    SharpClass sharpClass = this.CompileClass(data, @class);
                    data.TargetContent.AddClass(sharpClass);
                }
            }

            this.CompileFileIncludes(data);
        }

        private void CompileFileIncludes(CompilationData data)
        {
            /*foreach (string include in data.SourceContent.Includes)
            {
                //
                // Todo: process the includes
            }*/
        }
    }
}
