using System;
using System.Threading;
using UnityDeveloperConsole;
using UnityEngine;

public static class DeveloperConsole
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void Initialize ()
	{
		//For it not to slow down the project's initialiation in case of huge amounts of commands
		ThreadPool.QueueUserWorkItem((x) =>
		{
			Command[] commands = ConsoleCommandsHandler.LoadCompileTimeCommands();
			CommandSuggestionsHandler.RegisterCommands(commands);
		});
	}

	public static void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool indexed)
	{
		ConsoleCommandsHandler.RegisterRuntimeCommand(commandName, methodName, context, developerOnly, indexed);
	}

	public static void UnregisterRuntimeCommand (string commandName)
	{
		ConsoleCommandsHandler.UnregisterRuntimeCommand(commandName);
	}

	public static object ExecuteCommand (string commandName, string[] args)
	{
		return ConsoleCommandsHandler.ExecuteCommand(commandName, args);
	}

	internal static void Log (object message)
	{
		ConsoleUI.Instance.Log(message);
	}
}