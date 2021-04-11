using UnityDevConsole.Models.Command;
using UnityDevConsole.Settings;

namespace UnityDevConsole.Models.Console.Hint
{
    public static class ConsoleHintModelFactory
    {
        public static IConsoleHintModel Create (
            IConsoleInputHistoryModel history,
            ICommandsCollectionModel commandsCollection,
            IConsoleSettings settings
        )
        {
            return new ConsoleHintModel(
                commandsCollection,
                history,
                settings
            );
        }
    }
}
