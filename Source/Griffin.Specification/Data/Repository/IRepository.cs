/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Griffin.Repository;

namespace Griffin.Data.Repository
{
    /// <summary>
    /// Implementation of repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TKey">Primary key type</typeparam>
    /// <example>
    /// <code>
    /// var user = repos.Get(user => user.LastName == "Gauffin" && user.FirstName == "Jonas");
    /// if (user == null)
    ///     Console.WriteLine("Failed to find user");
    /// </code>
    /// </example>
    /// <remarks>
    /// <para>
    /// The repository specification is not coupled to any session management which means that each implementation
    /// can use it's on way to handle transactions and database connections. However, we strongly encourage each
    /// implementor to use the transaction and session management which is included in the <c>Griffin.Data</c> namespace.
    /// </para>
    /// <para>
    /// Implementations should NOT return <c>IQueryable{T}</c>
    /// </para>
    /// </remarks>
    public interface IRepository<TEntity, in TKey> where TEntity : class, new()
    {
        /// <summary>
        /// Find items using a condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// var users = repository.Find(user => user.LastName == "Gauffin");
        /// foreach (var user in users)
        /// {
        /// }
        /// </code>
        /// </example>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// Find items using a query
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        IEnumerable<TEntity> Find(IQuery<TEntity> query);

        /// <summary>
        /// Create a query used by <see cref="Find(IQuery{TEntity})"/>
        /// </summary>
        /// <returns>Data layer specific query implementation</returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// var query = repos.CreateQuery()
        ///     .Where(user => user.FirstName == "Jonas")
        ///     .OrderBy(user => user.LastName)
        ///     .Paging(1, 50);
        /// 
        /// var userS = repos.Find(query);
        /// ]]>
        /// </code>
        /// </example>
        IQuery<TEntity> CreateQuery();


        /// <summary>
        /// Find all items
        /// </summary>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        IEnumerable<TEntity> FindAll();

        /// <summary>
        /// Gets the number of items that a query will be returned.
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>Number of items that will be returned.</returns>
        /// <remarks>
        /// Counting is useful when working with paged result to be able to see the number
        /// of items that will be returned by an query.
        /// </remarks>
        /// <example>
        /// <code>
        /// var query = repository.CreateQuery()
        ///     .Where(user => user.Age >= 20 and user.Age >= 65);
        ///     .Paging(1, 50);
        /// 
        /// // paging is ignored
        /// var count = repository.Count(query); 
        ///
        /// // paging is used
        /// var users = repository.Find(query);
        /// </code>
        /// </example>
        int Count(IQuery<TEntity> query);

        /// <summary>
        /// Gets the number of items that will be returned by the specified condition.
        /// </summary>
        /// <param name="condition">Condition</param>
        /// <returns>Number of items</returns>
        /// <example>
        /// <code>
        /// var count = repository.Count(user => user.Age == 32);
        /// Console.WriteLine(string.Format("Number of returned items will be: {0}.", count));
        /// </code>
        /// </example>
        int Count(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// var user = repository.Get(user => user.Id = 10);
        /// if (user == null)
        ///     user = new User(Session["FirstName"], Session["LastName"]);
        /// 
        /// return user;
        /// </code>
        /// </example>
        TEntity Get(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get a specific item using it's primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// var user = repository.GetById(10);
        /// if (user == null)
        ///     user = new User(Session["FirstName"], Session["LastName"]);
        /// 
        /// return user;
        /// </code>
        /// </example>
        TEntity GetById(TKey key);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="item">Remove entity from data source</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// var user = repository.GetById(10);
        /// if (user != null && user.CompanyId == Session["CompanyId"])
        ///     repository.Remove(user);
        /// </code>
        /// </example>
        void Remove(TEntity item);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// repository.Remove(user => user.FirstName == "Jonas" && user.LastName == "Gauffin");
        /// </code>
        /// </example>
        void Remove(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// repository.RemoveById(20);
        /// </code>
        /// </example>
        void RemoveById(TKey key);

        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="item">Entity to update. Key must have been specified.</param>
        /// <exception cref="RepositoryException">Data layer failed to complete the operation.</exception>
        /// <example>
        /// <code>
        /// repository.Save(new User("Jonas", "Gauffin"));
        /// </code>
        /// </example>
        void Save(TEntity item);
    }
}