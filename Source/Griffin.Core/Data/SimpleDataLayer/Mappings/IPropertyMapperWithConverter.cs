namespace Griffin.Core.Data.SimpleDataLayer.Mappings
{
    /// <summary>
    /// A property mapper which also can convert the column value to a property value
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPropertyMapperWithConverter<TEntity> : IPropertyMapping<TEntity>
    {
        /// <summary>
        /// Convert value from column type to property type.
        /// </summary>
        /// <param name="context">Context information</param>
        /// <returns>Property valye</returns>
        object ConvertValue(IValueConverterContext<TEntity> context);

    }
}