using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities.Combat
{
    public class StandingManager : MonoBehaviour
    {
        [SerializeField] private List<Standing> standings;
        private static readonly Dictionary<string, Standing> AllStandings = new Dictionary<string, Standing>();

        private void Start()
        {
            foreach (Standing alignment in standings)
            {
                AllStandings.Add(alignment.Name, alignment);
            }
        }

        public static Standing GetStanding(string standing)
        {
            if (!AllStandings.ContainsKey(standing))
                Debug.LogError("No such standing: " + standing);
            return AllStandings[standing];
        }

        public float GetRelationship(Standing first, Standing second)
        {
            foreach (Standing.StandingRelationship alignmentRelationship in first.Relationships)
            {
                if (second == alignmentRelationship.Standing)
                {
                    return alignmentRelationship.Relationship;
                }
            }
            
            Debug.LogError($"No relationship for {first.Name} - {second.Name}");
            return 0;
        }

        public float GetRelationship(string first, string second)
        {
            if (!AllStandings.ContainsKey(first))
                Debug.LogError("No such alignment: " + first);
            if (!AllStandings.ContainsKey(second))
                Debug.LogError("No such alignment: " + second);
            return GetRelationship(AllStandings[first], AllStandings[second]);
        }
    }
}