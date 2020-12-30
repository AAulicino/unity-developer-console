using UnityEngine;

public interface IConsoleSettings
{
    int HistorySize { get; }
    int MaxHints { get; }
    string[] Assemblies { get; }

    KeyCode ToggleConsole { get; }
    KeyCode CloseHint { get; }
    KeyCode Submit { get; }
    KeyCode Submit2 { get; }
    KeyCode HintUp { get; }
    KeyCode HintDown { get; }
}
