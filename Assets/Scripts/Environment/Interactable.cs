using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Environment
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract string InteractText { get; }
        public abstract float InteractRadius { get; }
        public static readonly List<Interactable> AllInteractables = new List<Interactable>();

        protected virtual void OnEnable()
        {
            AllInteractables.Add(this);
        }

        protected virtual void OnDisable()
        {
            AllInteractables.Remove(this);
        }

        public abstract void Interact();
        public virtual bool CanInteract() => true;
    }
}