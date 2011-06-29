using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Griffin.Repository.NHibernate
{
    public interface IFlexRepository
    {
        /// <summary>
        /// Find items using a condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        IList<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

        /// <summary>
        /// Find items using a query
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        IList<TEntity> Find<TEntity>(IQuery<TEntity> query);

        /// <summary>
        /// Create a query used by <see cref="Find(Griffin.Repository.NHibernate.IQuery{TEntity})"/>
        /// </summary>
        /// <returns>Data layer specific query implementation</returns>
        IQuery<TEntity> CreateQuery<TEntity>();


        /// <summary>
        /// Find all items
        /// </summary>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        IList<TEntity> FindAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get a specific item using it's primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        TEntity GetById<TEntity, TKey>(TKey key);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="item">Remove entity from data source</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        void Remove<TEntity>(TEntity item);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        void Remove<TEntity>(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        void RemoveById<TEntity, TKey>(TKey key);

        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="item">Entity to update. Key must have been specified.</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        void Save<TEntity>(TEntity item);
    }
}
