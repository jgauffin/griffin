using Griffin.Core.Data.SimpleDataLayer.Mappings;
using Griffin.InversionOfControl;

namespace Griffin.Core.Data.SimpleDataLayer.Mappings
{
    public class ContainerMappingProvider : IMappingProvider
    {
        private readonly IServiceLocator _serviceLocator;

        public ContainerMappingProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IEntityMapper<T> Get<T>()
        {
            return _serviceLocator.Resolve<IEntityMapper<T>>();
        }

    }
}
