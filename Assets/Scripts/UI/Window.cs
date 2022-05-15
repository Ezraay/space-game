using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Spaceships.UI
{
    public class Window : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected WindowDragBar dragBar;
        [SerializeField] private Image dragVisual;
        [HideInInspector] public RectTransform rectTransform;
        [SerializeField] private Text titleText;
        private bool shown;
        public UnityEvent OnShow { get; } = new UnityEvent();
        public UnityEvent OnClose { get; } = new UnityEvent();


        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }

        protected virtual void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            dragBar.Setup(this);
        }
        
        protected void SetTitle(string title)
        {
            titleText.text = title;
        }

        public virtual void Show()
        {
            dragBar.gameObject.SetActive(true);
            shown = true;
            OnShow.Invoke();
        }

        public virtual void Close()
        {
            OnClose.Invoke();
            Destroy(gameObject);
        }

        public void Toggle()
        {
            if (shown)
                Close();
            else
                Show();
        }

        public void ShowDragVisual()
        {
            dragVisual.enabled = true;
        }

        public void HideDragVisual()
        {
            dragVisual.enabled = false;
        }

        public virtual void OnDragOnto(DraggableSlot slot)
        {
        }

        // public virtual bool CanDragOnto(DraggableSlot slot) => false;
    }
}