namespace Griffin.Core.Multitenancy
{
    /// <summary>
    /// Let all models which belongs to a tenant implement this class.
    /// </summary>
    /// <remarks>
    /// Do keep all tenant models in the business layer. User interface layers should not be 
    /// aware of which tenant objects belongs to, it should all be handled in the BL using
    /// prinicals.
    /// </remarks>
    /// <seealso cref="TenantPrincipal"/>
    interface ITenantModel
    {
        /// <summary>
        /// Gets tenant that the model belongs to.
        /// </summary>
        ITenant BelongsTo { get; }
    }
}
