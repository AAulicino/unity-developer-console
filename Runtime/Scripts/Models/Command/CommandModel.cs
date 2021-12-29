﻿using System.Linq;
using System.Reflection;

namespace UnityDevConsole.Models.Command
{
    public class CommandModel : ICommandModel
    {
        readonly MethodInfo method;
        readonly object context;

        public string Name { get; }
        public string Description { get; }
        public ParameterInfo[] Parameters { get; }
        public bool Hidden { get; }
        public bool DeveloperOnly { get; }

        public CommandModel (ConsoleCommandAttribute attribute, MethodInfo method)
            : this(attribute.CommandName, method, attribute.DeveloperOnly, attribute.Hidden)
        {

        }

        public CommandModel (
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

        CommandModel (string commandName, MethodInfo method, bool developerOnly, bool hidden)
        {
            Name = commandName;
            this.method = method;
            Parameters = method.GetParameters();
            Hidden = hidden;
            DeveloperOnly = developerOnly;

            string parameters = string.Join(" ", Parameters.Select(x => x.Name).ToArray());
            Description = $"{commandName} <color=#ff8575>{parameters}</color>";
        }

        public int CompareTo (CommandModel other) => Name.CompareTo(other);

        public object Invoke (object[] parameters) => method.Invoke(context, parameters);
    }
}
