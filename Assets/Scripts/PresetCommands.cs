public static class CommandsUtils
{
    [ConsoleCommand("cls")]
    [ConsoleCommand("clear")]
    static void ClearConsoleOutput ()
    {
        DeveloperConsole.Clear();
    }
}
