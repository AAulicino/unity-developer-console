using System;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleStateProvider
    {
        event Action<bool> OnEnableChange;

        bool Enabled { get; set; }
    }
}
