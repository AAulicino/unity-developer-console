using System;
using System.Linq;
using System.Reflection;

namespace UnityDevConsole.Models.Command
{
    public class Command : ICommand
    {
        public string Name { get; }
        public string FullName { get; }
        public MethodInfo Method { get; }
        public ParameterInfo[] Parameters { get; }
        public object Context { get; }
        public bool Hidden { get; }
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
            bool hidden
        )
            : this(commandName, method, developerOnly, hidden)
        {
            Context = context;
        }

        Command (string commandName, MethodInfo method, bool developerOnly, bool hidden)
        {
            Name = commandName;
            Method = method;
            Parameters = method.GetParameters();
            Hidden = hidden;
            DeveloperOnly = developerOnly;
            FullName = $"commandName {string.Join(" ", Parameters.Select(x => x.Name).ToArray())}";
        }

        public int CompareTo (Command other) => Name.CompareTo(other);
    }
}
