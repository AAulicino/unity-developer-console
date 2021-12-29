namespace UnityDevConsole.Models.Command
{
    public interface ICommandRunnerModel
    {
        object ExecuteCommand (string unparsedCommand);
        object ExecuteCommand (string commandName, string[] args);
    }
}
