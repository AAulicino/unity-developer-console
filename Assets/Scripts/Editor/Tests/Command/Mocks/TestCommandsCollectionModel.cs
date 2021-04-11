using System;
using System.Collections.Generic;
using UnityDevConsole.Models.Command;

namespace UnityDeveloperConsole.Tests.Command
{
    public class TestCommandsCollectionModel : ICommandsCollectionModel
    {
        public Dictionary<string, ICommandModel> Commands = new Dictionary<string, ICommandModel>();
        IReadOnlyDictionary<string, ICommandModel> ICommandsCollectionModel.Commands => Commands;

        public void Initialize ()
        {
            throw new NotImplementedException();
        }

        public bool RegisterRuntimeCommand (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            throw new NotImplementedException();
        }

        public void UnregisterRuntimeCommand (string commandName)
        {
            throw new NotImplementedException();
        }
    }
}
