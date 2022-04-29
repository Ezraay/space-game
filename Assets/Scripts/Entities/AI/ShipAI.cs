using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.AI
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour
    {
        public AIPersonality personality;
        public Dictionary<string, object> data; // Data used by behaviours for persistance
        private Ship ship;

        private void Start()
        {
            if (personality != null)
                Setup();
        }

        private void Update()
        {
            float highestWeight = 0;
            AIBehaviour highestBehaviour = null;
            foreach (AIPersonality.WeightedBehaviour behaviour in personality.WeightedBehaviours)
            {
                float weight = behaviour.weight * behaviour.behaviour.GetWeight(ship, this);
                if (weight > highestWeight)
                {
                    highestBehaviour = behaviour.behaviour;
                    highestWeight = weight;
                }
            }

            if (highestBehaviour != null)
                highestBehaviour.Tick(ship, this);
        }

        public void Setup()
        {
            ship = GetComponent<Ship>();
            data = new Dictionary<string, object>();
            foreach (AIPersonality.WeightedBehaviour weightedBehaviour in personality.WeightedBehaviours)
            {
                weightedBehaviour.behaviour.Setup(ship, this);
            }
        }

        public void Setup(AIPersonality personality)
        {
            ship = GetComponent<Ship>();
            this.personality = personality;
        }
    }
}