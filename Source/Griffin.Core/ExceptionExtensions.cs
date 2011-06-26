using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Griffin.Core
{
    /// <summary>
    /// Extensions making it easier to work with exceptions
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Used to inlude the original stacktrce when rethrowing excepitons (such as the inner exception for TargetInvocationException)
        /// </summary>
        /// <param name="ex">Exception that the stack trace should be preserved for.</param>
        /// <returns>Same exception as being called on</returns>
        /// <remarks>
        /// Use this method if you need to rethrow an exception without using <c>throw;</c>. The method will preserve the
        /// original stacktrace and should work both in .NET and in Mono.
        /// </remarks>
        public static Exception PreserveStacktrace(this Exception ex)
        {
            FieldInfo remoteStackTraceString =
                typeof (Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic) ??
                typeof (Exception).GetField("remote_stack_trace", BindingFlags.Instance | BindingFlags.NonPublic);

            remoteStackTraceString.SetValue(ex, ex.StackTrace);
            return ex;
        }
    }
}