﻿namespace CarbonCore.JSharpBridge.GL
{
    using CarbonCore.JSharpBridge.IO;

    public static class ARBVertexBufferObject
    {
        public static int GL_ARRAY_BUFFER_ARB;

        public static int GL_STREAM_DRAW_ARB;

        public static void GlGenBuffersARB(IntBuffer vertexBuffers)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void GlBindBufferARB(int glArrayBufferArb, int get)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void GlBufferDataARB(int glArrayBufferArb, ByteBuffer byteBuffer, int glStreamDrawArb)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }
}