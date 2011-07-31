using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Griffin.Logging.Targets.File
{
    [ContractClassFor(typeof(IFileWriter))]
    abstract class FileWriterContract : IFileWriter
    {

        public FileConfiguration Configuration
        {
            get
            {
                Contract.Ensures(Contract.Result<FileConfiguration>() != null);
                return null;
            }
        }

        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return null;
            }
        }

        public void Write(string logEntry)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(logEntry));
        }
    }
}
