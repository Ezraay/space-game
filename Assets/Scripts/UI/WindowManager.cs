using UnityEngine;

namespace Spaceships.UI
{
    public class WindowManager : MonoBehaviour
    {
        private Window shownWindow;

        private void Start()
        {
            Window[] allWindows = FindObjectsOfType<Window>();
            foreach (Window window in allWindows)
            {
                window.onShow.AddListener(() =>
                {
                    if (shownWindow != null)
                    {
                        shownWindow.Hide();
                    }

                    shownWindow = window;
                });

                window.onHide.AddListener(() => { shownWindow = null; });
            }
        }
    }
}