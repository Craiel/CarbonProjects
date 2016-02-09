namespace SMC.SourceLib.Intermediate
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    public enum TempProjectLanguage
    {
        Java,
    }

    public enum TempProjectStat
    {
        Files,
        Tokens,
        Classes,
        Functions,
        Members
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class TempProject
    {
        private readonly object statLock = new object();
        private readonly object fileLock = new object();

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [DefaultValue(null)]
        public string Name { get; set; }

        [DefaultValue(null)]
        public string TemplateFile { get; set; }

        [DefaultValue(null)]
        public string FileSourceRoot { get; set; }

        [DefaultValue(null)]
        public string FileTargetRoot { get; set; }

        [DefaultValue("")]
        public string RootNameSpace { get; set; }

        public TempProjectLanguage SourceLanguage { get; set; }

        [DefaultValue(null)]
        public Dictionary<TempProjectStat, long> Stats { get; set; }
        
        [DefaultValue(null)]
        public List<TempProjectFileEntry> Files { get; set; }
        
        public void AddStat(TempProjectStat stat, long value = 1)
        {
            lock (this.statLock)
            {
                if (this.Stats == null)
                {
                    this.Stats = new Dictionary<TempProjectStat, long>();
                }

                if (!this.Stats.ContainsKey(stat))
                {
                    this.Stats.Add(stat, value);
                    return;
                }
            }

            this.Stats[stat] += value;
        }

        public void AddFile(TempProjectFileEntry fileEntry)
        {
            lock (this.fileLock)
            {
                if (this.Files == null)
                {
                    this.Files = new List<TempProjectFileEntry>();
                }

                this.Files.Add(fileEntry);
            }
        }
    }
}