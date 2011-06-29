using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Griffin.Repository;

namespace Griffin.Repository
{
    public class Query<TEntity> : IQuery<TEntity>
    {
        private readonly List<SortKey<TEntity>> _items = new List<SortKey<TEntity>>();
        private int _pageNumber;
        private int _pageSize;
        private Expression<Func<TEntity, bool>> _query;

        public int PageNumber
        {
            get { return _pageNumber; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public List<SortKey<TEntity>> SortKeys
        {
            get { return _items; }
        }

        public Expression<Func<TEntity, bool>> WhereClause
        {
            get { return _query; }
        }

        #region IQuery<TEntity> Members

        public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            _query = query;
            return this;
        }

        public IQuery<TEntity> SortBy(Expression<Func<TEntity, object>> property)
        {
            _items.Add(new SortKey<TEntity> { Ascending = true, PropertyName = property });
            return this;
        }

        public IQuery<TEntity> SortByDescending(Expression<Func<TEntity, object>> property)
        {
            _items.Add(new SortKey<TEntity> { Ascending = false, PropertyName = property });
            return this;
        }

        public IQuery<TEntity> Paging(int pageNumber, int pageSize)
        {
            
            _pageNumber = pageNumber;
            _pageSize = pageSize;
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