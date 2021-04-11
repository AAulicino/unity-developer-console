using UnityDevConsole.Settings;
using UnityDevConsole.Views.Hint;
using UnityEngine;

namespace UnityDeveloperConsole.Views.Hint
{
    public class ConsoleHintEntryUIViewFactory : IConsoleHintEntryUIViewFactory
    {
        public IHintEntryUIView Create (IConsoleSettings settings, Transform parent)
        {
            return Object.Instantiate(settings.HintSkinPrefab, parent);
        }
    }
}
