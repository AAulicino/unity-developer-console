using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandsCollection : ICommandsCollection
    {
        readonly Dictionary<string, Command> registeredCommands = new Dictionary<string, Command>();
        readonly IConsoleCommandFactory commandFactory;

        public IReadOnlyDictionary<string, Command> Commands => registeredCommands;

        public CommandsCollection (IConsoleCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
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

        public void UnregisterRuntimeCommand (string commandName)
        {
            registeredCommands.Remove(commandName);
        }
    }
}
