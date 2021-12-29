using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityDevConsole.Models.Command
{
    public interface ICommandsCollectionModel
    {
        IReadOnlyDictionary<string, ICommandModel> Commands { get; }

        void Initialize ();

        bool RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        );

        void UnregisterRuntimeCommand (string commandName);
    }
}
