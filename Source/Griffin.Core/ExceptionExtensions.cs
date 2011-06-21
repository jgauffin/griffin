using System;
using System.Reflection;

namespace Griffin.Core
{
    public static class ExceptionExtensions
    {
        static ExceptionExtensions()
        {
        }

        public static Exception FixStacktrace(this Exception ex)
        {
            FieldInfo remoteStackTraceString =
                typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic) ??
                typeof(Exception).GetField("remote_stack_trace", BindingFlags.Instance | BindingFlags.NonPublic);

            remoteStackTraceString.SetValue(ex, ex.StackTrace);
            return ex;
        }

    }
}