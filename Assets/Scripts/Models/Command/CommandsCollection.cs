using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityDevConsole.Settings;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandsCollection : ICommandsCollection
    {
        readonly object _lock = new object();

        readonly Dictionary<string, ICommand> registeredCommands = new Dictionary<string, ICommand>();
        readonly IConsoleSettings settings;
        readonly IConsoleCommandFactory commandFactory;

        public IReadOnlyDictionary<string, ICommand> Commands => registeredCommands;

        public CommandsCollection (IConsoleSettings settings, IConsoleCommandFactory commandFactory)
        {
            this.settings = settings;
            this.commandFactory = commandFactory;
        }

        public void Initialize ()
        {
            Task.Run(() =>
            {
                try
                {
                    Dictionary<string, ICommand> commands = commandFactory
                        .CreateFromAssemblies(settings.Assemblies);

                    lock (_lock)
                    {
                        foreach (KeyValuePair<string, ICommand> command in commands)
                            registeredCommands.Add(command.Key, command.Value);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            });
        }

        public void RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            ICommand command = commandFactory.Create(
                commandName,
                methodName,
                context,
                developerOnly,
                hidden
            );

            lock (_lock)
            {
                if (registeredCommands.ContainsKey(commandName))
                {
                    Debug.LogError(
                        "[UnityDevConsole] Failed to insert runtime command. "
                        + "Reason: duplicate command name."
                    );
                }
                else
                    registeredCommands.Add(commandName, command);
            }
        }

        public void UnregisterRuntimeCommand (string commandName)
        {
            lock (_lock)
                registeredCommands.Remove(commandName);
        }
    }
}
