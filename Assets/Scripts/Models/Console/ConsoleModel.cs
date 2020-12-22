using System;
using System.Collections.Generic;
using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Console.Hint;

namespace UnityDevConsole.Models.Console
{
    public class ConsoleModel : IConsoleModel
    {
        public event Action<bool> OnEnableChange;
        public event Action<string> OnOutputUpdate;

        readonly ICommandsCollection commandsCollection;
        readonly ICommandRunnerModel commandRunner;
        readonly IConsoleOutputModel output;
        readonly IConsoleInputHistoryModel inputHistory;
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

        public IReadOnlyList<string> InputHistory => inputHistory.InputHistory;

        public ConsoleModel (
            ICommandsCollection commandsCollection,
            ICommandRunnerModel commandRunner,
            IConsoleOutputModel output,
            IConsoleInputHistoryModel inputHistory
        )
        {
            this.commandsCollection = commandsCollection;
            this.commandRunner = commandRunner;
            this.output = output;
            this.inputHistory = inputHistory;
            output.OnContentUpdate += x => OnOutputUpdate?.Invoke(x);
        }

        public void Submit (string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            Log("> " + text);
            inputHistory.Add(text);
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
