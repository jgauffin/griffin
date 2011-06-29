using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Repository
{
    public interface ITransactionManager
    {
        ITransaction Create();
    }
}
