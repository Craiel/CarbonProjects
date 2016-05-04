namespace SMC.SourceCompilation.Logic
{
    using System.Threading.Tasks;
    
    using CarbonCore.Utils.IO;

    public class CompilationJob
    {
        public CarbonFile SourceFile { get; set; }

        public CarbonFile TargetFile { get; set; }

        public bool IsCompressed { get; set; }

        public string NameSpace { get; set; }
        
        public Task ActiveTask { get; set; }
    }
}