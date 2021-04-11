using UnityDevConsole.Views;
using UnityDevConsole.Views.Hint;
using UnityEngine;

namespace UnityDevConsole.Settings
{
    public class ConsoleSettings : ScriptableObject, IConsoleSettings
    {
        public const string SETTINGS_PATH = "UnityDeveloperConsole/Resources";
        public const string SETTINGS_NAME = "ConsoleSettings";

        const string DEFAULT_SKIN_PATH = "DevConsole/DC_ConsoleUI";
        const string DEFAULT_HINT_PATH = "DevConsole/DC_HintEntry";

        static ConsoleSettings _instance;
        public static ConsoleSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = LoadInstance();
                return _instance;
            }
        }

        [SerializeField] bool autoInitialize = true;
        [SerializeField] int historySize = 10;
        [SerializeField] int maxHints = 10;

        [Header("Input")]
        [SerializeField] KeyCode toggleConsole = KeyCode.BackQuote;
        [SerializeField] KeyCode closeHint = KeyCode.Escape;
        [SerializeField] KeyCode submit = KeyCode.Return;
        [SerializeField] KeyCode submit2 = KeyCode.KeypadEnter;
        [SerializeField] KeyCode hintUp = KeyCode.UpArrow;
        [SerializeField] KeyCode hintDown = KeyCode.DownArrow;

        [Header("Skin")]
        [SerializeField] ConsoleUIView consoleSkinPrefab;
        [SerializeField] HintEntryUIView hintSkinPrefab;

        [SerializeField] string[] assemblies = new[] { "Assembly-CSharp" };

        public bool AutoInitialize => autoInitialize;
        public int HistorySize => historySize;
        public int MaxHints => maxHints;
        public string[] Assemblies => assemblies;

        public KeyCode ToggleConsole => toggleConsole;
        public KeyCode CloseHint => closeHint;
        public KeyCode Submit => submit;
        public KeyCode Submit2 => submit2;
        public KeyCode HintUp => hintUp;
        public KeyCode HintDown => hintDown;

        public ConsoleUIView ConsoleSkinPrefab => ConsoleSkinPrefab;
        public HintEntryUIView HintSkinPrefab => HintSkinPrefab;

        public static ConsoleSettings LoadInstance ()
            => Resources.Load<ConsoleSettings>(SETTINGS_NAME);

        public void Reset ()
        {
            consoleSkinPrefab = Resources.Load<ConsoleUIView>(DEFAULT_SKIN_PATH);
            hintSkinPrefab = Resources.Load<HintEntryUIView>(DEFAULT_HINT_PATH);
        }
    }
}
