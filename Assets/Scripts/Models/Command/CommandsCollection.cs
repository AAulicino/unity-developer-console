using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandsCollection : ICommandsCollection
    {
        readonly object _lock = new object();

        readonly Dictionary<string, Command> registeredCommands = new Dictionary<string, Command>();
        readonly IConsoleCommandFactory commandFactory;

        public IReadOnlyDictionary<string, Command> Commands => registeredCommands;

        public CommandsCollection (IConsoleCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void Initialize ()
        {
            Task.Run(() =>
            {
                try
                {
                    Dictionary<string, Command> commands = commandFactory
                        .CreateFromAssemblies(new[] { "Assembly-CSharp" });

                    lock (_lock)
                    {
                        foreach (KeyValuePair<string, Command> command in commands)
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
            Command command = commandFactory.Create(
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
