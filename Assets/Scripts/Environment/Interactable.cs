using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.Environment
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string InteractText { get; }

        public abstract void Interact();
    }
}