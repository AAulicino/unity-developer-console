using System.Collections.Generic;

namespace UnityDevConsole.Models.Command
{
    public interface IConsoleCommandFactory
    {
        ICommandModel Create (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        );

        IReadOnlyDictionary<string, ICommandModel> CreateFromAssemblies (string[] assemblies);
    }
}
