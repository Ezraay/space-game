using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.AI
{
    public class AIPersonality : MonoBehaviour
    {
        public AIBehaviour[] Behaviours { get; private set; }
        public AIDataBehaviour[] DataBehaviours { get; private set; }

        private void Awake()
        {
            Behaviours = GetComponents<AIBehaviour>();
            DataBehaviours = GetComponents<AIDataBehaviour>();
        }
    }
}