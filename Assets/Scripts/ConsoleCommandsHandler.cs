using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityDeveloperConsole
{
	public static class ConsoleCommandsHandler
	{
		readonly static Dictionary<string, Command> consoleCommandsDictionary = new Dictionary<string, Command>();

		public static Command[] LoadCompileTimeCommands ()
		{
			IEnumerable<MethodInfo> methods = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => a.FullName.Contains("Assembly-CSharp"))
				.SelectMany(a => a.GetTypes())
				.SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));

			foreach (MethodInfo method in methods)
			{
				foreach (object consoleCommand in method.GetCustomAttributes(typeof(ConsoleCommandAttribute), false))
				{
					ConsoleCommandAttribute consoleAttr = (ConsoleCommandAttribute)consoleCommand;

					if (consoleCommandsDictionary.ContainsKey(consoleAttr.CommandName))
						Debug.LogWarning("[UnityDeveloperConsole] Duplicate command found. Command Name:" + consoleAttr.CommandName);
					else
						consoleCommandsDictionary.Add(consoleAttr.CommandName, new Command(consoleAttr, method));
				}
			}
			return consoleCommandsDictionary.Values.ToArray();
		}

		public static void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool indexed)
		{
			MethodInfo method = context.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (consoleCommandsDictionary.ContainsKey(commandName))
				Debug.LogWarning("[UnityDeveloperConsole] Failed to inser runtime command. Reason: duplicate command name.");
			else
				consoleCommandsDictionary.Add(commandName, new Command(commandName, method, context, developerOnly, indexed));
		}

		public static void UnregisterRuntimeCommand (string commandName)
		{
			consoleCommandsDictionary.Remove(commandName);
		}

		public static object ExecuteCommand (string commandName, string[] args)
		{
			Command command;

			if (consoleCommandsDictionary.TryGetValue(commandName, out command))
			{
				List<object> invokeParams = new List<object>(command.Parameters.Length);

				for (int i = 0; i < command.Parameters.Length; i++)
				{
					if (args.Length < i)
					{
						object parsedObject;

						try
						{
							parsedObject = TypeParser.Parse(args[i], command.Parameters[i].ParameterType);
						}
						catch (Exception ex)
						{
							if (ConsoleUI.Instance != null)
							{
								ConsoleUI.Instance.Log(string.Format("[DeveloperConsole] Failed to parse argument {0} of type: {1}. Expected: {2}",
									args[i], args[i].GetType(), command.Parameters[i].ParameterType));
							}

							Debug.LogException(ex);
							return null;
						}

						invokeParams.Add(parsedObject);
					}
					else
						invokeParams.Add(Type.Missing);
				}

				return command.Method.Invoke(command.Context, invokeParams.ToArray());
			}

			return "Unknown command. " + commandName;
		}
	}
}