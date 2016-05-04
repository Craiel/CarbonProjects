namespace SMC.SourceCompilation.Logic
{
    using System.IO;
    using System.Text;
    
    using CarbonCore.Utils.IO;
    using CarbonCore.Utils.Json;

    using SMC.SourceCompilation.Contracts;
    using SMC.SourceLib.Intermediate;
    using SMC.SourceLib.Sharp;

    public partial class Compiler : ICompiler
    {
        public const char NameSpaceSeparator = '.';

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string GetNameSpace(CarbonDirectory relativePath)
        {
            return relativePath.GetPath().Replace(Path.DirectorySeparatorChar, NameSpaceSeparator).Trim(NameSpaceSeparator);
        }

        public bool Compile(ICompilationState state, CompilationJob job)
        {
            System.Diagnostics.Trace.TraceInformation("Compile: {0} -> {1}", job.SourceFile, job.TargetFile);

            // Initialize the compilation data
            var data = new CompilationData
                    {
                        State = state,
                        SourceContent = JsonExtensions.LoadFromFile<TempFileFull>(job.SourceFile, job.IsCompressed),
                        TargetContent = new SharpFile { NameSpace = job.NameSpace }
                    };

            // Compile the file data
            this.CompileFile(data);

            // Create the target file directory if it does not exist
            if (!job.TargetFile.GetDirectory().IsNull)
            {
                job.TargetFile.GetDirectory().Create();
            }

            // Save the target file
            using (var stream = job.TargetFile.OpenWrite())
            {
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 4096, true))
                {
                    writer.Write(data.TargetContent.ToString());
                }
            }

            return true;
        }
    }
}
