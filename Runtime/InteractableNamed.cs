using UnityEngine;

namespace JiRath.InteractSystem
{
    public class InteractableNamed : Interactable
    {
        [Tooltip("The name of the interactable")]
        public string displayName = "";

        public override bool CanInteract(GameObject Interactor)
        {
            return true;
        }

        public override string GetName()
        {
            string name = base.GetName();

            if (nameVisible)
                name = displayName;
            return name;
        }
    }
}