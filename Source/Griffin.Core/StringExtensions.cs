namespace Griffin.Core
{
    public static class StringExtensions
    {
        public static string FormatWith(this string instance, params object[] arguments)
        {
            return string.Format(instance, arguments);
        }

        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }
    }
}