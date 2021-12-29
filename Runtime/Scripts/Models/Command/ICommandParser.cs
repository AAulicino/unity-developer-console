namespace UnityDevConsole.Models.Command
{
    public interface ICommandParser
    {
        object[] ParseArgs (ICommandModel command, string[] args);
        (string command, string[] args) ParseCommand (string input);
    }
}
