
using System.Collections;
using UnityEngine;
using TMPro;

namespace JiRath.InteractSystem.UI
{
    [RequireComponent(typeof(Animator))]
    public class InteractUI : UIBase
    {
        public TMP_Text textInteractable;
        private Animator interactAnimator;

        private bool isVisible = false;

        void Awake()
        {
            //Allow interact UI to overlap with other UI
            enableUIOverlap = true;

            interactAnimator = GetComponent<Animator>();
        }

        void UI_InteractHover(Interactable interactableInView)
        {
            //Make sure player is hovering over an item
            if (interactableInView && GetInteractableMessage(interactableInView) != "")
            {
                StopCoroutine(DelayedFadeOut());
                textInteractable.SetText(GetInteractableMessage(interactableInView));

                //Make text visible if it is not already
                if (!isVisible && textInteractable.text != "")
                {
                    //Make text visible if it is not already
                    isVisible = true;
                    interactAnimator.SetBool("isVisible", isVisible);
                }

            }
            else if (isVisible)
            {
                //Delay fade out to make sure text does not blink while quickly switching targets
                isVisible = false;
                StartCoroutine(DelayedFadeOut());
            }
        }

        protected virtual string GetInteractableMessage(Interactable interactable)
        {
            string interactName = "";
            if (interactable && interactable.GetName() != "")
            {
                // Only display name for interactables that allow it
                var namedInteractable = interactable as InteractableNamed;
                if (namedInteractable)
                {
                    if (namedInteractable.nameVisible)
                        interactName = interactable.GetName();
                }
                else
                {
                    interactName = interactable.GetName();
                }
            }
            return interactName;
        }

        IEnumerator DelayedFadeOut()
        {
            yield return new WaitForSeconds(0.1f);
            interactAnimator.SetBool("isVisible", isVisible);
        }

        protected override void OnDisable()
        {
            if (owningPlayer)
                owningPlayer.GetComponent<InteractManager>().OnHoverUpdate -= UI_InteractHover;
        }

        public override void Bind(GameObject owner)
        {
            base.Bind(owner);
            owner.GetComponent<InteractManager>().OnHoverUpdate += UI_InteractHover;
        }

        public override bool IsEnabled()
        {
            return isVisible;
        }
    }
}