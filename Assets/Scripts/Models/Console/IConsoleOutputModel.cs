using System;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleOutputModel
    {
        event Action<string> OnContentUpdate;

        void Append (string text);
        void Clear ();
    }
}
