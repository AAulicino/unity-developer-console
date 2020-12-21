using System;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleModel : IConsoleStateProvider
    {
        event Action<string> OnOutputUpdate;

        void ClearOutput ();
        void Log (object obj);
        void Submit (string text);
    }
}
