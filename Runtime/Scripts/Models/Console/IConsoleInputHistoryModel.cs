using System.Collections.Generic;

namespace UnityDevConsole.Models.Console
{
    public interface IConsoleInputHistoryModel
    {
        IReadOnlyList<string> InputHistory { get; }
        void Add (string text);
    }
}
