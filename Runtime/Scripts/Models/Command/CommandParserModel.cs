using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityDevConsole.Models.Command.Parser;

namespace UnityDevConsole.Models.Command
{
    public class CommandParserModel : ICommandParser
    {
        readonly ITypeParserModel typeParser;

        public CommandParserModel (ITypeParserModel typeParser)
        {
            this.typeParser = typeParser;
        }

        public object[] ParseArgs (ICommandModel command, string[] args)
        {
            object[] parsedArgs = new object[command.Parameters.Length];

            for (int i = 0; i < command.Parameters.Length; i++)
            {
                if (i >= args.Length)
                {
                    parsedArgs[i] = Type.Missing;
                    continue;
                }

                try
                {
                    parsedArgs[i] = typeParser.Parse(args[i], command.Parameters[i].ParameterType);
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

            return parsedArgs;
        }

        public (string command, string[] args) ParseCommand (string input)
        {
            /*
                Splits by white spaces and preserve things between quotes.
                Example:
                    "MyCommand arg1 \"arg 2\"", becomes string[] {"MyCommand", "arg1", "arg 2"}.
            */
            string[] tokens = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
              .Cast<Match>()
              .Select(m => m.Value.Trim('"'))
              .ToArray();

            return (tokens.First(), tokens.Skip(1).ToArray());
        }
    }
}
