﻿namespace CarbonCore.JSharpBridge
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public static class ExceptionExtensions
    {
        public static string GetLocalizedMessage(this Exception exception)
        {
            return Utils.Edge.Diagnostic.Internal.NotImplemented<string>();
        }
    }

    [Serializable]
    public class InterruptedException : Exception
    {
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [Serializable]
    public class NumberFormatException : FormatException
    {
    }

    [Serializable]
    public class RuntimeException : Exception
    {
        public RuntimeException(string message = null, Exception par2Exception = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }

        public RuntimeException(Exception inner)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }

    [Serializable]
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }

    [Serializable]
    public class NoSuchAlgorithmException : Exception
    {
    }

    [Serializable]
    public class UnsupportedEncodingException : Exception
    {
    }

    [Serializable]
    public class InvalidKeySpecException : Exception
    {
    }

    [Serializable]
    public class IllegalBlockSizeException : Exception
    {
    }

    [Serializable]
    public class BadPaddingException : Exception
    {
    }

    [Serializable]
    public class InvalidKeyException : Exception
    {
    }

    [Serializable]
    public class NoSuchPaddingException : Exception
    {
    }

    [Serializable]
    public class JsonParseException : Exception
    {
        public JsonParseException(string message = null, Exception inner = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }

    [Serializable]
    public class EOFException : Exception
    {
    }

    [Serializable]
    public class IllegalFormatException : Exception
    {
    }

    [Serializable]
    public class SocketException : Exception
    {
    }

    [Serializable]
    public class SocketTimeoutException : Exception
    {
    }

    [Serializable]
    public class SoundSystemException : Exception
    {
    }

    [Serializable]
    public class IllegalAccessException : Exception
    {
    }

    [Serializable]
    public class InvalidSyntaxException : Exception
    {
    }

    [Serializable]
    public class ParseException : Exception
    {
    }

    [Serializable]
    public class MalformedURLException : Exception
    {
    }

    [Serializable]
    public class UnknownHostException : Exception
    {
    }

    [Serializable]
    public class PortUnreachableException : Exception
    {
    }

    [Serializable]
    public class ConnectException : Exception
    {
    }

    [Serializable]
    public class DataFormatException : Exception
    {
    }

    [Serializable]
    public class URISyntaxException : Exception
    {
    }

    [Serializable]
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string message = null)
        {
            Utils.Edge.Diagnostic.Internal.NotImplemented();
        }
    }
}
