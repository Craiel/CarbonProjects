﻿namespace CarbonCore.Applications.CrystalBuild.JavaScript.Logic.Processors
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    using CarbonCore.Applications.CrystalBuild.JavaScript.Contracts.Processors;
    using CarbonCore.CrystalBuild.Java.Logic;
    using CarbonCore.Utils.IO;

    public class CssProcessor : ContentProcessor, ICssProcessor
    {
        private const string IncludeKey = "include";

        private static readonly IList<string> ImageExtensions = new List<string>
                                                                    {
                                                                        ".png",
                                                                        ".jpg",
                                                                        ".jpeg",
                                                                        ".gif",
                                                                        ".bmp"
                                                                    };

        private static readonly Regex CommentRegex = new Regex(@"/\*(.*?)\*/");
        private static readonly Regex StyleRegex = new Regex(@"(.*?){([^\}]*)\}", RegexOptions.IgnoreCase);

        private readonly IDictionary<string, CssStyle> styleDictionary;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CssProcessor()
        {
            this.styleDictionary = new Dictionary<string, CssStyle>();
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void DoProcess(CarbonFile source)
        {
            string content = source.ReadAsString();
            this.AppendLine(content);
        }

        protected override string PostProcessData(string data)
        {
            this.styleDictionary.Clear();
            IList<CssStyle> styles = this.AnalyzeStyleSheets(data);
            foreach (CssStyle style in styles)
            {
                string key = style.Name.ToLowerInvariant();
                if (this.styleDictionary.ContainsKey(key))
                {
                    this.GetContext<JavaScriptBuildingContext>().AddError("Duplicate style: {0}", style.Name);
                    continue;
                }

                this.styleDictionary.Add(key, style);
            }

            // Check all the style's contents next
            foreach (CssStyle style in styles)
            {
                this.CheckStyleContent(style);
            }
            
            return this.FormatStyles(styles);
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private string FormatStyles(IEnumerable<CssStyle> styles)
        {
            var builder = new StringBuilder();
            foreach (CssStyle style in styles)
            {
                builder.Append(style.StyleTarget);
                builder.Append("{");
                foreach (string contentKey in style.Content.Keys)
                {
                    foreach (string entry in style.Content[contentKey])
                    {
                        builder.AppendFormat(" {0}: {1}; ", contentKey, entry);
                    }
                }

                builder.AppendLine("}");
            }

            return builder.ToString();
        }

        private IList<CssStyle> AnalyzeStyleSheets(string data)
        {
            string formattedData = data.Replace("\n", string.Empty).Replace("\r", string.Empty);
            MatchCollection matches = CommentRegex.Matches(formattedData);
            foreach (Match match in matches)
            {
                formattedData = formattedData.Replace(match.Groups[0].Value, string.Empty);
            }

            matches = StyleRegex.Matches(formattedData);
            IList<CssStyle> styles = new List<CssStyle>();
            foreach (Match match in matches)
            {
                var style = new CssStyle
                {
                    IsStyle = match.Groups[1].Value.Trim() == ".",
                    Name = match.Groups[1].Value.Trim(),
                    StyleTarget = match.Groups[1].Value.Trim()
                };

                string content = match.Groups[2].Value.Trim();
                string[] contentSegments = content.Split(';');
                foreach (string segment in contentSegments)
                {
                    if (string.IsNullOrEmpty(segment))
                    {
                        continue;
                    }

                    string[] segmentParts = segment.Split(':');
                    if (segmentParts.Length != 2)
                    {
                        this.GetContext<JavaScriptBuildingContext>().AddError("Invalid segment count: {0}", segment);
                        continue;
                    }
                    
                    style.AddContent(segmentParts[0].Trim(), segmentParts[1].Trim());
                }

                styles.Add(style);
            }

            return styles;
        }

        private void CheckStyleContent(CssStyle style)
        {
            if (style.IsChecked)
            {
                return;
            }

            // Process includes first
            IList<string> includes;
            if (style.Content.TryGetValue(IncludeKey, out includes))
            {
                foreach (string value in includes)
                {
                    CssStyle includedStyle = this.LocateStyle(value);
                    if (includedStyle == null)
                    {
                        this.GetContext<JavaScriptBuildingContext>().AddError("Included style not found, {0} in {1}", value, style.Name);
                        continue;
                    }

                    if (!includedStyle.IsChecked)
                    {
                        this.CheckStyleContent(includedStyle);
                    }

                    this.MergeStyle(includedStyle, style);
                }

                style.Content.Remove(IncludeKey);
            }

            // Now check the style contents for issues
            foreach (string key in style.Content.Keys)
            {
                IList<string> values = style.Content[key];
                foreach (string value in values)
                {
                    foreach (string extension in ImageExtensions)
                    {
                        if (value.ToLowerInvariant().Contains(extension))
                        {
                            this.GetContext<JavaScriptBuildingContext>().AddWarning("Style contains image reference: {0}", style.Name);
                        }   
                    }
                }
            }
            
            style.IsChecked = true;
        }

        private CssStyle LocateStyle(string name)
        {
            string key = name.ToLowerInvariant();
            if (this.styleDictionary.ContainsKey(key) && this.styleDictionary[key].IsStyle)
            {
                return this.styleDictionary[key];
            }

            return null;
        }

        private void MergeStyle(CssStyle source, CssStyle target)
        {
            foreach (string key in source.Content.Keys)
            {
                foreach (string value in source.Content[key])
                {
                    target.AddContent(key, value);
                }
            }
        }

        // -------------------------------------------------------------------
        // internal
        // -------------------------------------------------------------------
        internal class CssStyle
        {
            public CssStyle()
            {
                this.Content = new Dictionary<string, IList<string>>();
            }

            public bool IsStyle { get; set; }
            public bool IsChecked { get; set; }

            public string Name { get; set; }
            public string StyleTarget { get; set; }

            public IDictionary<string, IList<string>> Content { get; private set; }

            public void AddContent(string key, string value)
            {
                IList<string> content;
                if (this.Content.TryGetValue(key, out content))
                {
                    content.Add(value);
                    return;
                }

                content = new List<string> { value };
                this.Content.Add(key, content);
            }
        }
    }
}
