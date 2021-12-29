using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public interface IHintUIView
    {
        Transform EntriesContainer { get; }
        GameObject SelectionOutline { get; }

        void SetActive (bool active);
    }
}
