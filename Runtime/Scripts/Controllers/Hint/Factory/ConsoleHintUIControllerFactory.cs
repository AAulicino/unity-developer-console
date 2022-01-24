using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Settings;
using UnityDevConsole.Views;
using UnityDevConsole.Views.Hint;

namespace UnityDevConsole.Controllers.Hint.Factory
{
    public static class ConsoleHintUIControllerFactory
    {
        public static ConsoleHintUIController Create (
            IConsoleHintModel model,
            IHintUIView hintView,
            IConsoleUIView consoleView,
            IConsoleInputDetectorModel input,
            IConsoleSettings settings
        )
        {
            return new ConsoleHintUIController(
                model,
                hintView,
                consoleView,
                new ConsoleHintEntryUIViewFactory(),
                input,
                settings
            );
        }
    }
}
