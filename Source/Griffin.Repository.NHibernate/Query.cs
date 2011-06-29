using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Griffin.Repository.NHibernate
{
    public class Query<TEntity> : IQuery<TEntity>
    {
        private readonly List<SortKey<TEntity>> _items = new List<SortKey<TEntity>>();

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public List<SortKey<TEntity>> SortKeys
        {
            get { return _items; }
        }

        public Expression<Func<TEntity, bool>> WhereClause { get; private set; }

        #region IQuery<TEntity> Members

        public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            WhereClause = query;
            return this;
        }

        public IQuery<TEntity> SortBy(Expression<Func<TEntity, object>> property)
        {
            _items.Add(new SortKey<TEntity> {Ascending = true, PropertyName = property});
            return this;
        }

        public IQuery<TEntity> SortByDescending(Expression<Func<TEntity, object>> property)
        {
            _items.Add(new SortKey<TEntity> {Ascending = false, PropertyName = property});
            return this;
        }

        public IQuery<TEntity> Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            return this;
        }

        #endregion
    }

    public class SortKey<TEntity>
    {
        /// <summary>
        /// Gets or sets description
        /// </summary>
        public bool Ascending { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public Expression<Func<TEntity, object>> PropertyName { get; set; }
    }
}