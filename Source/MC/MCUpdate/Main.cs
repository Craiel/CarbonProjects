namespace CarbonCore.Applications.MCUpdate
{
    using System;
    using System.Text;
    using CarbonCore.Applications.MCUpdate.Data;
    using CarbonCore.Applications.MCUpdate.Logic;
    using CarbonCore.Applications.MCUpdate.Logic.Enums;
    using CarbonCore.ToolFramework.Console.Logic;
    using CarbonCore.Utils;
    using CarbonCore.Utils.Contracts.IoC;
    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Edge.CommandLine.Contracts;
    using CarbonCore.Utils.IO;
    using MCUpdate.Contracts;
    using INEModLookup = Logic.INEModLookup;

    public class Main : ConsoleApplicationBase, IMain
    {
        private readonly IMCModManager modManager;
        private readonly INEModLookup modLookup;

        private CarbonDirectory instanceDirectory;

        private string version;

        private UpdateMode mode = UpdateMode.ClientInstance;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Main(IFactory factory) 
            : base(factory)
        {
            this.modManager = factory.Resolve<IMCModManager>();
            this.modLookup = factory.Resolve<INEModLookup>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override string Name => "MCUpdate";

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override void StartFinished()
        {
            switch (this.mode)
            {
                    case UpdateMode.ClientInstance:
                    case UpdateMode.ServerInstance:
                    {
                        if (!this.InitializeInstanceModManager())
                        {
                            return;
                        }

                        break;
                    }

                    case UpdateMode.Directory:
                    {
                        if (!this.InitializeDirectoryModManager())
                        {
                            return;
                        }

                        break;
                    }
            }

            if (!this.InitializeModLookup())
            {
                return;
            }

            this.CheckForUpdates();
        }

        protected override bool RegisterCommandLineArguments()
        {
            ICommandLineSwitchDefinition definition = this.Arguments.Define("v", "version", x => this.version = x);
            definition.RequireArgument = true;
            definition.Required = true;
            definition.Description = "The version of the current Instance";

            definition = this.Arguments.Define("d", "directory", x => this.instanceDirectory = new CarbonDirectory(x));
            definition.RequireArgument = true;
            definition.Description = "The Root directory of the instance (if not current)";

            definition = this.Arguments.Define("m", "mode", x => Enum.TryParse(x, out this.mode));
            definition.RequireArgument = true;
            definition.Description = "mode, default is ClientInstance";

            return true;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private bool InitializeDirectoryModManager()
        {
            if (this.instanceDirectory == null)
            {
                this.instanceDirectory = RuntimeInfo.WorkingDirectory;
            }

            this.modManager.Initialize(this.instanceDirectory);
            if (this.modManager.Mods.Count <= 0)
            {
                Diagnostic.Warning("No valid mods detected, aborting update check");
                return false;
            }

            return true;
        }

        private bool InitializeInstanceModManager()
        {
            if (this.instanceDirectory == null)
            {
                this.instanceDirectory = RuntimeInfo.WorkingDirectory;
            }

            if (this.instanceDirectory == null || !this.instanceDirectory.Exists)
            {
                Diagnostic.Error("Invalid Directory, no Minecraft Instance detected");
                return false;
            }

            CarbonDirectory modDirectory = this.instanceDirectory.ToDirectory(Constants.ModsDirectory);
            if (!modDirectory.Exists)
            {
                Diagnostic.Warning("No mods directory found, aborting update check");
                return false;
            }

            this.modManager.Initialize(modDirectory);
            if (this.modManager.Mods.Count <= 0)
            {
                Diagnostic.Warning("No valid mods detected, aborting update check");
                return false;
            }

            return true;
        }

        private bool InitializeModLookup()
        {
            MinecraftVersion version = this.DetermineVersion();
            if (version == MinecraftVersion.Unknown)
            {
                Diagnostic.Error("Unknown Version: {0}", this.version);
                return false;
            }

            this.modLookup.Initialize(version);

            return true;
        }

        private void CheckForUpdates()
        {
            Diagnostic.Info($"\n\nChecking Updates for {this.modManager.Mods.Count} Mods against {this.modLookup.Mods.Count} infos\n\n");

            StringBuilder outputBuilder = new StringBuilder("<html><body><table>");
            outputBuilder.AppendFormat($"<tr><td><b>Mod</b></td><td><b>File</b></td><td><b>Current</b></td><td><b>Remote</b></td><td><b>URL</b></td></tr>");

            foreach (MCMod mod in this.modManager.Mods)
            {
                NEModInfo info = this.modLookup.FindMod(mod.Id);
                if (info == null)
                {
                    // No info
                    continue;
                }

                string invariantVersion = info.Version.ToLowerInvariant();
                if (mod.Version.Equals(info.Version)
                    || mod.Version.ToLowerInvariant().Contains(invariantVersion)
                    || mod.File.FileNameWithoutExtension.ToLowerInvariant().Contains(invariantVersion))
                {
                    // Same version
                    continue;
                }
                
                Diagnostic.Info($"Mod {mod.Id} -> {mod.Version} == {info.Version}");
                string formattedFileName = mod.File.FileNameWithoutExtension.Replace('{', '[').Replace('}', ']');
                string formattedVersion = mod.Version.Replace('$', '_').Replace('{', '[').Replace('}', ']');
                outputBuilder.AppendFormat($"<tr><td>{mod.Id}</td><td>{formattedFileName}</td><td>{formattedVersion}</td><td>{info.Version}</td><td><a href={info.LongUrl}>{info.LongUrl}</a></td></tr>");
            }

            outputBuilder.AppendLine("</table></body></html>");

            CarbonFile outFile = this.instanceDirectory.ToFile("MCUpdate.Result.html");
            outFile.WriteAsString(outputBuilder.ToString());
        }

        private MinecraftVersion DetermineVersion()
        {
            switch (this.version.ToLowerInvariant())
            {
                case "1.7.10":
                    {
                        return MinecraftVersion.V1_7_10;
                    }

                case "1.8.9":
                    {
                        return MinecraftVersion.V1_8_9;
                    }

                case "1.9":
                    {
                        return MinecraftVersion.V1_9;
                    }
            }

            return MinecraftVersion.Unknown;
        }
    }
}
