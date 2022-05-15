using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Spaceships.UI
{
    public class ResizableWindow : Window
    {
        [SerializeField] private float borderSize = 0.01f;
        [SerializeField] private Vector2 minimumSize = new Vector2();
        
        private Vector2 direction;

        protected override void Start()
        {
            base.Start();

            Show();
        }

        private void Update()
        {
            // Debug.Log(InputController.mousePosition);
            // Vector2 direction = GetBorderDirection(InputController.mousePosition);
            // if (direction.x == direction.y && direction.x != 0)
            //     CursorManager.SetCursor("topright_resize");
            //     // rect.xMin += direction.x;
            // else if (direction.x + direction.y == 0 && direction.x != 0)
            //         CursorManager.SetCursor("topleft_resize");
            //     // rect.yMin += direction.y;
            // else if (direction.x != 0 )
            //     // rect.width += direction.x;
            //     CursorManager.SetCursor("horizontal_resize");
            // else if (direction.y != 0)
            //     CursorManager.SetCursor("vertical_resize");
            // else
            //     CursorManager.SetCursor("default");
                // rect.height += direction.y;
            // if (InputController.leftMouseDown)
            // {
            //     Rect rect = rectTransform.rect;
            //     rectTransform.sizeDelta = rect.size;
            //     rectTransform.position = rect.position;
            // }
            // Debug.Log(direction);
        }

        // public override void OnDrag(PointerEventData eventData)
        // {
            // eventData.delta
        // }

        private Vector2 GetBorderDirection(Vector2 mousePosition)
        {
            Rect rect = rectTransform.rect;
            rect.position += (Vector2)rectTransform.position;

            bool left = rect.xMin <= mousePosition.x && mousePosition.x <= rect.xMin + borderSize && rect.yMin <= mousePosition.y && mousePosition.y <= rect.yMax;
            bool bottom = rect.yMin <= mousePosition.y && mousePosition.y <= rect.yMin + borderSize && rect.xMin <= mousePosition.x && mousePosition.x <= rect.xMax;
            bool right = rect.xMax - borderSize <= mousePosition.x && mousePosition.x <= rect.xMax && rect.yMin <= mousePosition.y && mousePosition.y <= rect.yMax;
            bool top = rect.yMax - borderSize <= mousePosition.y && mousePosition.y <= rect.yMax&& rect.xMin <= mousePosition.x && mousePosition.x <= rect.xMax;

            Vector2 result = new Vector2();
            if (left)
                result += Vector2.left;
            if (right)
                result += Vector2.right;
            if (top)
                result += Vector2.up;
            if (bottom)
                result += Vector2.down;
            
            return result;
        }
    }
}