namespace CarbonCore.JSharpBridge.Core
{
    using System;
    
    public class BridgeThread : IDisposable
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public BridgeThread(string name = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public BridgeThread(Runnable source)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public BridgeThread(object source, string fileIoThread)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Dispose()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void SetDaemon(bool unknown)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Start()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Stop()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void Sleep(long time)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void Interrupt()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public bool IsInterrupted()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        public static void Yield()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void DumpStack()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public bool IsAlive()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static BridgeThread CurrentThread()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<BridgeThread>();
        }

        public StackTraceElement[] GetStackTrace()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<StackTraceElement[]>();
        }

        public void SetPriority(int i)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public void SetName(string minecraftMainThread)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }
}
