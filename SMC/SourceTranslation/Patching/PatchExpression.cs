namespace SourceTranslation.Patching
{
    using System.Text.RegularExpressions;

    public class PatchExpression : Patch
    {
        public PatchExpression(string pattern, string replacement, PatchMode mode = PatchMode.Any, int count = 0, RegexOptions options = RegexOptions.None)
            : base(pattern, replacement, mode, count)
        {
            this.Expression = new Regex(pattern, options);
            this.TargetGroup = 1;
        }

        public Regex Expression { get; private set; }
        public int TargetGroup { get; set; }
        public int? ReplacementGroup { get; set; }

        public override bool Apply(ref string line)
        {
            if (this.ApplyTargetCount > 0 && this.ApplyCount >= this.ApplyTargetCount)
            {
                return false;
            }

            var matches = this.Expression.Matches(line);
            foreach (Match match in matches)
            {
                string target = match.Groups[this.TargetGroup].ToString();
                string replacement = this.Replacement;
                if (this.ReplacementGroup != null)
                {
                    replacement = string.Format(replacement, match.Groups[this.ReplacementGroup.Value]);
                }

                line = line.Replace(target, replacement);
                this.ApplyCount++;

                if (this.ApplyTargetCount > 0 && this.ApplyCount >= this.ApplyTargetCount)
                {
                    return true;
                }
            }

            return matches.Count > 0;
        }
    }
}
