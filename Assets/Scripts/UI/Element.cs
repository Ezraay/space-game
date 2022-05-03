using UnityEngine;
using UnityEngine.EventSystems;

namespace Spaceships.UI
{
    public abstract class Element : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
        IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}