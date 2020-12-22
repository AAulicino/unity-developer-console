using UnityDevConsole.Controllers.Console;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Views;

public static class DeveloperConsole
{
    static IConsoleModel model;

    public static void Initialize ()
    {
        if (model != null)
            return;

        model = ConsoleModelFactory.Create();

        ConsoleUIView view = ConsoleUIViewFactory.Create();
        IConsoleInputDetectorModel inputDetector = ConsoleInputDetectorModelFactory.Create(view);

        ConsoleUIControllerFactory.Create(model, view, inputDetector);
        model.Initialize();
        inputDetector.Initialize(model);
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
