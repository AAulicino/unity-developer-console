using UnityDevConsole.Models.Console;

namespace UnityDevConsole.Controllers.Input
{
    public static class ConsoleInputDetectorModelFactory
    {
        public static ConsoleInputDetectorModel Create (
            ICoroutineRunner runner,
            IConsoleStateProvider console,
            IConsoleSettings settings
        )
        {
            return new ConsoleInputDetectorModel(runner, new UnityInput(), console, settings);
        }
    }
}
