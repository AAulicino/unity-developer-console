using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace UnityDeveloperConsole
{
	public class ConsoleCommandsHandler
	{
		Dictionary<string, Command> consoleCommandsDictionary = new Dictionary<string, Command>();

		public ConsoleCommandsHandler ()
		{
			IEnumerable<MethodInfo> methods = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => a.FullName.Contains("Assembly-CSharp"))
				.SelectMany(a => a.GetTypes())
				.SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));

			//For it not to slow down the project's initialiation in case of huge amounts of commands
			ThreadPool.QueueUserWorkItem((x) =>
			{
				foreach (MethodInfo method in methods)
				{
					foreach (object consoleCommand in method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false))
					{
						ConsoleCommandAttribute consoleAttr = (ConsoleCommandAttribute)consoleCommand;

						if (consoleCommandsDictionary.ContainsKey(consoleAttr.CommandName))
							Debug.LogWarning("Duplicate command found. Command Name:" + consoleAttr.CommandName);
						else
							consoleCommandsDictionary.Add(consoleAttr.CommandName, new Command(consoleAttr, method));
					}
				}
			});
		}

		public void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool indexed)
		{
			MethodInfo method = context.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			consoleCommandsDictionary.Add(commandName, new Command(commandName, method, context, developerOnly, indexed));
		}

		public void ExecuteCommand (string commandName, string[] args)
		{
			Command command;

			if (consoleCommandsDictionary.TryGetValue(commandName, out command))
			{
				List<object> invokeParams = new List<object>(command.Parameters.Length);

				foreach (ParameterInfo parameter in command.Parameters)
					invokeParams.Add(Convert.ChangeType(args, parameter.ParameterType));

				command.Method.Invoke(command.Context, invokeParams.ToArray());
			}
		}
	}
}