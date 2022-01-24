using UnityDevConsole.Settings;
using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public interface IConsoleHintEntryUIViewFactory
    {
        IHintEntryUIView Create (IConsoleSettings settings, Transform parent);
    }
}
