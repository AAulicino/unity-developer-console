using System.Collections.Generic;

namespace UnityDevConsole.Models.Command
{
    public interface ICommandsCollectionModel
    {
        IReadOnlyDictionary<string, ICommandModel> Commands { get; }

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
