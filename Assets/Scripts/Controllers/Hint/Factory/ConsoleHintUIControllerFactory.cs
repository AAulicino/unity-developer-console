using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console.Hint;
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
            IConsoleHintEntryUIViewFactory entryFactory,
            IConsoleInputDetectorModel input
        )
        {
            return new ConsoleHintUIController(model, hintView, consoleView, entryFactory, input);
        }
    }
}
