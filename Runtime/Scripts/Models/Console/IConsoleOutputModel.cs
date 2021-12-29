using System;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleOutputModel
    {
        event Action<string> OnContentUpdate;

        void WriteLine (string text);
        void Clear ();
    }
}
