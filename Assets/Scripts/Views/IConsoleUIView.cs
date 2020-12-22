using UnityEngine;
using UnityEngine.UI;

namespace UnityDevConsole.Views
{
    public interface IConsoleUIView : ICoroutineRunner
    {
        bool Enabled { get; set; }
        // HintBoxUI HintBox { get; }
        InputField Body { get; }
        InputField InputField { get; }
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
