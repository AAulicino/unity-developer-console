using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityDevConsole.Settings;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandsCollectionModel : ICommandsCollectionModel
    {
        readonly object _lock = new object();

        readonly Dictionary<string, ICommandModel> registeredCommands
            = new Dictionary<string, ICommandModel>();

        readonly IConsoleSettings settings;
        readonly IConsoleCommandFactory commandFactory;

        public IReadOnlyDictionary<string, ICommandModel> Commands => registeredCommands;

        public CommandsCollectionModel (
            IConsoleSettings settings,
            IConsoleCommandFactory commandFactory
        )
        {
            this.settings = settings;
            this.commandFactory = commandFactory;
        }

        public void Initialize ()
        {
            try
            {
                IReadOnlyDictionary<string, ICommandModel> commands = commandFactory
                    .CreateFromAssemblies(settings.Assemblies);

                lock (_lock)
                {
                    foreach (KeyValuePair<string, ICommandModel> command in commands)
                        registeredCommands.Add(command.Key, command.Value);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public bool RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            ICommandModel command = commandFactory.Create(
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
                    return false;
                }
                registeredCommands.Add(commandName, command);
                return true;
            }
        }

        public void UnregisterRuntimeCommand (string commandName)
        {
            lock (_lock)
                registeredCommands.Remove(commandName);
        }
    }
}
