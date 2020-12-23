using System;
using System.Collections.Generic;

namespace UnityDevConsole.Models.Console.Hint
{
    public interface IConsoleHintModel
    {
        event Action<bool> OnEnableChange;
        event Action<string> OnHintSelected;

        IReadOnlyList<string> InputHistory { get; }
        IReadOnlyList<string> ActiveHints { get; }

        bool Enabled { get; }
        int SelectedIndex { get; }
        bool HasSelection { get; }

        void Submit ();
        void Enable ();
        void Disable ();
        void MoveSelectionDown ();
        void MoveSelectionUp ();
        void OnInputChange (string input);
        void SelectCustom (string text);
    }
}
