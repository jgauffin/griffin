using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Griffin.Repository;

namespace Griffin.Repository
{
    /// <summary>
    /// Implementation of repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class, new()
    {
        /// <summary>
        /// Find items using a condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        IList<TEntity> Find(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// Find items using a query
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        IList<TEntity> Find(IQuery<TEntity> query);

        /// <summary>
        /// Create a query used by <see cref="Find(Gate.Core.NHibernate.IQuery{TEntity})"/>
        /// </summary>
        /// <returns>Data layer specific query implementation</returns>
        IQuery<TEntity> CreateQuery();


        /// <summary>
        /// Find all items
        /// </summary>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        IList<TEntity> FindAll();

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        TEntity Get(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get a specific item using it's primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        TEntity GetById(TKey key);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="item">Remove entity from data source</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        void Remove(TEntity item);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        void Remove(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        void RemoveById(TKey key);

        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="item">Entity to update. Key must have been specified.</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        void Save(TEntity item);
    }
}