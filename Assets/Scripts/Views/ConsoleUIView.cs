using UnityEngine;
using UnityEngine.UI;

namespace UnityDevConsole.Views
{
    public class ConsoleUIView : MonoBehaviour, IConsoleUIView
    {
        // [SerializeField] HintBoxUI hintBox;
        [SerializeField] InputField body;
        [SerializeField] InputField inputField;
        [SerializeField] GameObject uiContainer;

        public bool Enabled
        {
            get { return UIContainer.activeSelf; }
            set
            {
                UIContainer.SetActive(value);
                if (value)
                {
                    inputField.Select();
                    inputField.ActivateInputField();
                }
            }
        }

        // public HintBoxUI HintBox => hintBox;
        public InputField Body => body;
        public InputField InputField => inputField;
        public GameObject UIContainer => uiContainer;

        // void Update ()
        // {
        //     if (Input.GetKeyDown(KeyCode.BackQuote) && !anyKeyLastFrame)
        //         Enabled = !Enabled;

        //     if (!Enabled)
        //         return;

        //     if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //     {
        //         if (hintBox.Enabled)
        //             inputField.text = hintBox.GetSelectedSuggestion();
        //         else
        //             Submit();
        //     }
        //     else if (Input.GetKeyDown(KeyCode.UpArrow))
        //     {
        //         if (hintBox.Enabled)
        //             hintBox.MoveSelectionUp();
        //         else
        //             hintBox.DisplayHint(inputField.text);
        //     }
        //     else if (Input.GetKeyDown(KeyCode.DownArrow))
        //     {
        //         if (hintBox.Enabled)
        //             hintBox.MoveSelectionDown();
        //     }
        //     else if (Input.GetKeyDown(KeyCode.Escape))
        //         hintBox.Enabled = false;

        //     anyKeyLastFrame = Input.anyKey;
        // }
    }
}
/*
         CommandSuggestionsHandler.RegisterInputToHistory(inputField.text);

        Log("> " + inputField.text);
Log(CommandsHandler.ExecuteCommand(inputField.text));

inputField.text = "";
inputField.Select();
inputField.ActivateInputField();
*/
