using UnityDevConsole.Models.Command;

namespace UnityDevConsole.Tests.Command
{
    public class TestCommandParser : ICommandParser
    {
        public object[] ParseArgsReturnValue { get; set; }
        public (string command, string[] args) ParseCommandReturnValue { get; set; }

        public object[] ParseArgs (ICommandModel command, string[] args)
        {
            return ParseArgsReturnValue;
        }

        public (string command, string[] args) ParseCommand (string input)
        {
            return ParseCommandReturnValue;
        }
    }
}
