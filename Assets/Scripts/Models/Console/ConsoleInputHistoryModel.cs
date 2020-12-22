using System.Collections.Generic;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleInputHistoryModel : IConsoleInputHistoryModel
    {
        readonly List<string> inputHistory = new List<string>(10);

        public IReadOnlyList<string> InputHistory => inputHistory;

        public void Add (string text)
        {
            if (inputHistory.Count >= 10)
                inputHistory.RemoveAt(0);

            inputHistory.Remove(text);
            inputHistory.Add(text);
        }
    }
}
