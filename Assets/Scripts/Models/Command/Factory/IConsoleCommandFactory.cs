using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityDevConsole.Models.Command
{
    public interface IConsoleCommandFactory
    {
        Command Create (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        );

        Dictionary<string, Command> CreateFromAssemblies (string[] assemblies);
    }
}
