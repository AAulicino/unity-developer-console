using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Models.Console.Hint
{
    public static class ConsoleHintModelFactory
    {
        public static IConsoleHintModel Create (
            IConsoleInputHistoryModel history,
            ICommandsCollection commandsCollection
        )
        {
            return new ConsoleHintModel(
                commandsCollection,
                history
            );
        }
    }
}
