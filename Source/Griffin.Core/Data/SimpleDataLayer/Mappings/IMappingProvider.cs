namespace Griffin.Core.Data.SimpleDataLayer.Mappings
{
    public interface IMappingProvider
    {
        IEntityMapper<T> Get<T>();
    }
}