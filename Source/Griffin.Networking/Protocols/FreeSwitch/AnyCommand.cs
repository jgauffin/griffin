namespace Griffin.Networking.Protocols.FreeSwitch
{
    internal class AnyCommand : Command
    {
        public AnyCommand(string name, params string[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; set; }
        public virtual string[] Arguments { get; set; }

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