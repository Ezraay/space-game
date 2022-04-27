using UnityEngine;

namespace Spaceships.Entities
{
    [CreateAssetMenu(menuName = "Create Ship", fileName = "New Ship", order = 0)]
    public class ShipData : ScriptableObject
    {
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
        public Projectile Projectile => projectile;

        public int CreditCost => creditCost;
        public int Tier => tier;
        public Sprite Sprite => sprite;

        [SerializeField] private new string name = "New Ship";
        [SerializeField] private string id = "new_ship";
        [SerializeField] private float forwardSpeed = 3;
        [SerializeField] private float backwardSpeed = 1;
        [SerializeField] private float strafeSpeed = 1;
        [SerializeField] private float rotateSpeed = 90;

        [SerializeField] private float acceleration = 4;
        [SerializeField] private float rotateAcceleration = 180;
        [SerializeField] private float tiltAmount = 45;

        [SerializeField] private float shotCooldown = 0.4f;
        [SerializeField] private Projectile projectile;

        [SerializeField] private int creditCost = 1000;
        [SerializeField] private int tier = 1;
        [SerializeField] private Sprite sprite;
    }
}