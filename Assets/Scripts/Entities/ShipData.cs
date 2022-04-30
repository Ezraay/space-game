using NaughtyAttributes;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Entities
{
    [CreateAssetMenu(menuName = "Create Ship", fileName = "New Ship", order = 0)]
    public class ShipData : ScriptableObject
    {
        [BoxGroup("General")] [SerializeField] private new string name = "New Ship";
        [BoxGroup("General")] [SerializeField] private string id = "new_ship";

        [BoxGroup("Movement")] [SerializeField] private float forwardSpeed = 3;
        [BoxGroup("Movement")] [SerializeField] private float backwardSpeed = 1;
        [BoxGroup("Movement")] [SerializeField] private float strafeSpeed = 1;
        [BoxGroup("Movement")] [SerializeField] private float rotateSpeed = 90;
        [BoxGroup("Movement")] [SerializeField] private float acceleration = 5;
        [BoxGroup("Movement")] [SerializeField] private float rotateAcceleration = 180;
        [BoxGroup("Movement")] [SerializeField] private float tiltAmount = 45;


        [BoxGroup("Combat")] [SerializeField] [Min(0)] private float shotCooldown = 0.4f; // For each shot
        [BoxGroup("Combat")] [SerializeField] private Projectile projectile;
        [BoxGroup("Combat")] [SerializeField] [Min(1)] private int shotsPerClick = 1;

        [BoxGroup("Item")] [SerializeField] [Min(0)] private int creditCost = 1000;
        [BoxGroup("Item")] [SerializeField] [Range(1, 10)] private int tier = 1;
        [BoxGroup("Item")] [SerializeField] private Sprite sprite;

        public string Name => name;
        public string ID => id;
        public float ForwardSpeed => forwardSpeed;
        public float BackwardSpeed => backwardSpeed;
        public float StrafeSpeed => strafeSpeed;
        public float RotateSpeed => rotateSpeed;
        public float Acceleration => acceleration;

        public float RotateAcceleration => rotateAcceleration;

        public float TiltAmount => tiltAmount;

        public float ShotCooldown => shotCooldown;
        public int ShotsPerClick => shotsPerClick;
        public Projectile Projectile => projectile;

        public int CreditCost => creditCost;
        public int Tier => tier;
        public Sprite Sprite => sprite;
    }
}