using System;
using UnityEngine;

namespace Spaceships.Hangar
{
    public class HangarManager : MonoBehaviour
    {
        public static string hangarName;
        
        private void Start()
        {
            hangarName = SceneTransitions.HangarData.stationName;
        }
    }
}