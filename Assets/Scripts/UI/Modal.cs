using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI
{
    public class Modal : MonoBehaviour
    {
        private List<Button> buttons;

        public void Setup(string title, string descrption, List<string> buttons)
        {
            
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}