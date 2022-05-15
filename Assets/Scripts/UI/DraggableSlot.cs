using Spaceships.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Spaceships.UI
{
    public class DraggableSlot : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform draggableContent;
        private Window windowHovering;

        public void OnDrag(PointerEventData eventData)
        {
            Window window = UIRaycast.GetWindowAt(InputController.mousePosition);
            if (windowHovering != window)
            {
                windowHovering?.HideDragVisual();
                windowHovering = window;
                if (window != null && CanDragInto(window))
                {
                    window.ShowDragVisual();
                }
            }

            draggableContent.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (windowHovering != null && CanDragInto(windowHovering))
            {
                windowHovering.HideDragVisual();
                windowHovering.OnDragOnto(this);
            }

            draggableContent.anchoredPosition = Vector3.zero;
        }

        protected virtual bool CanDragInto(Window window)
        {
            return false;
        }
    }
}