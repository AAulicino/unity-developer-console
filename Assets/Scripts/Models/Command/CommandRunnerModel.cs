using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityDevConsole.Models.Command.Parser;
using UnityEngine;

namespace UnityDevConsole.Models.Command
{
    public class CommandRunnerModel : ICommandRunnerModel
    {
        readonly ICommandsCollection commandsCollection;
        readonly ITypeParserModel typeParser;

        public CommandRunnerModel (
            ICommandsCollection commandsCollection,
            ITypeParserModel typeParser
        )
        {
            this.commandsCollection = commandsCollection;
            this.typeParser = typeParser;
        }

        public object ExecuteCommand (string unparsedCommand)
        {
            if (string.IsNullOrEmpty(unparsedCommand))
                return null;

            /*
                Splits by white spaces and preserve things between quotes.
                Example:
                    Input=MyCommand arg1 "arg 2", becomes string[] {"MyCommand", "arg1", "arg 2"}.
            */
            string[] tokens = Regex.Matches(unparsedCommand, @"[\""].+?[\""]|[^ ]+")
              .Cast<Match>()
              .Select(m => m.Value.Trim('"'))
              .ToArray();

            string commandName = tokens.First();
            string[] args = tokens.Skip(1).ToArray();

            return ExecuteCommand(commandName, args);
        }

        public object ExecuteCommand (string commandName, string[] args)
        {
            if (!commandsCollection.Commands.TryGetValue(commandName, out Command command))
                return "Unknown command. " + commandName;

            try
            {
                return command.Method.Invoke(command.Context, GetInvokeParams(command, args));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return "[DeveloperConsole] Command Failed. Ex: " + ex;
            }
        }

        object[] GetInvokeParams (Command command, string[] args)
        {
            object[] invokeParams = new object[command.Parameters.Length];

            for (int i = 0; i < command.Parameters.Length; i++)
            {
                if (i >= args.Length)
                {
                    invokeParams[i] = Type.Missing;
                    continue;
                }

                try
                {
                    invokeParams[i] = typeParser.Parse(args[i], command.Parameters[i].ParameterType);
                }
                catch (FormatException inner)
                {
                    throw new FormatException(
                        $"[DeveloperConsole] Failed to parse argument {args[i]} "
                        + $"of type: {args[i].GetType()}. "
                        + $"Expected: {command.Parameters[i].ParameterType}",
                        inner
                    );
                }

            }

            return invokeParams;
        }
    }
}
