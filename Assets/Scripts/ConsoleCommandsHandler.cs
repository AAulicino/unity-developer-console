using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Threading;

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

		public void RegisterRuntimeCommand (string commandName, Action method, bool isDeveloper, bool indexed)
		{

		}

		public void ExecuteCommand (string commandName, string[] args)
		{
			Command command;

			if(consoleCommandsDictionary.TryGetValue(commandName, out command))
			{
				
			}
		}
	}
}