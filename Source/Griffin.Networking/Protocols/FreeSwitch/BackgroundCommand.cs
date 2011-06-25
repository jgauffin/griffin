namespace Griffin.Networking.Protocols.FreeSwitch
{
    internal class BackgroundCommand : Command
    {
        private readonly Command _command;

        public BackgroundCommand(Command command)
        {
            _command = command;
        }

        /// <summary>
        /// Gets or sets job id
        /// </summary>
        /// <remarks>
        /// Identifier used to find a background command when it has completed.
        /// </remarks>
        public string JobId { get; set; }

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