using System;
using System.Linq;
using System.Reflection;

namespace UnityDeveloperConsole
{
	public class Command : IComparable<Command>
	{
		public readonly string Name;
		public readonly string FullName;
		public readonly MethodInfo Method;
		public readonly ParameterInfo[] Parameters;
		public readonly object Context;
		public readonly bool Indexed;
		public readonly bool DeveloperOnly;

		public Command (ConsoleCommandAttribute attribute, MethodInfo method) :
			this(attribute.CommandName, method, attribute.DeveloperOnly, attribute.Indexed)
		{

		}

		public Command (string commandName, MethodInfo method, object context, bool developerOnly, bool indexed)
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
			FullName = commandName + string.Join("", Parameters.Select(x => x.Name).ToArray());
		}

		public int CompareTo (Command other)
		{
			return Name.CompareTo(other);
		}
	}
}