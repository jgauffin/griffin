using System;
using System.Data;
using NHibernate;

namespace Griffin.Repository.NHibernate
{
    internal class HibernateHelper
    {
        private readonly Type _entityType;

        public HibernateHelper(Type entityType)
        {
            _entityType = entityType;
        }

        /// <exception cref="DataLayerException"><c>DataLayerException</c>.</exception>
        public T ExceptionHandling<T>(Func<T> action, string failedMessage)
        {
            try
            {
                return action();
            }
            catch (MappingException err)
            {
                throw new DataLayerException(_entityType,
                                             "Failed to find class/table mapping definition.", err);
            }
            catch (HibernateException err)
            {
                throw new DataLayerException(_entityType, failedMessage, err);
            }
        }
    }

    public static class NHibernateExtensions
{
            public static void DeleteById<TEntity>(this ISession session, object id)
            {
                string queryString = string.Format("delete {0} where id = :id",
                                                   typeof(TEntity));
                session.CreateQuery(queryString)
                    .SetParameter("id", id)
                    .ExecuteUpdate();
            }

}
}