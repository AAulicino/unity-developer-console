using System.Reflection;

namespace UnityDeveloperConsole
{
	public class Command
	{
		public readonly string Name;
		public readonly MethodInfo Method;
		public readonly bool Indexed;
		public readonly bool DeveloperOnly;

		public Command (ConsoleCommandAttribute attribute, MethodInfo method)
		{
			Name = attribute.CommandName;
			Method = method;
			Indexed = attribute.Indexed;
			DeveloperOnly = attribute.DeveloperOnly;
		}
	}
}
