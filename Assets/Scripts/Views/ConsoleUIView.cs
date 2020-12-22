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

        // public HintBoxUI HintBox => hintBox;
        public InputField Body => body;
        public InputField InputField => inputField;
    }
}
