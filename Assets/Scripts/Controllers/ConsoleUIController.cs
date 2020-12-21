using UnityDevConsole.Views;

namespace UnityDevConsole.Controllers.Console
{
    public class ConsoleUIController
    {
        readonly IConsoleUIView view;

        public ConsoleUIController (IConsoleUIView view)
        {
            this.view = view;
        }
    }
}
