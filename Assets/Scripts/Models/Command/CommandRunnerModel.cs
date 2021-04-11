using System;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandRunnerModel : ICommandRunnerModel
    {
        readonly ICommandsCollectionModel commandsCollection;
        readonly ICommandParser commandParser;

        public CommandRunnerModel (
            ICommandsCollectionModel commandsCollection,
            ICommandParser commandParser
        )
        {
            this.commandsCollection = commandsCollection;
            this.commandParser = commandParser;
        }

        public object ExecuteCommand (string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
                return null;

            (string command, string[] args) = commandParser.ParseCommand(commandText);
            return ExecuteCommand(command, args);
        }

        public object ExecuteCommand (string commandName, string[] args)
        {
            if (!commandsCollection.Commands.TryGetValue(commandName, out ICommandModel command))
                return "Unknown command. " + commandName;

            try
            {
                return command.Invoke(commandParser.ParseArgs(command, args));
            }
            catch (ArgumentException)
            {
                return "Command Failed. Incorrect parameters usage.";
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return "Command Failed. Ex: " + ex.Message;
            }
        }
    }
}
