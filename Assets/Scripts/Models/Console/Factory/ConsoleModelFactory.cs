using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Command.Parser;
using UnityDevConsole.Models.Console;

public static class ConsoleModelFactory
{
    public static ConsoleModel Create ()
    {
        CommandsCollection commandsCollection = new CommandsCollection(new ConsoleCommandFactory());

        return new ConsoleModel(
            new ConsoleOutputModel(),
            commandsCollection,
            new CommandRunnerModel(
                commandsCollection,
                new TypeParserModel()
            )
        );
    }
}
