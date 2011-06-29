using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Griffin.Repository.NHibernate
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
