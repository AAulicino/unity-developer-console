namespace UnityDevConsole.Views
{
    using UnityEngine;

    public static class ConsoleUIViewFactory
    {
        public static ConsoleUIView Create ()
        {
            return Object.Instantiate(Resources.Load<ConsoleUIView>("DevConsole/ConsoleUI"));
        }
    }
}
