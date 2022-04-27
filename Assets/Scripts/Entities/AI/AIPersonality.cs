using UnityEngine;

namespace Spaceships.Entities.AI
{
    [CreateAssetMenu(menuName = "AI/Personality", fileName = "New Personality", order = 0)]
    public class AIPersonality : ScriptableObject
    {
        public WeightedBehaviour[] WeightedBehaviours => weightedBehaviours;
        
        [SerializeField] private WeightedBehaviour[] weightedBehaviours;

        [System.Serializable]
        public struct WeightedBehaviour
        {
            public AIBehaviour behaviour;
            public float weight;
        }
    }
}