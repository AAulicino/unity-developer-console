using System;
using System.Collections.Generic;
using UnityDevConsole.Models.Command;
using UnityEngine;

namespace UnityDevConsole.Models.Console.Hint
{
    public class ConsoleHintModel : IConsoleHintModel
    {
        const int NO_SELECTION = -1;

        public event Action<bool> OnEnableChange;
        public event Action<string> OnHintSelected;

        readonly ICommandsCollection commandCollection;
        readonly IConsoleInputHistoryModel history;
        bool _enabled;

        public IReadOnlyList<string> InputHistory => history.InputHistory;

        public string[] ActiveHints { get; private set; }

        public bool Enabled
        {
            get => _enabled;
            private set
            {
                _enabled = value;
                OnEnableChange?.Invoke(value);
            }
        }

        public int SelectedIndex { get; private set; }
        public bool HasSelection => SelectedIndex != NO_SELECTION;

        public ConsoleHintModel (ICommandsCollection commands, IConsoleInputHistoryModel history)
        {
            this.commandCollection = commands;
            this.history = history;
        }

        public void Enable ()
        {
            SelectedIndex = NO_SELECTION;
            Enabled = true;
        }

        public void Submit ()
        {
            if (!Enabled)
                return;
            if (SelectedIndex == NO_SELECTION)
                return;
            OnHintSelected?.Invoke(ActiveHints[SelectedIndex]);
        }

        public void OnInputChange (string input) => UpdateHints(input);

        public void MoveSelectionUp ()
        {
            if (!Enabled)
                return;
            SelectedIndex = Mathf.Max(0, --SelectedIndex);
        }

        public void MoveSelectionDown ()
        {
            if (!Enabled)
                return;
            SelectedIndex = Mathf.Min(ActiveHints.Length - 1, ++SelectedIndex);
        }

        public void SelectCustom (string text)
        {
            if (!Enabled)
                return;
            SelectedIndex = Array.IndexOf(ActiveHints, text);
        }

        public void Disable ()
        {
            Enabled = false;
        }

        void UpdateHints (string input)
        {
            List<string> results = new List<string>();

            const int MAX_RESULTS = 10;
            foreach (ICommand command in commandCollection.Commands.Values)
            {
                if (results.Count >= MAX_RESULTS)
                    break;

                if (command.Hidden)
                    continue;

                if (command.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                    results.Add(command.FullName);
            }

            results.Sort();
            ActiveHints = results.ToArray();
        }
    }
}
