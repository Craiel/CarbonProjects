namespace SourceTranslation.Patching
{
    using System.Collections.Generic;

    using SourceTranslation.Java;

    public static class Patcher
    {
        public static void Apply(string file, IList<string> data, IDictionary<string, IList<Patch>> perFilePatches = null)
        {
            IList<Patch> filePatches = null;
            if (perFilePatches != null && perFilePatches.ContainsKey(file))
            {
                filePatches = perFilePatches[file];
            }

            if (Entry.Config.Verbose)
            {
                if (filePatches != null)
                {
                    System.Diagnostics.Trace.TraceInformation(
                        " -- Patching {0} with {1} Java and {2} Specific Patches",
                        file,
                        JavaDefinitions.JavaPatches.Count,
                        filePatches.Count);
                }
                else
                {
                    System.Diagnostics.Trace.TraceInformation(
                        " -- Patching {0} with {1} Java Patches",
                        file,
                        JavaDefinitions.JavaPatches.Count);
                }
            }

            for (int i = 0; i < data.Count; i++)
            {
                string line = data[i];

                foreach (Patch globalPatch in JavaDefinitions.JavaPatches)
                {
                    if (globalPatch.Apply(ref line) && Entry.Config.Verbose)
                    {
                        System.Diagnostics.Trace.TraceInformation("    | Applied Java patch @line {0}, {1}", i, globalPatch.Name ?? globalPatch.Pattern);
                    }
                }

                if (filePatches != null)
                {
                    foreach (Patch patch in filePatches)
                    {
                        if (patch.Apply(ref line) && Entry.Config.Verbose)
                        {
                            System.Diagnostics.Trace.TraceInformation(
                                "    | Applied Specific patch @line {0}, {1}", i, patch.Name ?? patch.Pattern);
                        }
                    }
                }

                data[i] = line;
            }

            if (filePatches == null)
            {
                return;
            }

            // Do some checking on the patching results if we had per file patches to apply
            foreach (Patch patch in filePatches)
            {
                if (patch.ApplyTargetCount > 0 && patch.ApplyCount != patch.ApplyTargetCount)
                {
                    System.Diagnostics.Trace.TraceWarning("Patch applied only {0} out of {1}: {2}", patch.ApplyCount, patch.ApplyTargetCount, patch.Pattern);
                }
                else if (patch.ApplyCount <= 0)
                {
                    System.Diagnostics.Trace.TraceWarning("Patch did not apply for {0}: {1}", file, patch.Pattern);
                }
            }
        }

        public static void WriteReport()
        {
            foreach (Patch patch in JavaDefinitions.JavaPatches)
            {
                System.Diagnostics.Trace.TraceInformation("  Patch {0} applied {1} times", patch.Name ?? patch.Pattern, patch.ApplyCount);
            }
        }
    }
}
