using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.AI
{
    [RequireComponent(typeof(Ship))]
    public class ShipAI : MonoBehaviour
    {
        private AIPersonality personality;
        private Ship ship;

        private void Update()
        {
            foreach (AIDataBehaviour behaviour in personality.DataBehaviours)
            {
                behaviour.Tick();
            }


            List<AIBehaviour> highestBehaviours = GetHighestBehaviours();
            foreach (AIBehaviour behaviour in highestBehaviours)
            {
                behaviour.Tick();
            }
        }

        private List<AIBehaviour> GetHighestBehaviours()
        {
            float highestWeight = 0;
            List<AIBehaviour> highestBehaviours = new List<AIBehaviour>();
            foreach (AIBehaviour behaviour in personality.Behaviours)
            {
                float weight = behaviour.Weight;
                if (weight == 0)
                    continue;

                if (weight > highestWeight)
                {
                    highestBehaviours.Clear();
                    highestWeight = weight;
                }

                if (highestWeight == weight)
                    highestBehaviours.Add(behaviour);
            }

            return highestBehaviours;
        }

        public void Setup(AIPersonality personalityPrefab)
        {
            personality = Instantiate(personalityPrefab, transform);
            ship = GetComponent<Ship>();

            foreach (AIDataBehaviour dataBehaviour in personality.DataBehaviours)
            {
                dataBehaviour.Setup(ship, this);
            }

            foreach (AIBehaviour behaviour in personality.Behaviours)
            {
                behaviour.Setup(ship, this);
            }
        }
    }
}