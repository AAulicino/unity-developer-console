namespace UnityDevConsole.Models.Console
{
    public static class ConsoleInputHistoryModelFactory
    {
        public static IConsoleInputHistoryModel Create ()
        {
            return new ConsoleInputHistoryModel();
        }
    }
}
