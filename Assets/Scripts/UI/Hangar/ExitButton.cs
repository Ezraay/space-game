using Spaceships.Hangar;
using Spaceships.SceneTransitions;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Hangar
{
    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        private Button button;
        
        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                if (SpaceData.playerShipID != null)
                {
                    HangarManager.ShipStorage.RemoveActiveShip();
                    Loader.LoadSpace();
                }
                else
                {
                    // TODO: Show a modal to the player saying that you need a ship equipped
                }
            });
        }
    }
}
