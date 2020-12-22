using System;
using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleModel : IConsoleModel
    {
        public event Action<bool> OnEnableChange;
        public event Action<string> OnOutputUpdate;

        readonly IConsoleOutputModel output;
        readonly ICommandsCollection commandsCollection;
        readonly ICommandRunnerModel commandRunner;

        bool _enabled;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnEnableChange?.Invoke(value);
            }
        }

        public ConsoleModel (
            IConsoleOutputModel output,
            ICommandsCollection commandsCollection,
            ICommandRunnerModel commandRunner
        )
        {
            this.commandRunner = commandRunner;
            this.output = output;
            this.commandsCollection = commandsCollection;
            output.OnContentUpdate += x => OnOutputUpdate?.Invoke(x);
        }

        public void Initialize ()
        {
            commandsCollection.Initialize();
        }

        public void Submit (string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // CommandSuggestionsHandler.RegisterInputToHistory(inputField.text);

            Log("> " + text);
            Log(commandRunner.ExecuteCommand(text));
        }

        public void Log (object obj) => output.WriteLine(obj?.ToString());
        public void ClearOutput () => output.Clear();

        public void RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            commandsCollection.RegisterRuntimeCommand(
                commandName,
                methodName,
                context,
                developerOnly,
                hidden
            );
        }

        public void UnregisterRuntimeCommand (string commandName)
            => commandsCollection.UnregisterRuntimeCommand(commandName);

        public object ExecuteCommand (string commandName, string[] args)
            => commandRunner.ExecuteCommand(commandName, args);
    }
}
