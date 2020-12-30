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
        readonly IConsoleSettings settings;
        bool _enabled;

        readonly List<string> activeHints;

        public IReadOnlyList<string> InputHistory => history.InputHistory;
        public IReadOnlyList<string> ActiveHints => activeHints;

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

        public ConsoleHintModel (
            ICommandsCollection commands,
            IConsoleInputHistoryModel history,
            IConsoleSettings settings
        )
        {
            this.commandCollection = commands;
            this.history = history;
            this.settings = settings;
            activeHints = new List<string>(settings.MaxHints);
        }

        public void Enable ()
        {
            if (Enabled)
                return;
            SelectedIndex = NO_SELECTION;
            Enabled = true;
        }

        public void Submit ()
        {
            if (!Enabled)
                return;
            if (SelectedIndex == NO_SELECTION)
                return;
            OnHintSelected?.Invoke(ActiveHints[SelectedIndex].Split(' ')[0]);
        }

        public void OnInputChange (string input) => UpdateHints(input);

        public void MoveSelectionUp ()
        {
            if (!Enabled)
                return;
            SelectedIndex = ++SelectedIndex % ActiveHints.Count;
        }

        public void MoveSelectionDown ()
        {
            if (!Enabled)
                return;
            if (--SelectedIndex < 0)
                SelectedIndex = ActiveHints.Count - 1;
        }

        public void SelectCustom (string text)
        {
            if (!Enabled)
                return;
            SelectedIndex = activeHints.IndexOf(text);
        }

        public void Disable ()
        {
            if (!Enabled)
                return;
            Enabled = false;
        }

        void UpdateHints (string input)
        {
            activeHints.Clear();

            if (string.IsNullOrEmpty(input))
                return;

            foreach (ICommand command in commandCollection.Commands.Values)
            {
                if (activeHints.Count >= settings.MaxHints)
                    break;

                if (command.Hidden)
                    continue;

                if (command.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                    activeHints.Add(command.FullName);
            }

            SelectedIndex = NO_SELECTION;
            activeHints.Sort();
        }
    }
}
