using System.Collections.Generic;

namespace UnityDevConsole.Models.Command
{
    public interface IConsoleCommandFactory
    {
        ICommand Create (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        );

        Dictionary<string, ICommand> CreateFromAssemblies (string[] assemblies);
    }
}
