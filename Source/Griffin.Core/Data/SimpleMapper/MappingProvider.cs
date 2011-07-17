using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Data.SimpleMapper
{
    public class MappingProvider
    {
        public static MappingProvider Instance
        {
            get { return null; }
        }

        public IEntityMapper<T> Get<T>()
        {
            return null;
        }
    }
}
