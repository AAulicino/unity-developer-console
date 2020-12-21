using System;

namespace UnityDevConsole.Views
{
    using UnityEngine;

    public class ConsoleUIViewFactory
    {
        public Lazy<ConsoleUIView> CreateViewLazy ()
        {
            return new Lazy<ConsoleUIView>(
                Object.Instantiate(Resources.Load<ConsoleUIView>("UnityDevConsoleUI"))
            );
        }
    }
}
