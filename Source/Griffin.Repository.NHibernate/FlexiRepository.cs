using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace Griffin.Repository.NHibernate
{
    public class FlexiRepository : IFlexRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public FlexiRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        /// <exception cref="DataLayerException"><c>DataLayerException</c>.</exception>
        private void ExceptionHandling<TEntity>(Action action, string failedMessage)
        {
            try
            {
                action();
            }
            catch (MappingException err)
            {
                throw new DataLayerException(typeof (TEntity),
                                             string.Format("Failed to find mapping for type '{0}'.",
                                                           typeof (TEntity).FullName), err);
            }
            catch (HibernateException err)
            {
                throw new DataLayerException(typeof (TEntity), failedMessage, err);
            }
        }


        /// <exception cref="DataLayerException"><c>DataLayerException</c>.</exception>
        private TEntity ExceptionHandling<TEntity>(Func<TEntity> action, string failedMessage)
        {
            try
            {
                return action();
            }
            catch (MappingException err)
            {
                throw new DataLayerException(typeof (TEntity),
                                             "Failed to find class/table mapping definition.", err);
            }
            catch (HibernateException err)
            {
                throw new DataLayerException(typeof (TEntity), failedMessage, err);
            }
        }

        #region IFlexRepository Members

        /// <summary>
        /// Find items using a condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public IList<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            string errorMessage = string.Format("Failed to find entities using query: " + condition);
            ISession session = _sessionProvider.GetSession();
            IQueryable<TEntity> query = session.Query<TEntity>().Where(condition);
            return ExceptionHandling(() => query.ToList(), errorMessage);
        }

        /// <summary>
        /// Find items using a query
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public IList<TEntity> Find<TEntity>(IQuery<TEntity> query)
        {
            string errorMessage = string.Format("Failed to find entities using query: " + query);
            ISession session = _sessionProvider.GetSession();
            IQueryable<TEntity> linqQuery = session.Query<TEntity>();
            var myQuery = (Query<TEntity>) query;

            if (myQuery.WhereClause != null)
                linqQuery = linqQuery.Where(myQuery.WhereClause);

            foreach (var key in myQuery.SortKeys)
            {
                linqQuery = key.Ascending
                                ? linqQuery.OrderBy(key.PropertyName)
                                : linqQuery.OrderByDescending(key.PropertyName);
            }

            if (myQuery.PageNumber != 0)
                linqQuery = linqQuery.Skip((myQuery.PageNumber - 1)*myQuery.PageSize).Take(myQuery.PageSize);

            return ExceptionHandling(() => linqQuery.ToList(), errorMessage);
        }

        /// <summary>
        /// Create a query used by <see cref="Find(Griffin.Repository.NHibernate.IQuery{TEntity})"/>
        /// </summary>
        /// <returns>Data layer specific query implementation</returns>
        public IQuery<TEntity> CreateQuery<TEntity>()
        {
            return new Query<TEntity>();
        }

        /// <summary>
        /// Find all items
        /// </summary>
        /// <returns>Collection of items (or an empty list if none is found).</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public IList<TEntity> FindAll<TEntity>() where TEntity : class
        {
            string errorMsg = "Failed to retrieve entries.";
            ISession session = _sessionProvider.GetSession();
            return ExceptionHandling(() => session.QueryOver<TEntity>().List(), errorMsg);
        }

        /// <summary>
        /// Get a specific item
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> query)
        {
            string errorMsg = string.Format("Failed to retrieve entry using query '{0}'", query);
            ISession session = _sessionProvider.GetSession();
            return ExceptionHandling(() =>
                                     session.Query<TEntity>().Where(query).Take(1).ToList().SingleOrDefault()
                                     ,
                                     errorMsg);
        }

        /// <summary>
        /// Get a specific item using it's primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Entity if found; otherwise <c>null</c>.</returns>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public TEntity GetById<TEntity, TKey>(TKey key)
        {
            string errorMsg = string.Format("Failed to retrieve entry using id '{0}'", key);
            ISession session = _sessionProvider.GetSession();
            return ExceptionHandling(() => session.Get<TEntity>(key), errorMsg);
        }

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="item">Remove entity from data source</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public void Remove<TEntity>(TEntity item)
        {
            string errorMsg = string.Format("Failed to remove '{0}'", item);
            ISession session = _sessionProvider.GetSession();
            ExceptionHandling<TEntity>(() => session.Delete(item), errorMsg);
        }

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="query">LINQ query</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public void Remove<TEntity>(Expression<Func<TEntity, bool>> query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove entity from the data source.
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public void RemoveById<TEntity, TKey>(TKey key)
        {
            string errorMsg = string.Format("Failed to remove using id '{0}'", key);
            ISession session = _sessionProvider.GetSession();
            ExceptionHandling<TEntity>(() => session.DeleteById<TEntity>(key), errorMsg);
        }

        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="item">Entity to update. Key must have been specified.</param>
        /// <exception cref="DataLayerException">Data layer failed to complete the operation.</exception>
        public void Save<TEntity>(TEntity item)
        {
            string errorMsg = string.Format("Failed to save '{0}'", item);
            ISession session = _sessionProvider.GetSession();
            ExceptionHandling<TEntity>(() => session.SaveOrUpdate(item), errorMsg);
        }

        #endregion
    }
}