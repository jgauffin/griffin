namespace Griffin.Core.Data.SimpleMapper
{
    /// <summary>
    /// Delegate used to convert a column value to a property value
    /// </summary>
    /// <typeparam name="TEntity">Entity model type</typeparam>
    /// <param name="context">Context used to make conversion esier</param>
    /// <returns>Value which can be assigned to the class property</returns>
    public delegate object ValueConverter<in TEntity>(IValueConverterContext<TEntity> context);
}