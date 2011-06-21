namespace Griffin.Core
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object instance)
        {
            return (T) instance;
        }
    }
}