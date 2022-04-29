using Spaceships.Entities;
using UnityEngine;

namespace Spaceships
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] [Range(0f, 1f)] private float smoothAmount = 0.2f;
        [SerializeField] private float minZoom = 3;
        [SerializeField] private float maxZoom = 50;
        [SerializeField] private float zoomStep = 100;
        private new Camera camera;

        private float zoom;

        private void Start()
        {
            zoom = (maxZoom + minZoom) / 2;
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            float zoomChange = (maxZoom - minZoom) / zoomStep;
            zoom = Mathf.Clamp(zoom + zoomChange * InputController.zoomInput, minZoom, maxZoom);
            camera.orthographicSize = zoom;
        }

        private void FixedUpdate()
        {
            Vector3 targetPosition = player.Position;
            targetPosition.z = -10;

            transform.position = Vector3.Lerp(transform.position, targetPosition, 1 / smoothAmount * Time.deltaTime);
        }
    }
}