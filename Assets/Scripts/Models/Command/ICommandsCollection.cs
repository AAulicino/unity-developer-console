using System.Collections.Generic;

namespace UnityDevConsole.Models.Command
{
    public interface ICommandsCollection
    {
        IReadOnlyDictionary<string, Command> Commands { get; }

        void Initialize ();

        void RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        );

        void UnregisterRuntimeCommand (string commandName);
    }
}
