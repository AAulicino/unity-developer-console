using UnityDevConsole.Views.Hint;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevConsole.Views
{
    public class ConsoleUIView : MonoBehaviour, IConsoleUIView
    {
        [SerializeField] GameObject uiContainer;
        [SerializeField] InputField bodyText;
        [SerializeField] InputField inputField;
        [SerializeField] HintUIView hintUI;

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

        public InputField BodyText => bodyText;
        public InputField InputField => inputField;
        public HintUIView HintUI => hintUI;
    }
}
