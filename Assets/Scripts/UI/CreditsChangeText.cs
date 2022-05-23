using System;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI
{
    public class CreditsChangeText : MonoBehaviour
    {
        [SerializeField] private Text text;
        private float speed;
        private float time;
        private Vector2 direction;
        private float counter;

        public void Setup(int amount, float speed, float time, Vector2 direction)
        {
            this.time = time;
            this.speed = speed;
            this.direction = direction;
            
            Destroy(gameObject, time);
            text.text = amount.ToString();
        }

        private void Update()
        {
            counter += Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);
            Color color = text.color;
            color.a = 1 - counter / time;
            text.color = color;
        }
    }
}