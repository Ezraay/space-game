using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Utility
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private List<CursorType> cursorTypes;
        private static Dictionary<string, Texture2D> cursors = new Dictionary<string, Texture2D>();


        private void Start()
        {
            foreach (CursorType cursorType in cursorTypes)
            {
                cursors.Add(cursorType.name, cursorType.sprite);
            }
            
            SetCursor("default");
        }

        public static void SetCursor(string name)
        {
            if (!cursors.ContainsKey(name))
                Debug.LogError("No such cursor: " + name);
            Cursor.SetCursor(cursors[name], Vector2.one * 12, CursorMode.Auto);
        }

        [System.Serializable]
        private struct CursorType
        {
            public Texture2D sprite;
            public string name;
        }
    }
}
