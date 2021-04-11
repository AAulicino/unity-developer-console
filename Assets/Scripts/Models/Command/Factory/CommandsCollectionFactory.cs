using UnityDevConsole.Settings;

namespace UnityDevConsole.Models.Command
{
    public static class CommandsCollectionFactory
    {
        public static ICommandsCollectionModel Create (IConsoleSettings settings)
        {
            return new CommandsCollectionModel(settings, new ConsoleCommandFactory());
        }
    }
}
