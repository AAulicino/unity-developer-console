using UnityDevConsole.Views.Hint;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevConsole.Views
{
    public interface IConsoleUIView : ICoroutineRunner
    {
        bool Enabled { get; set; }
        InputField BodyText { get; }
        InputField InputField { get; }
        HintUIView HintUI { get; }
    }
}
