using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Griffin.Repository
{
    /// <summary>
    /// Used to querying a repository 
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public interface IQuery<TEntity>
    {
        /// <summary>
        /// Where clause limiting result
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> query);
        IQuery<TEntity> SortBy(Expression<Func<TEntity, object>> property);
        IQuery<TEntity> SortByDescending(Expression<Func<TEntity, object>> property);
        IQuery<TEntity> Paging(int pageNumber, int pageSize);
    }
}