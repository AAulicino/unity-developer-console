using System;
using System.Threading;
using UnityDevConsole;
using UnityDevConsole.Models.Console;
using UnityEngine;

public static class DeveloperConsole
{
    static ConsoleModel model;

    public static void Initialize ()
    {
        model = ConsoleModelFactory.Create();
    }

    // public static void ClearConsoleOutput () => model?.ClearOutput();

    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // static void Initialize ()
    // {
    //     //For it not to slow down the project's initialization in case of huge amounts of commands
    //     ThreadPool.QueueUserWorkItem((x) =>
    //     {
    //         Command[] commands = CommandsHandler.LoadCompileTimeCommands();
    //         CommandSuggestionsHandler.RegisterCommands(commands);
    //     });
    // }

    // public static void RegisterRuntimeCommand (string commandName, string methodName, object context, bool developerOnly, bool hidden)
    // {
    //     CommandsHandler.RegisterRuntimeCommand(commandName, methodName, context, developerOnly, hidden);
    // }

    // public static void UnregisterRuntimeCommand (string commandName)
    // {
    //     CommandsHandler.UnregisterRuntimeCommand(commandName);
    // }

    // public static object ExecuteCommand (string commandName, string[] args)
    // {
    //     return CommandsHandler.ExecuteCommand(commandName, args);
    // }

    public static void Log (object message) => model.Log(message);
}
