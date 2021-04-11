using System.Threading.Tasks;
using UnityDevConsole.Controllers.Console;
using UnityDevConsole.Controllers.Hint.Factory;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Settings;
using UnityDevConsole.Views;
using UnityEngine;

public static class DeveloperConsole
{
    static IConsoleModel model;

    [RuntimeInitializeOnLoadMethod]
    static void AutoInitialize ()
    {
        if (ConsoleSettings.Instance.AutoInitialize)
            Initialize();
    }

    public static void Initialize ()
    {
        if (model != null)
            return;

        IConsoleSettings settings = ConsoleSettings.Instance;
        IConsoleInputHistoryModel historyModel = ConsoleInputHistoryModelFactory.Create(settings);
        ICommandsCollectionModel commandsCollection = CommandsCollectionFactory.Create(settings);
        model = ConsoleModelFactory.Create(historyModel, commandsCollection);

        IConsoleHintModel hintModel = ConsoleHintModelFactory.Create(
            historyModel,
            commandsCollection,
            settings
        );

        ConsoleUIView view = ConsoleUIViewFactory.Create();
        IConsoleInputDetectorModel inputDetector = ConsoleInputDetectorModelFactory.Create(
            view,
            model,
            settings
        );

        ConsoleUIControllerFactory.Create(model, view, inputDetector, hintModel);
        ConsoleHintUIControllerFactory.Create(
            hintModel,
            view.HintUI,
            view,
            inputDetector
        );
        Task.Run(commandsCollection.Initialize);
        inputDetector.Initialize();
    }

    public static void Clear () => model?.ClearOutput();

    public static void RegisterRuntimeCommand (
        string commandName,
        string methodName,
        object context,
        bool developerOnly,
        bool hidden
    )
    {
        model.RegisterRuntimeCommand(commandName, methodName, context, developerOnly, hidden);
    }

    public static void UnregisterRuntimeCommand (string commandName)
        => model.UnregisterRuntimeCommand(commandName);

    public static object ExecuteCommand (string commandName, string[] args)
        => model.ExecuteCommand(commandName, args);

    public static void Log (object message) => model.Log(message);
}
