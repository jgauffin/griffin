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
using System.Linq.Expressions;

namespace Griffin.Data.Repository
{
    /// <summary>
    /// Used to querying a repository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <example>
    /// <code>
    /// var query = repos.CreateQuery()
    ///     .Where(user => user.LastName == 'Gauffin')
    ///     .SortBy(user => user.LastName)
    ///     .SortByDescending(user => user.Age)
    ///     .Paging(1, 50);
    /// 
    /// var users = repos.Find(query);
    /// </code>
    /// </example>
    public interface IQuery<TEntity>
    {
        /// <summary>
        /// Where clause limiting result
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>This instance</returns>
        /// <example>
        /// <code>
        /// var query = repos.CreateQuery()
        ///     .SortBy(model => model.FirstName);
        /// 
        /// var users = repos.Find(query);
        /// </code>
        /// </example>
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Sort ascending by the specified property
        /// </summary>
        /// <param name="property">Name of property</param>
        /// <returns>This instance</returns>
        /// <example>
        /// <code>
        /// var query = repos.CreateQuery()
        ///     .SortBy(model => model.FirstName);
        /// 
        /// var users = repos.Find(query);
        /// </code>
        /// </example>
        IQuery<TEntity> SortBy(Expression<Func<TEntity, object>> property);

        /// <summary>
        /// Sort descending by the specified property
        /// </summary>
        /// <param name="property">Name of property</param>
        /// <returns>This instance</returns>
        /// <example>
        /// <code>
        /// var query = repos.CreateQuery()
        ///     .SortByDescending(model => model.FirstName);
        /// 
        /// var users = repos.Find(query);
        /// </code>
        /// </example>
        IQuery<TEntity> SortByDescending(Expression<Func<TEntity, object>> property);

        /// <summary>
        /// Limit the result
        /// </summary>
        /// <param name="pageNumber">Page (1 is the first page)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>This instance</returns>
        /// <example>
        /// <code>
        /// var query = repos.CreateQuery()
        ///     .Paging(2, 50);
        /// 
        /// var users = repos.Find(query);
        /// </code>
        /// </example>
        IQuery<TEntity> Paging(int pageNumber, int pageSize);
    }
}