namespace SourceTranslation.Patching
{
    using System;

    public enum PatchMode
    {
        Any,
        Once,
        Count
    }

    public class Patch
    {
        public Patch(string pattern, string replacement, PatchMode mode = PatchMode.Any, int count = 0)
        {
            this.Mode = mode;

            this.Pattern = pattern;
            this.Replacement = replacement;

            this.ApplyTargetCount = count;
        }

        public string Name { get; set; }

        public string Pattern { get; private set; }
        public string Replacement { get; private set; }

        public PatchMode Mode { get; private set; }

        public long ApplyCount { get; protected set; }
        public long ApplyTargetCount { get; private set; }

        public virtual bool Apply(ref string line)
        {
            if (this.ApplyTargetCount > 0 && this.ApplyCount >= this.ApplyTargetCount)
            {
                return false;
            }

            int index = line.IndexOf(this.Pattern);
            bool didMatch = false;
            while (index >= 0)
            {
                didMatch = true;
                line = string.Concat(line.Substring(0, index), this.Replacement, line.Substring(index + this.Pattern.Length, line.Length - index - this.Pattern.Length));
                this.ApplyCount++;
                if (this.ApplyTargetCount > 0 && this.ApplyCount > this.ApplyTargetCount)
                {
                    return true;
                }

                index = line.IndexOf(this.Pattern, index + this.Replacement.Length);
            }

            return didMatch;
        }
    }
}
