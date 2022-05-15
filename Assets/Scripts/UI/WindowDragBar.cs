using UnityEngine;
using UnityEngine.EventSystems;

namespace Spaceships.UI
{
    public class WindowDragBar : MonoBehaviour, IDragHandler
    {
        private Window window;
        
        public void Setup(Window window)
        {
            this.window = window;
        }
        
        
        
        public void OnDrag(PointerEventData eventData)
        {
            window.rectTransform.anchoredPosition += eventData.delta;
        }
    }
}