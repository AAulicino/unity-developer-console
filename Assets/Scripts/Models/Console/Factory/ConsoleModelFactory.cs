using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Command.Parser;
using UnityDevConsole.Models.Console;

public static class ConsoleModelFactory
{
    public static ConsoleModel Create ()
    {
        return new ConsoleModel(
            new ConsoleOutputModel(),
            new CommandRunnerModel(
                new CommandsCollection(new ConsoleCommandFactory()),
                new TypeParserModel()
            )
        );
    }
}
