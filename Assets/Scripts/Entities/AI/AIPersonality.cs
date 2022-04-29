using System;
using UnityEngine;

namespace Spaceships.Entities.AI
{
    [CreateAssetMenu(menuName = "AI/Personality", fileName = "New Personality", order = 0)]
    public class AIPersonality : ScriptableObject
    {
        [SerializeField] private WeightedBehaviour[] weightedBehaviours;
        public WeightedBehaviour[] WeightedBehaviours => weightedBehaviours;

        [Serializable]
        public struct WeightedBehaviour
        {
            public AIBehaviour behaviour;
            public float weight;
        }
    }
}