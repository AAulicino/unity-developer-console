using System.Collections.Generic;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Views;
using UnityDevConsole.Views.Hint;

namespace UnityDevConsole.Controllers.Hint
{
    public class ConsoleHintUIController
    {
        readonly IConsoleHintModel model;
        readonly IHintUIView view;
        readonly IConsoleHintEntryUIViewFactory entryFactory;
        readonly List<IHintEntryUIView> hintEntries = new List<IHintEntryUIView>();

        int hintCount;

        public ConsoleHintUIController (
            IConsoleHintModel model,
            IHintUIView hintView,
            IConsoleUIView consoleView,
            IConsoleHintEntryUIViewFactory entryFactory,
            IConsoleInputDetectorModel input
        )
        {
            this.model = model;
            this.view = hintView;
            this.entryFactory = entryFactory;

            model.OnEnableChange += HandleEnableChange;
            consoleView.InputField.onValueChanged.AddListener(HandleInputValueChanged);

            input.OnSubmit += HandleSubmit;
            input.OnEscape += HandleEscape;
            input.OnMoveUp += HandleMoveUp;
            input.OnMoveDown += HandleMoveDown;
        }

        void HandleEnableChange (bool value) => view.SetActive(value);

        void HandleInputValueChanged (string input)
        {
            model.OnInputChange(input);
            if (model.Enabled && model.ActiveHints.Length == 0)
            {
                model.Disable();
                return;
            }
            model.Enable();
            Display(model.ActiveHints);
            UpdateHighlight();
        }

        void HandleSubmit () => model.Submit();
        void HandleEscape () => model.Disable();

        void HandleMoveUp ()
        {
            model.MoveSelectionUp();
            UpdateHighlight();
        }

        void HandleMoveDown ()
        {
            model.MoveSelectionDown();
            UpdateHighlight();
        }

        void Display (string[] hints)
        {
            hintCount = hints.Length;

            while (hintCount > hintEntries.Count)
                hintEntries.Add(entryFactory.Create(view.EntriesContainer));

            for (int i = 0; i < hintEntries.Count; i++)
            {
                if (i < hintCount)
                {
                    hintEntries[i].Text = hints[i];
                    hintEntries[i].SetActive(true);
                }
                else
                    hintEntries[i].SetActive(false);
            }
        }

        void UpdateHighlight ()
        {
            if (!model.HasSelection)
            {
                view.SelectionOutline.SetActive(false);
                return;
            }

            view.SelectionOutline.transform.position = hintEntries[model.SelectedIndex].Position;
            view.SelectionOutline.SetActive(true);
        }
    }
}
