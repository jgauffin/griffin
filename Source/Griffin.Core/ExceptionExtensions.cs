using System;
using System.Reflection;

namespace Griffin.Core
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Used to inlude the original stacktrce when rethrowing excepitons (such as the inner exception for TargetInvocationException)
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception FixStacktrace(this Exception ex)
        {
            FieldInfo remoteStackTraceString =
                typeof (Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic) ??
                typeof (Exception).GetField("remote_stack_trace", BindingFlags.Instance | BindingFlags.NonPublic);

            remoteStackTraceString.SetValue(ex, ex.StackTrace);
            return ex;
        }
    }
}