using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.Combat
{
    [CreateAssetMenu(menuName = "Create Alignment", fileName = "New Alignment", order = 0)]
    public class Standing : ScriptableObject
    {
        [SerializeField] private new string name = "New Alignment";
        [SerializeField] private Color color = Color.white;
        [SerializeField] private List<StandingRelationship> relationships;

        public string Name => name;
        public Color Colour => color;
        public List<StandingRelationship> Relationships => relationships;
        

        [System.Serializable]
        public struct StandingRelationship
        {
            [SerializeField] private Standing standing;
            [SerializeField] [Range(0, 100)] private float relationship;

            public Standing Standing => standing;
            public float Relationship => relationship;
        }
    }
}