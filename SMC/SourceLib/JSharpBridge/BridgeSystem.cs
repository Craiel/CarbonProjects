namespace CarbonCore.JSharpBridge
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    using CarbonCore.JSharpBridge.Core;
    using CarbonCore.JSharpBridge.IO;

    public class ThreadLocal : ThreadLocal<object>
    {
        public object Get()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object>();
        }
    }

    public static class BridgeSystem
    {
        public static int UTF_8;

        public static object LocaleUS;

        public static InputStream @in;

        public static StackTraceElement[] GetStackTrace(this Exception exception)
        {
            throw new InvalidOperationException();
        }

        public static void PrintStackTrace(this Exception exception, TextWriter customWriter = null)
        {
            System.Diagnostics.Trace.TraceWarning("PrintStackTrace is not tested!");

            Console.WriteLine(exception.StackTrace);
        }

        public static string GetMessage(this Exception exception)
        {
            System.Diagnostics.Trace.TraceWarning("GetMessage is not tested!");

            return exception.Message;
        }

        public static long JavaNanoTime()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static long GetSystemTime()
        {
            return DateTime.Now.Ticks;
        }

        public static long GetTimerResolution()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static string GetVersion()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string JavaStringFormat(string format, params object[] entries)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string ToBinaryString(int value)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string GetCanonicalName(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string GetSigners(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static Package GetPackage(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Package>();
        }

        public static string GetSimpleName(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static Field GetDeclaredField(this Type type, string name)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Field>();
        }

        public static ClassLoader GetClassLoader(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<ClassLoader>();
        }

        public static string GetProperty(string key)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static RuntimeMXBean GetRuntimeMXBean()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<RuntimeMXBean>();
        }

        public static Runtime GetRuntime()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Runtime>();
        }

        // Todo: check arguments
        public static void Arraycopy<T>(T[] source, int index, T[] target, int targetIndex, int count)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
        
        public static bool RegionMatches(this string source, bool unknown1, int unknown2, string unknown3, int unknown4, int unknown5)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static bool EqualsIgnoreCase(this string source, object other)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static string[] Split(this string source, string splitString, int unknown = 0)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string[]>();
        }

        public static bool IsEmpty(this string source)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static bool IsBlank(string value)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static bool IsNotEmpty(string value)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<bool>();
        }

        public static long GetMostSignificantBits(this Guid guid)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static long GetLeastSignificantBits(this Guid guid)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static Guid CreateGUID(string value)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Guid>();
        }

        public static Guid CreateGUID(long mostSignificant, long leastSignificant)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Guid>();
        }

        public static Guid RandomUUID()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Guid>();
        }

        public static byte ByteValue(this object source)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<byte>();
        }

        public static short ShortValue(this object source)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<short>();
        }

        public static float FloatValue(this object source)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<float>();
        }

        public static byte[] GetBytes(this string source, string encoding = null)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<byte[]>();
        }

        public static void PrintLine(string line)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static object NewInstance(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object>();
        }

        public static int IndexOf(this string value, int target, int unknown)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<int>();
        }

        public static void Println(string getMessage)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void Println(object something)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static Type TypeForName(string name)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Type>();
        }

        public static void SetProperty(string javaNetPreferipv4stack, string @true)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static long CurrentTimeMillis()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static long NanoTime()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static void DeleteCharAt(this StringBuilder builder, int position)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static InputStream GetResourceAsStream(this Type type, string assetsMinecraftLangEnUsLang)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<InputStream>();
        }

        public static int IndexOf(this string value, int index)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<int>();
        }

        public static byte[] GetBytes(this string source, object encoding)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<byte[]>();
        }

        public static void Exit(int exitCode)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void Gc()
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static long GetTime()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<long>();
        }

        public static Guid GuidFromString(string e199ad21Ba8aC53D13D5c69d3a)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Guid>();
        }

        public static string NormalizeSpace(string var2)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static Toolkit GetDefaultToolkit()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Toolkit>();
        }

        public static void OpenURL(string s)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static string Substring(string text, int i, int i1)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string ReplaceAll(this string value, string what, string with)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string GetName(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static Type GetSuperclass(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Type>();
        }

        public static Field[] JavaGetFields(this Type type)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<Field[]>();
        }

        public static object GetDefaultTimeZone()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object>();
        }

        public static object NewInstance(this ConstructorInfo info, object[] unknown)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<object>();
        }

        public static string CreateString(byte[] par0ArrayOfByte = null, int par1 = 0, int i = 0, string utf = null)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }

        public static string CreateString(char[] par0ArrayOfByte)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }
    }
}
