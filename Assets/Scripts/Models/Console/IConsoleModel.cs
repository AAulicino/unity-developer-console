using System;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleModel : IConsoleStateProvider
    {
        event Action<string> OnOutputUpdate;

        void Initialize ();
        void ClearOutput ();
        void Log (object obj);
        void Submit (string text);
        void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool hidden);
        void UnregisterRuntimeCommand (string commandName);
        object ExecuteCommand (string commandName, string[] args);
    }
}
