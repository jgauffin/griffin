namespace Griffin.Core.Multitenancy
{
    public interface ITenantAuthorizer
    {
        bool Authorize(ITenant tenant);
    }
}