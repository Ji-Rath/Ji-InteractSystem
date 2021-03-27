using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JiRath.InteractSystem
{
    [SelectionBase]
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("Whether the name of the interactable should be displayed on certain UI elements")]
        public bool nameVisible = false;

        protected bool bActivated = false;

        /// <summary>
        /// Called to change the state of the Interactable (on/off)
        /// </summary>
        /// <param name="activate"></param>
        public virtual void Activate(bool bActivate)
        {
            if (bActivate != bActivated)
                OnInteract(gameObject);
        }

        public virtual void OnInteract(GameObject Interactor)
        {
            if (CanInteract(Interactor))
                bActivated = !bActivated;
        }

        public abstract bool CanInteract(GameObject Interactor);

        public virtual string GetName()
        {
            string name = "";
            if (nameVisible)
                name = gameObject.name;
            return name;
        }
    }
}