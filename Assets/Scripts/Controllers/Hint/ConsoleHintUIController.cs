using System.Collections.Generic;
using UnityDevConsole.Controllers.Input;
using UnityDevConsole.Models.Console.Hint;
using UnityDevConsole.Settings;
using UnityDevConsole.Views;
using UnityDevConsole.Views.Hint;

namespace UnityDevConsole.Controllers.Hint
{
    public class ConsoleHintUIController
    {
        readonly IConsoleHintModel model;
        readonly IHintUIView view;
        readonly IConsoleHintEntryUIViewFactory entryFactory;
        readonly IConsoleInputDetectorModel input;
        readonly IConsoleSettings settings;
        readonly List<IHintEntryUIView> hintEntries = new List<IHintEntryUIView>();

        public ConsoleHintUIController (
            IConsoleHintModel model,
            IHintUIView hintView,
            IConsoleUIView consoleView,
            IConsoleHintEntryUIViewFactory entryFactory,
            IConsoleInputDetectorModel input,
            IConsoleSettings settings
        )
        {
            this.model = model;
            this.view = hintView;
            this.entryFactory = entryFactory;
            this.input = input;
            this.settings = settings;
            model.OnEnableChange += HandleEnableChange;
            consoleView.InputField.onValueChanged.AddListener(HandleInputValueChanged);

            hintView.SetActive(false);
        }

        void HandleEnableChange (bool value)
        {
            if (value)
                AddInputListeners();
            else
                RemoveInputListeners();
            view.SetActive(value);
        }

        void HandleInputValueChanged (string input)
        {
            model.OnInputChange(input);
            if (model.ActiveHints.Count == 0)
            {
                if (model.Enabled)
                    model.Disable();
                return;
            }
            model.Enable();
            SyncHints();
            SyncHighlight();
        }

        void HandleSubmit () => model.Submit();
        void HandleEscape () => model.Disable();

        void HandleMoveUp ()
        {
            model.MoveSelectionUp();
            SyncHighlight();
        }

        void HandleMoveDown ()
        {
            model.MoveSelectionDown();
            SyncHighlight();
        }

        void SyncHints ()
        {
            int hintCount = model.ActiveHints.Count;

            while (hintCount > hintEntries.Count)
                hintEntries.Add(entryFactory.Create(settings, view.EntriesContainer));

            for (int i = 0; i < hintEntries.Count; i++)
            {
                if (i < hintCount)
                {
                    hintEntries[i].Text = model.ActiveHints[i];
                    hintEntries[i].SetActive(true);
                }
                else
                    hintEntries[i].SetActive(false);
            }
        }

        void SyncHighlight ()
        {
            if (!model.HasSelection)
            {
                view.SelectionOutline.SetActive(false);
                return;
            }

            view.SelectionOutline.transform.position = hintEntries[model.SelectedIndex].Position;
            view.SelectionOutline.SetActive(true);
        }

        void AddInputListeners ()
        {
            input.OnSubmit += HandleSubmit;
            input.OnEscape += HandleEscape;
            input.OnMoveUp += HandleMoveUp;
            input.OnMoveDown += HandleMoveDown;
        }

        void RemoveInputListeners ()
        {
            input.OnSubmit -= HandleSubmit;
            input.OnEscape -= HandleEscape;
            input.OnMoveUp -= HandleMoveUp;
            input.OnMoveDown -= HandleMoveDown;
        }
    }
}
