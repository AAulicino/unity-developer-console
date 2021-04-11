using System.Collections.Generic;
using UnityDevConsole.Models.Command;

namespace UnityDeveloperConsole.Tests.Command.Factory
{
    public class TestConsoleCommandFactory : IConsoleCommandFactory
    {
        public TestCommandModel CreateCommand { get; set; }
        public Dictionary<string, ICommandModel> CreateFromAssembliesCommand { get; set; }

        public TestConsoleCommandFactory ()
        {
            CreateCommand = new TestCommandModel();
            CreateFromAssembliesCommand = new Dictionary<string, ICommandModel>();
        }

        public ICommandModel Create (
            string commandName,
            string methodName,
            object context,
            bool developerOnly,
            bool hidden
        )
        {
            return CreateCommand;
        }

        public IReadOnlyDictionary<string, ICommandModel> CreateFromAssemblies (string[] assemblies)
        {
            return CreateFromAssembliesCommand;
        }
    }
}
