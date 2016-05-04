namespace CarbonCore.JSharpBridge.Log
{
    using System;

    public enum Level
    {
        WARNING,
        INFO,
        SEVERE,
        FINE
    }

    public class Logger
    {
        public static Logger GetLogger(string par1Str)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Logger>();
        }

        public void SetUseParentHandlers(bool b)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public Handler[] GetHandlers()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Handler[]>();
        }

        public void RemoveHandler(Handler var4)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void AddHandler(Handler var7)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Log(Level warning, string s, Exception var5)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Log(Level warning, string par1Str)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Log(Level warning, string par1Str, object[] par2ArrayOfObj)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static Logger GetAnonymousLogger()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Logger>();
        }
    }
}
