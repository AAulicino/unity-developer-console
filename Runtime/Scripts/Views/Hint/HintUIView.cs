using UnityEngine;

namespace UnityDevConsole.Views.Hint
{
    public class HintUIView : MonoBehaviour, IHintUIView
    {
        [SerializeField] GameObject selectionOutline;
        [SerializeField] Transform entriesContainer;

        public GameObject SelectionOutline => selectionOutline;
        public Transform EntriesContainer => entriesContainer;

        public void SetActive (bool active) => gameObject.SetActive(active);
    }
}
