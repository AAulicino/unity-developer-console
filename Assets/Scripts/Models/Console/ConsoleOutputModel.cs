using System;
using System.Text;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleOutputModel : IConsoleOutputModel
    {
        public event Action<string> OnContentUpdate;

        readonly StringBuilder content = new StringBuilder();

        public void WriteLine (string text)
        {
            content.AppendLine(text);
            OnContentUpdate(content.ToString());
        }

        public void Clear ()
        {
            content.Clear();
            OnContentUpdate("");
        }
    }
}
