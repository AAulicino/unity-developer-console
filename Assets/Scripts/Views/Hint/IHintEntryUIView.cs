using System;
using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public interface IHintEntryUIView
    {
        event Action<IHintEntryUIView> OnClick;
        event Action<IHintEntryUIView> OnPointerEnter;
        event Action<IHintEntryUIView> OnPointerExit;

        Vector3 Position { get; }
        string Text { get; set; }

        void SetActive (bool active);
    }
}
