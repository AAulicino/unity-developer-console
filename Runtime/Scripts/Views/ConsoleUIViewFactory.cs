using UnityDevConsole.Settings;
using UnityEngine;

namespace UnityDevConsole.Views
{
    public static class ConsoleUIViewFactory
    {
        public static ConsoleUIView Create (IConsoleSettings settings)
        {
            return Object.Instantiate(settings.ConsoleSkinPrefab);
        }
    }
}
