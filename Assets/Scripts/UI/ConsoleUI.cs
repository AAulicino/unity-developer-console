using UnityEngine;
using UnityEngine.UI;

namespace UnityDeveloperConsole
{
    public class ConsoleUI : MonoBehaviour
    {
        public static ConsoleUI Instance { get; private set; }

        HintBoxUI hintBox;
        //Selectable text must be inputfield
        InputField body;
        InputField inputField;
        GameObject uiContainer;
        bool anyKeyLastFrame;
        readonly string previousInputText;

        public bool Enabled
        {
            get { return uiContainer.activeSelf; }
            set
            {
                uiContainer.SetActive(value);
                if (value)
                {
                    inputField.Select();
                    inputField.ActivateInputField();
                }
            }
        }

        public static void Initialize()
        {
            if (Instance != null)
                Instantiate(Resources.Load<GameObject>("UnityDeveloperConsoleUI"));
        }

        [ConsoleCommand("cls")]
        [ConsoleCommand("clear")]
        static void ClearBuffer()
        {
            Instance.Clear();
        }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            hintBox = transform.FindChildRecursive("HintBoxUI").GetComponent<HintBoxUI>();
            uiContainer = transform.FindChildRecursive("UI").gameObject;
            body = transform.FindChildRecursive("BodyText").GetComponent<InputField>();
            inputField = transform.FindChildRecursive("Input").GetComponent<InputField>();
            uiContainer.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote) && !anyKeyLastFrame)
                Enabled = !Enabled;

            if (!Enabled)
                return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (hintBox.Enabled)
                    inputField.text = hintBox.GetSelectedSuggestion();
                else
                    Submit();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (hintBox.Enabled)
                    hintBox.MoveSelectionUp();
                else
                    hintBox.DisplayHint(inputField.text);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (hintBox.Enabled)
                    hintBox.MoveSelectionDown();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
                hintBox.Enabled = false;

            anyKeyLastFrame = Input.anyKey;
        }

        public void Submit()
        {
            if (inputField.text == "")
                return;

            CommandSuggestionsHandler.RegisterInputToHistory(inputField.text);

            Log("> " + inputField.text);
            Log(CommandsHandler.ExecuteCommand(inputField.text));

            inputField.text = "";
            inputField.Select();
            inputField.ActivateInputField();
        }

        public void Log(object message)
        {
            if (message == null)
                return;

            string content = message.ToString();

            if (content == null)
                return;

            body.text += content + "\n";
        }

        public void Clear()
        {
            body.text = "";
        }
    }
}
