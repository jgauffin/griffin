namespace Griffin.Core.Net
{
    public interface IObjectPool<out T> where T : class
    {
        T Pull();
        void Push(object reusableObject);
    }
}