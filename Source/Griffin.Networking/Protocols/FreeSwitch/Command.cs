namespace Griffin.Networking.Protocols.FreeSwitch
{
    public abstract class Command
    {
        /// <summary>
        /// Gets or sets description
        /// </summary>
        public abstract string CommandName { get; }

        public abstract string BuildCommandString();

        public override string ToString()
        {
            return CommandName;
        }
    }
}