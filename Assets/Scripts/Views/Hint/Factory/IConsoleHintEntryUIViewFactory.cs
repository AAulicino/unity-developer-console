using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public interface IConsoleHintEntryUIViewFactory
    {
        IHintEntryUIView Create (Transform parent);
    }
}
