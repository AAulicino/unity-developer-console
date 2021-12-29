using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityDevConsole.Views.Hint
{
    public class HintEntryUIView : MonoBehaviour, IHintEntryUIView, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<IHintEntryUIView> OnClick;
        public event Action<IHintEntryUIView> OnPointerEnter;
        public event Action<IHintEntryUIView> OnPointerExit;

        [SerializeField] Text text;

        public string Text
        {
            get => text.text;
            set => text.text = value;
        }

        public Vector3 Position => transform.position;

        void IPointerClickHandler.OnPointerClick (PointerEventData eventData)
            => OnClick?.Invoke(this);

        void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
            => OnPointerExit?.Invoke(this);

        void IPointerExitHandler.OnPointerExit (PointerEventData eventData)
            => OnPointerEnter?.Invoke(this);

        public void SetActive (bool active) => gameObject.SetActive(active);
    }
}
