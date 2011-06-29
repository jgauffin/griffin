using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Repository
{
    public interface IDbSessionFactory
    {
        void OpenSession();
        void CloseSession();
        IDbSession Current { get; }

    }
}
