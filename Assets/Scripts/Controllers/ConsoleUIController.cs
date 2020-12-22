using System;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Views;

namespace UnityDevConsole.Controllers.Console
{
    public class ConsoleUIController
    {
        readonly IConsoleModel model;
        readonly IConsoleUIView view;

        public ConsoleUIController (
            IConsoleModel model,
            IConsoleUIView view,
            IConsoleInputDetectorModel input
        )
        {
            this.model = model;
            this.view = view;

            model.OnEnableChange += HandleModelEnableChange;
            model.OnOutputUpdate += HandleOutputUpdate;

            input.OnToggleVisibility += HandleOnToggleVisibility;
            input.OnSubmit += HandleOnSubmit;
            input.OnMoveUp += HandleOnMoveUp;
            input.OnMoveDown += HandleOnMoveDown;
            input.OnEscape += HandleOnEscape;

            view.Enabled = false;
            input.Initialize(model);
        }

        void HandleOnToggleVisibility () => model.Enabled = !model.Enabled;

        void HandleModelEnableChange (bool enabled) => view.Enabled = enabled;

        void HandleOutputUpdate (string content) => view.Body.text = content;

        void HandleOnSubmit ()
        {
            //         if (hintBox.Enabled)
            //             inputField.text = hintBox.GetSelectedSuggestion();
            //         else
            model.Submit(view.InputField.text);
            view.InputField.text = "";
            view.InputField.Select();
            view.InputField.ActivateInputField();
        }

        void HandleOnMoveUp ()
        {
            //         if (hintBox.Enabled)
            //             hintBox.MoveSelectionUp();
            //         else
            //             hintBox.DisplayHint(inputField.text);
        }

        void HandleOnMoveDown ()
        {
            //         if (hintBox.Enabled)
            //             hintBox.MoveSelectionDown();
        }

        void HandleOnEscape ()
        {
            // hintBox.Enabled = false;
        }
    }
}
