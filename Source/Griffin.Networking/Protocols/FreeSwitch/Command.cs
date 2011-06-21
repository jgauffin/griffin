using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net.Protocols.FreeSwitch
{
    public abstract class Command
    {
        protected Command()
        {
        }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public abstract string CommandName { get;  }

        public abstract string BuildCommandString();

        public override string ToString()
        {
            return CommandName;
        }
    }
}
