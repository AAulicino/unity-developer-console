using System;
using UnityDevConsole.Models.Console;

namespace UnityDevConsole.Controllers.Input
{
    public interface IConsoleInputDetectorModel : IDisposable
    {
        event Action OnToggleVisibility;
        event Action OnSubmit;
        event Action OnMoveUp;
        event Action OnMoveDown;
        event Action OnEscape;

        void Initialize (IConsoleStateProvider console);
    }
}
