namespace UnityDevConsole.Models.Command
{
    public static class CommandsCollectionFactory
    {
        public static ICommandsCollection Create (IConsoleSettings settings)
        {
            return new CommandsCollection(settings, new ConsoleCommandFactory());
        }
    }
}
