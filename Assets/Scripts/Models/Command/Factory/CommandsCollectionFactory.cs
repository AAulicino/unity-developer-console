namespace UnityDevConsole.Models.Command
{
    public static class CommandsCollectionFactory
    {
        public static ICommandsCollection Create ()
        {
            return new CommandsCollection(new ConsoleCommandFactory());
        }
    }
}
