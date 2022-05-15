using System;
using System.Collections;
using System.Collections.Generic;
using Spaceships.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships
{
    public class MenuBar : MonoBehaviour
    {
        [SerializeField] private List<MenuWindow> windows;

        private void Start()
        {
            foreach (MenuWindow menuWindow in windows)
            {
                menuWindow.button.onClick.AddListener(() =>
                {
                    Instantiate(menuWindow.window, transform.parent);
                });
            }
        }

        [System.Serializable]
        private struct MenuWindow
        {
            public Button button;
            public Window window;
        }
    }
}
