using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Views;

namespace UnityDevConsole.Controllers.Console
{
    public class ConsoleUIController
    {
        readonly IConsoleModel model;
        readonly IConsoleUIView view;
        readonly IConsoleHintModel hintModel;

        public ConsoleUIController (
            IConsoleModel model,
            IConsoleUIView view,
            IConsoleInputDetectorModel input,
            IConsoleHintModel hintModel
        )
        {
            this.model = model;
            this.view = view;
            this.hintModel = hintModel;

            model.OnEnableChange += HandleModelEnableChange;
            model.OnOutputUpdate += HandleOutputUpdate;

            hintModel.OnHintSelected += HandleHintSelected;

            input.OnToggleVisibility += HandleOnToggleVisibility;
            input.OnSubmit += HandleOnSubmit;

            view.Enabled = false;
        }

        void HandleOnToggleVisibility () => model.Enabled = !model.Enabled;

        void HandleModelEnableChange (bool enabled) => view.Enabled = enabled;

        void HandleOutputUpdate (string content) => view.BodyText.text = content;

        void HandleOnSubmit ()
        {
            if (hintModel.Enabled && hintModel.HasSelection)
                return;
            model.Submit(view.InputField.text);
            view.InputField.text = "";
            SelectInputField();
        }

        void HandleHintSelected (string text)
        {
            view.InputField.text = text;
            SelectInputField();
        }

        void SelectInputField ()
        {
            view.InputField.Select();
            view.InputField.ActivateInputField();
        }
    }
}
