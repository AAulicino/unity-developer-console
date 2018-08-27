using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityDeveloperConsole
{
	public static class CommandsHandler
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

		public static object ExecuteCommand (string unparsedCommand)
		{
			if (string.IsNullOrEmpty(unparsedCommand))
				return null;

			//Splits by white spaces and preserve things between quotes. Example: Input=MyCommand arg1 "arg 2",
			//becomes string[] {"MyCommand", "arg1", "arg 2"}.
			string[] tokens = Regex.Matches(unparsedCommand, @"[\""].+?[\""]|[^ ]+")
			  .Cast<Match>()
			  .Select(m => m.Value.Trim('"'))
			  .ToArray();

			string commandName = tokens.First();
			string[] args = tokens.Skip(1).ToArray();

			return ExecuteCommand(commandName, args);
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
							Debug.LogException(ex);
							return string.Format("[DeveloperConsole] Failed to parse argument {0} of type: {1}. Expected: {2}",
									args[i], args[i].GetType(), command.Parameters[i].ParameterType);
						}

						invokeParams.Add(parsedObject);
					}
					else
						invokeParams.Add(Type.Missing);
				}

				try
				{
					return command.Method.Invoke(command.Context, invokeParams.ToArray());
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					return "[DeveloperConsole] Exception occurred during execution of command. Ex: " + ex;
				}
			}

			return "Unknown command. " + commandName;
		}
	}
}