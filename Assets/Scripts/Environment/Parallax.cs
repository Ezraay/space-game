using UnityEngine;

namespace Spaceships.Environment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] [Range(0f, 1f)] private float strength;
        [SerializeField] private Transform target;

        private Vector3 lastPosition;
        private float textureUnitSize;

        private void Start()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Sprite sprite = renderer.sprite;
            Texture2D texture = sprite.texture;
            textureUnitSize = texture.width / sprite.pixelsPerUnit * transform.localScale.x;
        }

        private void LateUpdate()
        {
            Vector3 deltaMovement = target.position - lastPosition;
            transform.position += new Vector3(deltaMovement.x * strength, deltaMovement.y * strength);
            lastPosition = target.position;

            if (Mathf.Abs(target.position.x - transform.position.x) > textureUnitSize)
            {
                float offset = (target.position.x - transform.position.x) % textureUnitSize;
                transform.position =
                    new Vector3(target.position.x + offset, transform.position.y, transform.position.z);
            }
            if (Mathf.Abs(target.position.y - transform.position.y) > textureUnitSize)
            {
                float offset = (target.position.y - transform.position.y) % textureUnitSize;
                transform.position =
                    new Vector3(transform.position.y, target.position.y + offset, transform.position.z);
            }
        }
    }
}