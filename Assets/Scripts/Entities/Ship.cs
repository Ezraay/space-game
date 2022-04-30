using System.Collections.Generic;
using Spaceships.Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : Damageable
    {
        [HideInInspector] public UnityEvent onWarp = new UnityEvent();
        [HideInInspector] public UnityEvent onShoot = new UnityEvent();

        [SerializeField] private ShipData shipData;

        [SerializeField] private Transform gunParent;
        [HideInInspector] public float rotationInput;
        [HideInInspector] public float thrustInput;
        [HideInInspector] public float strafeInput;

        [HideInInspector] public float forwardVelocity;
        [HideInInspector] public float rotationVelocity;
        private readonly List<Transform> gunLocations = new List<Transform>();
        private Transform model;

        private new Rigidbody2D rigidbody;
        private float shotCooldown;
        private int shotNumber;

        private ShipTrail[] trails;
        public ShipData ShipData => shipData;
        public override float MaxHealth => shipData.MaxHealth;
        public override string Name => shipData.Name;

        protected override void Start()
        {
            base.Start();

            rigidbody = GetComponent<Rigidbody2D>();
            trails = GetComponentsInChildren<ShipTrail>();
            model = transform.GetChild(0);

            foreach (Transform gunChild in gunParent)
            {
                gunLocations.Add(gunChild);
            }
        }

        protected virtual void Update()
        {
            foreach (ShipTrail trail in trails)
            {
                trail.SetLength(forwardVelocity / shipData.ForwardSpeed);
            }

            shotCooldown = Mathf.Max(0, shotCooldown - Time.deltaTime);
        }

        protected virtual void FixedUpdate()
        {
            UpdateVelocity();
            UpdatePosition();
        }

        public void Shoot()
        {
            if (shotCooldown > 0)
                return;
            shotCooldown = shipData.ShotCooldown * shipData.ShotsPerClick;
            for (int i = 0; i < shipData.ShotsPerClick; i++)
            {
                Vector2 difference = InputController.mouseWorldPosition - (Vector2) gunLocations[shotNumber].position;
                float angle = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
                // Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Projectile newProjectile =
                    Instantiate(shipData.Projectile, gunLocations[shotNumber].position, Quaternion.identity);
                newProjectile.Setup(shipData.DamagePerShot, this, angle, shipData.Pierce);

                shotNumber = ++shotNumber % gunLocations.Count;
            }
        }

        public void Warp()
        {
            Destroy(gameObject, 3);

            if (onWarp != null)
            {
                onWarp.Invoke();
                OnDie.Invoke();
            }
        }

        private void UpdateVelocity()
        {
            // Movement
            if (thrustInput == 0)
            {
                if (Mathf.Abs(forwardVelocity) <= shipData.Acceleration * Time.fixedDeltaTime)
                    forwardVelocity = 0;
                else
                    forwardVelocity -= Mathf.Sign(forwardVelocity) * shipData.Acceleration * Time.fixedDeltaTime;
            }
            else
            {
                forwardVelocity += thrustInput * shipData.Acceleration * Time.fixedDeltaTime;
            }

            forwardVelocity = Mathf.Clamp(forwardVelocity, -shipData.BackwardSpeed, shipData.ForwardSpeed);
            model.localRotation =
                Quaternion.Euler(0, -shipData.TiltAmount * rotationVelocity / shipData.RotateSpeed, 0);
        }

        private void UpdatePosition()
        {
            if (rotationInput == 0)
            {
                if (Mathf.Abs(rotationVelocity) <= shipData.RotateAcceleration * Time.fixedDeltaTime)
                    rotationVelocity = 0;
                else
                {
                    rotationVelocity -=
                        Mathf.Sign(rotationVelocity) * shipData.RotateAcceleration * Time.fixedDeltaTime;
                }
            }
            else
            {
                rotationVelocity += rotationInput * shipData.RotateAcceleration * Time.fixedDeltaTime;
            }

            rotationVelocity = Mathf.Clamp(rotationVelocity, -shipData.RotateSpeed, shipData.RotateSpeed);

            transform.Rotate(Vector3.back * rotationVelocity * Time.fixedDeltaTime * Mathf.Sign(forwardVelocity));
            float strafeVelocity = strafeInput * shipData.StrafeSpeed;
            Vector2 velocity = new Vector2(strafeVelocity, forwardVelocity);

            rigidbody.velocity = transform.rotation * velocity;
        }
    }
}