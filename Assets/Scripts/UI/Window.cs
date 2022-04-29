using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.UI
{
    public class Window : Element
    {
        [HideInInspector] public UnityEvent onShow = new UnityEvent();
        [HideInInspector] public UnityEvent onHide = new UnityEvent();
        
        [SerializeField] private GameObject content;
        private bool shown;

        protected virtual void Start()
        {
            Hide();
        }

        public virtual void Show()
        {
            content.SetActive(true);
            shown = true;
            onShow.Invoke();
        }

        public virtual void Hide()
        {
            content.SetActive(false);
            shown = false;
            onHide.Invoke();
        }

        public void Toggle()
        {
            if (shown)
                Hide();
            else
                Show();
        }
    }
}