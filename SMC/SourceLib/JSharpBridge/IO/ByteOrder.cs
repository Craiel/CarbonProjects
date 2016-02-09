namespace CarbonCore.JSharpBridge.IO
{
    public class ByteOrder
    {
        public static ByteOrder LITTLE_ENDIAN;

        public static ByteOrder NativeOrder()
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<ByteOrder>();
        }
    }
}
