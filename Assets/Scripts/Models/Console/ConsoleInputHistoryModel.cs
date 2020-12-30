using System.Collections.Generic;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleInputHistoryModel : IConsoleInputHistoryModel
    {
        readonly List<string> inputHistory;
        readonly IConsoleSettings settings;

        public IReadOnlyList<string> InputHistory => inputHistory;

        public ConsoleInputHistoryModel (IConsoleSettings settings)
        {
            this.settings = settings;
            inputHistory = new List<string>(settings.HistorySize);
        }

        public void Add (string text)
        {
            if (inputHistory.Count >= settings.HistorySize)
                inputHistory.RemoveAt(0);

            inputHistory.Remove(text);
            inputHistory.Add(text);
        }
    }
}
