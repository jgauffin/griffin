using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net.Protocols.FreeSwitch
{
    class AnyCommand : Command
    {
        public string Name { get; set; }
        public virtual string[] Arguments { get; set; }

        public AnyCommand(string name, params string[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public override string CommandName
        {
            get { return Name; }
        }

        public override string BuildCommandString()
        {
            return Name + " " + string.Join(" ", Arguments);
        }
    }
}
