using UnityDevConsole.Settings;

namespace UnityDevConsole.Models.Console
{
    public static class ConsoleInputHistoryModelFactory
    {
        public static IConsoleInputHistoryModel Create (IConsoleSettings settings)
        {
            return new ConsoleInputHistoryModel(settings);
        }
    }
}
