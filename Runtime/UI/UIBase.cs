using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JiRath.InteractSystem.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        private static List<UIBase> UIList = new List<UIBase>();
        [HideInInspector]
        protected GameObject owningPlayer;
        [Tooltip("Canvas to toggle active when using Inventory UI"), SerializeField]
        protected Canvas canvasReference;
        public bool enableUIOverlap = false;

        public event Action<bool> OnDisablePlayer;

        protected virtual void OnEnable()
        {
            if (!enableUIOverlap && !UIList.Contains(this))
                UIList.Add(this);
        }

        protected virtual void OnDisable()
        {
            if (!enableUIOverlap && UIList.Contains(this))
                UIList.Remove(this);
        }

        /// <summary>
        /// Bind the UI to the inputted GameObject
        /// </summary>
        /// <param name="owner">The GameObject to bind to</param>
        /// <returns></returns>
        public virtual void Bind(GameObject owner)
        {
            if (owner)
                owningPlayer = owner;
        }

        public abstract bool IsEnabled();

        public bool CanEnable()
        {
            foreach (UIBase UI in UIList)
            {
                if (UI != this && UI.IsEnabled())
                    return false;
            }
            return true;
        }

        public virtual void DisablePlayer(bool disablePlayer)
        {
            if (disablePlayer)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            OnDisablePlayer?.Invoke(disablePlayer);
        }
    }
}