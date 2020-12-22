using UnityDevConsole.Views.Hint;
using UnityEngine;

namespace UnityDeveloperConsole.Views.Hint
{
    public class ConsoleHintEntryUIViewFactory : IConsoleHintEntryUIViewFactory
    {
        public IHintEntryUIView Create (Transform parent)
        {
            return Object.Instantiate(
                Resources.Load<HintEntryUIView>("DevConsole/DC_HintEntry"),
                parent
            );
        }
    }
}
