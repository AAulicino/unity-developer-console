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
        readonly IConsoleSettings settings;
        readonly IInput input;
        readonly IConsoleStateProvider console;

        Coroutine listenRoutine;

        bool anyKeyLastFrame;

        public ConsoleInputDetectorModel (
            ICoroutineRunner runner,
            IInput input,
            IConsoleStateProvider console,
            IConsoleSettings settings
        )
        {
            this.input = input;
            this.console = console;
            this.runner = runner;
            this.settings = settings;
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

                if (input.GetKeyDown(settings.ToggleConsole) && !anyKeyLastFrame)
                    OnToggleVisibility?.Invoke();

                anyKeyLastFrame = input.AnyKey;

                if (!console.Enabled)
                    continue;

                if (input.GetKeyDown(settings.Submit) || input.GetKeyDown(settings.Submit2))
                    OnSubmit();
                else if (input.GetKeyDown(settings.HintUp))
                    OnMoveUp?.Invoke();
                else if (input.GetKeyDown(settings.HintDown))
                    OnMoveDown?.Invoke();
                else if (input.GetKeyDown(settings.CloseHint))
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
