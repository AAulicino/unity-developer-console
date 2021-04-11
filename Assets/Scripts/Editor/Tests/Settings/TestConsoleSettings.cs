using UnityDevConsole.Settings;
using UnityDevConsole.Views;
using UnityDevConsole.Views.Hint;
using UnityEngine;

namespace UnityDevConsole.Tests.Settings
{
    public class TestConsoleSettings : IConsoleSettings
    {
        public int HistorySize { get; set; }

        public int MaxHints { get; set; }

        public string[] Assemblies { get; set; }

        public KeyCode ToggleConsole { get; set; }

        public KeyCode CloseHint { get; set; }

        public KeyCode Submit { get; set; }

        public KeyCode Submit2 { get; set; }

        public KeyCode HintUp { get; set; }

        public KeyCode HintDown { get; set; }

        public bool AutoInitialize { get; set; }

        public ConsoleUIView ConsoleSkinPrefab => throw new System.NotImplementedException();

        public HintEntryUIView HintSkinPrefab => throw new System.NotImplementedException();
    }
}
