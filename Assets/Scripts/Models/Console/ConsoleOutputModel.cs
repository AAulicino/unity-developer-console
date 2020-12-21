using System;
using System.Text;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleOutputModel : IConsoleOutputModel
    {
        public event Action<string> OnContentUpdate;

        readonly StringBuilder content = new StringBuilder();

        public void Append (string text)
        {
            content.AppendLine(text);
            OnContentUpdate(text);
        }

        public void Clear ()
        {
            content.Clear();
            OnContentUpdate("");
        }
    }
}
