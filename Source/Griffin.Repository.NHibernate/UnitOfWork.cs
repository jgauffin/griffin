/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Griffin.Repository.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _saved;
        private bool _rolledBack;
        private global::NHibernate.ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
            Session = session;
            _transaction = session.BeginTransaction();
        }

        public ISession Session { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            Cancel();
            Disposed(this, EventArgs.Empty);
        }

        #endregion

        public event EventHandler Disposed = delegate { };
        public void SaveChanges()
        {
            _saved = true;
            _transaction.Commit();
        }

        public void Cancel()
        {
            if (_rolledBack ||  _saved)
                return;
            
            _rolledBack = true;
            _transaction.Rollback();
        }

        public event EventHandler Saved;
    }
}
*/