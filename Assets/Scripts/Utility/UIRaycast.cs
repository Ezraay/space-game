using System.Collections.Generic;
using Spaceships.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Spaceships.Utility
{
    public class UIRaycast
    {
        public static List<RaycastResult> Raycast(Vector2 position){
            PointerEventData pointerData = new PointerEventData (EventSystem.current)
            {
                pointerId = -1,
            };
         
            pointerData.position = position;
 
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results;
        }

        public static Window GetWindowAt(Vector2 position)
        {
            List<RaycastResult> elements = Raycast(position);
            foreach (RaycastResult element in elements)
            {
                Window window = element.gameObject.GetComponent<Window>();
                if (window != null)
                    return window;
            }

            return null;
        }
    }
}