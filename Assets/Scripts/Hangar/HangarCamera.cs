using UnityEngine;

namespace Spaceships.Hangar
{
    [RequireComponent(typeof(Camera))]
    public class HangarCamera : MonoBehaviour
    {
        [SerializeField] private float swayAmount = 25f;
        [SerializeField] private float swaySpeed = 2f;
        private float originalAngle;

        private void Start()
        {
            originalAngle = transform.eulerAngles.y;
        }

        private void Update()
        {
            float angle = Mathf.Sin(Time.time / swaySpeed) * Mathf.Rad2Deg * swayAmount;
            Vector3 currentAngles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(currentAngles.x, originalAngle + angle, currentAngles.z);
        }
    }
}