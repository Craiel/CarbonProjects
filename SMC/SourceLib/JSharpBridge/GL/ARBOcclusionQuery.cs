namespace CarbonCore.JSharpBridge.GL
{
    using CarbonCore.JSharpBridge.IO;

    public static class ARBOcclusionQuery
    {
        public static int GL_QUERY_RESULT_AVAILABLE_ARB;

        public static int GL_QUERY_RESULT_ARB;

        public static int GL_SAMPLES_PASSED_ARB;

        public static void GlGenQueriesARB(IntBuffer glOcclusionQueryBase)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void GlGetQueryObjectuARB(int glOcclusionQuery, int glQueryResultAvailableArb, IntBuffer occlusionResult)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void GlBeginQueryARB(int glSamplesPassedArb, int glOcclusionQuery)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public static void GlEndQueryARB(int glSamplesPassedArb)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }
}
