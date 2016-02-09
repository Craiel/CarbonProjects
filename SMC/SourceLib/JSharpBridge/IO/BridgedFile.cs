namespace CarbonCore.JSharpBridge.IO
{
    using CarbonCore.JSharpBridge.Net;

    public class BridgedFile
    {
        public BridgedFile(BridgedFile directory, string fileName)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public BridgedFile(string directory)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public BridgedFile GetParentFile()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
            return null;
        }

        public void Mkdirs()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public string GetAbsolutePath()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public bool IsDirectory()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public void Delete()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public BridgedFile[] ListFiles(FileFilter resourcePackFilter = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
            return null;
        }

        public void Mkdir()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public string GetName()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public bool Exists()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public long LastModified()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public URI ToURI()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<URI>();
        }

        public bool IsFile()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public void RenameTo(BridgedFile var8)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public string[] List()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string[]>();
        }
    }
}
