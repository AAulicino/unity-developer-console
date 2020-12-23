using System;
using System.Linq;
using System.Reflection;

namespace UnityDevConsole.Models.Command
{
    public class Command : ICommand
    {
        readonly MethodInfo method;
        readonly object context;

        public string Name { get; }
        public string FullName { get; }
        public ParameterInfo[] Parameters { get; }
        public bool Hidden { get; }
        public bool DeveloperOnly { get; }

        public Command (ConsoleCommandAttribute attribute, MethodInfo method)
            : this(attribute.CommandName, method, attribute.DeveloperOnly, attribute.Hidden)
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
            this.context = context;
        }

        Command (string commandName, MethodInfo method, bool developerOnly, bool hidden)
        {
            Name = commandName;
            this.method = method;
            Parameters = method.GetParameters();
            Hidden = hidden;
            DeveloperOnly = developerOnly;

            string parameters = string.Join(" ", Parameters.Select(x => x.Name).ToArray());
            FullName = $"{commandName} <color=#ff8575>{parameters}</color>";
        }

        public int CompareTo (Command other) => Name.CompareTo(other);

        public object Invoke (object[] parameters) => method.Invoke(context, parameters);
    }
}
