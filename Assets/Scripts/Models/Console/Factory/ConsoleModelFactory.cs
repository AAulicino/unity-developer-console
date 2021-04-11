using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Command.Parser;
using UnityDevConsole.Models.Console;

public static class ConsoleModelFactory
{
    public static ConsoleModel Create (
        IConsoleInputHistoryModel historyModel,
        ICommandsCollectionModel commandsCollection
    )
    {
        return new ConsoleModel(
            commandsCollection,
            new CommandRunnerModel(
                commandsCollection,
                new TypeParserModel()
            ),
            new ConsoleOutputModel(),
            historyModel
        );
    }
}
