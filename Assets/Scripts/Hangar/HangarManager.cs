using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Hangar
{
    public class HangarManager : MonoBehaviour
    {
        public static string hangarName;

        private void Start()
        {
            hangarName = HangarData.stationName;
        }
    }
}