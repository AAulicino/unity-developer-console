using System;
using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleModel : IConsoleModel
    {
        public event Action<string> OnOutputUpdate;

        readonly IConsoleOutputModel output;
        readonly ICommandRunnerModel commandRunner;

        public bool Enabled { get; }

        public ConsoleModel (IConsoleOutputModel output, ICommandRunnerModel commandRunner)
        {
            this.commandRunner = commandRunner;
            this.output = output;

            output.OnContentUpdate += x => OnOutputUpdate?.Invoke(x);
        }

        public void Submit (string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // CommandSuggestionsHandler.RegisterInputToHistory(inputField.text);

            Log("> " + text);
            Log(commandRunner.ExecuteCommand(text));
        }

        public void Log (object obj) => output.Append(obj.ToString());
        public void ClearOutput () => output.Clear();
    }
}
