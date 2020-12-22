using System;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Views;

namespace UnityDevConsole.Controllers.Console
{
    public static class ConsoleUIControllerFactory
    {
        public static ConsoleUIController Create (
            IConsoleModel model,
            IConsoleUIView view,
            IConsoleInputDetectorModel inputDetector
        )
        {
            return new ConsoleUIController(
                model,
                view,
                inputDetector
            );
        }
    }
}
