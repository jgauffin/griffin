using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Griffin.Repository.NHibernate
{
    public interface IQuery<TEntity>
    {
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> query);
        IQuery<TEntity> SortBy(Expression<Func<TEntity, object>> property);
        IQuery<TEntity> SortByDescending(Expression<Func<TEntity, object>> property);
        IQuery<TEntity> Paging(int pageNumber, int pageSize);
    }
}
