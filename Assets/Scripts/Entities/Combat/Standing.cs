using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.Combat
{
    [CreateAssetMenu(menuName = "Create Alignment", fileName = "New Alignment", order = 0)]
    public class Standing : ScriptableObject
    {
        private const float EnemyThreshold = 20;

        [SerializeField] private new string name = "New Alignment";
        [SerializeField] private Color color = Color.white;
        [SerializeField] private List<StandingRelationship> relationships;

        public string Name => name;
        public Color Colour => color;
        public List<StandingRelationship> Relationships => relationships;


        public List<Standing> GetEnemies()
        {
            List<Standing> result = new List<Standing>();

            foreach (StandingRelationship standingRelationship in relationships)
            {
                if (standingRelationship.Relationship <= EnemyThreshold)
                    result.Add(standingRelationship.Standing);
            }

            return result;
        }

        [Serializable]
        public struct StandingRelationship
        {
            [SerializeField] private Standing standing;
            [SerializeField] [Range(0, 100)] private float relationship;

            public Standing Standing => standing;
            public float Relationship => relationship;
        }
    }
}