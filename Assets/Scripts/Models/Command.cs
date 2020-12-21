using System;
using System.Linq;
using System.Reflection;

namespace UnityDeveloperConsole.Models
{
    public class Command : ICommand
    {
        public string Name { get; }
        public string FullName { get; }
        public MethodInfo Method { get; }
        public ParameterInfo[] Parameters { get; }
        public object Context { get; }
        public bool Indexed { get; }
        public bool DeveloperOnly { get; }

        public Command (ConsoleCommandAttribute attribute, MethodInfo method) :
            this(attribute.CommandName, method, attribute.DeveloperOnly, attribute.Hidden)
        {

        }

        public Command (
            string commandName,
            MethodInfo method,
            object context,
            bool developerOnly,
            bool indexed
        )
            : this(commandName, method, developerOnly, indexed)
        {
            Context = context;
        }

        Command (string commandName, MethodInfo method, bool developerOnly, bool indexed)
        {
            Name = commandName;
            Method = method;
            Parameters = method.GetParameters();
            Indexed = indexed;
            DeveloperOnly = developerOnly;
            FullName = $"commandName {string.Join(" ", Parameters.Select(x => x.Name).ToArray())}";
        }

        public int CompareTo (Command other) => Name.CompareTo(other);
    }
}
