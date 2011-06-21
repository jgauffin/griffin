using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.Core.Net.Protocols.FreeSwitch
{
    class BackgroundCommand : Command
    {
        private readonly Command _command;

        /// <summary>
        /// Gets or sets job id
        /// </summary>
        /// <remarks>
        /// Identifier used to find a background command when it has completed.
        /// </remarks>
        public string JobId { get; set; }

        public BackgroundCommand(Command command)
        {
            _command = command;
        }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public override string CommandName
        {
            get { return "bgapi"; }
        }

        public override string BuildCommandString()
        {
            return "bgapi " + _command.BuildCommandString();
        }
    }
}
