using UnityDevConsole.Controllers.Console;
using UnityDevConsole.Controllers.Hint;
using UnityDevConsole.Controllers.Hint.Factory;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Command;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Views;
using UnityDeveloperConsole.Views.Hint;

public static class DeveloperConsole
{
    static IConsoleModel model;

    public static void Initialize ()
    {
        if (model != null)
            return;

        IConsoleInputHistoryModel historyModel = ConsoleInputHistoryModelFactory.Create();
        ICommandsCollection commandsCollection = CommandsCollectionFactory.Create();
        model = ConsoleModelFactory.Create(historyModel, commandsCollection);

        IConsoleHintModel hintModel = ConsoleHintModelFactory.Create(
            historyModel,
            commandsCollection
        );

        ConsoleUIView view = ConsoleUIViewFactory.Create();
        IConsoleInputDetectorModel inputDetector = ConsoleInputDetectorModelFactory.Create(
            view,
            model
        );

        ConsoleUIControllerFactory.Create(model, view, inputDetector, hintModel);
        ConsoleHintUIControllerFactory.Create(
            hintModel,
            view.HintUI,
            view,
            new ConsoleHintEntryUIViewFactory(),
            inputDetector
        );
        commandsCollection.Initialize();
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
