using UnityEngine;

namespace UnityDevConsole.Views
{
    public static class ConsoleUIViewFactory
    {
        public static ConsoleUIView Create ()
        {
            return Object.Instantiate(Resources.Load<ConsoleUIView>("DevConsole/DC_ConsoleUI"));
        }
    }
}
