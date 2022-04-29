using UnityEngine;

namespace Spaceships.UI
{
    public class Window : Element
    {
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
        }

        public virtual void Hide()
        {
            content.SetActive(false);
            shown = false;
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