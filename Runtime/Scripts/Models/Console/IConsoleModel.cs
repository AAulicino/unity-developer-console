using System;
using System.Collections.Generic;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleModel : IConsoleStateProvider
    {
        IReadOnlyList<string> InputHistory { get; }

        event Action<string> OnOutputUpdate;

        void ClearOutput ();
        void Log (object obj);
        void Submit (string text);
        void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool hidden);
        void UnregisterRuntimeCommand (string commandName);
        object ExecuteCommand (string commandName, string[] args);
    }
}
