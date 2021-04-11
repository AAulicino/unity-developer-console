using UnityDevConsole.Settings;
using UnityDevConsole.Views.Hint;
using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public class ConsoleHintEntryUIViewFactory : IConsoleHintEntryUIViewFactory
    {
        public IHintEntryUIView Create (IConsoleSettings settings, Transform parent)
        {
            return Object.Instantiate(settings.HintSkinPrefab, parent);
        }
    }
}
