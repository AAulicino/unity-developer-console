using System;
using System.Collections;
using UnityDevConsole.Models.Console;
using UnityEngine;

namespace UnityDevConsole.Controllers.Input
{
    public class ConsoleInputDetectorModel : IConsoleInputDetectorModel
    {
        public event Action OnToggleVisibility;
        public event Action OnSubmit;
        public event Action OnMoveUp;
        public event Action OnMoveDown;
        public event Action OnEscape;

        readonly ICoroutineRunner runner;
        readonly IInput input;
        readonly IConsoleStateProvider console;

        Coroutine listenRoutine;

        bool anyKeyLastFrame;

        public ConsoleInputDetectorModel (
            ICoroutineRunner runner,
            IInput input,
            IConsoleStateProvider console
        )
        {
            this.input = input;
            this.console = console;
            this.runner = runner;
        }

        public void Initialize ()
        {
            listenRoutine = runner.StartCoroutine(ListenToInput());
        }

        IEnumerator ListenToInput ()
        {
            while (true)
            {
                yield return null;

                if (input.GetKeyDown(KeyCode.BackQuote) && !anyKeyLastFrame)
                    OnToggleVisibility?.Invoke();

                anyKeyLastFrame = input.AnyKey;

                if (!console.Enabled)
                    continue;

                if (input.GetKeyDown(KeyCode.Return) || input.GetKeyDown(KeyCode.KeypadEnter))
                    OnSubmit();
                else if (input.GetKeyDown(KeyCode.UpArrow))
                    OnMoveUp?.Invoke();
                else if (input.GetKeyDown(KeyCode.DownArrow))
                    OnMoveDown?.Invoke();
                else if (input.GetKeyDown(KeyCode.Escape))
                    OnEscape?.Invoke();
            }
        }

        public void Dispose ()
        {
            if (listenRoutine != null)
                runner.StopCoroutine(listenRoutine);
        }
    }
}
