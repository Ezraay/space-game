using System.Collections.Generic;
using Spaceships.Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipCombat))]
    public class Ship : MonoBehaviour
    {
        public static List<Ship> AllShips = new List<Ship>();

        [SerializeField] private ShipData shipData;

        [HideInInspector] public float rotationInput;
        [HideInInspector] public float thrustInput;
        [HideInInspector] public float strafeInput;

        [HideInInspector] public float forwardVelocity;
        [HideInInspector] public float rotationVelocity;
        private Transform model;

        private new Rigidbody2D rigidbody;

        private ShipTrail[] trails;

        public UnityEvent OnWarp { get; } = new UnityEvent();
        public ShipData ShipData => shipData;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            trails = GetComponentsInChildren<ShipTrail>();
            model = transform.GetChild(0);
            
            AllShips.Add(this);
        }

        protected virtual void Update()
        {
            foreach (ShipTrail trail in trails)
            {
                trail.SetLength(forwardVelocity / shipData.ForwardSpeed);
            }

        }

        protected virtual void FixedUpdate()
        {
            UpdateVelocity();
            UpdatePosition();
        }

        private void OnDestroy()
        {
            AllShips.Remove(this);
        }

        public void Warp()
        {
            OnWarp.Invoke();

            Destroy(gameObject, 3);
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
                rotationVelocity += rotationInput * shipData.RotateAcceleration * Time.fixedDeltaTime;


            rotationVelocity = Mathf.Clamp(rotationVelocity, -shipData.RotateSpeed, shipData.RotateSpeed);

            transform.Rotate(Vector3.back * rotationVelocity * Time.fixedDeltaTime * Mathf.Sign(forwardVelocity));
            float strafeVelocity = strafeInput * shipData.StrafeSpeed;
            Vector2 velocity = new Vector2(strafeVelocity, forwardVelocity);

            rigidbody.velocity = transform.rotation * velocity;
        }
    }
}