using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Base class for selectable gameplay objects already placed in the scene.
    /// </summary>
    public class InteractableObject : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField] private string objectName;
        [SerializeField] private bool canSelect = true;
        [SerializeField] private bool canActivate;
        [SerializeField] private bool canRotate;

        public string ObjectName => objectName;
        public bool CanSelect => canSelect;
        public bool CanActivate => canActivate;
        public bool CanRotate => canRotate;

        public virtual void OnSelected()
        {
            if (!canSelect)
            {
                return;
            }
        }

        public virtual void OnDeselected()
        {
        }

        public virtual void OnActivate()
        {
            if (!canActivate)
            {
                return;
            }
        }

        public virtual void OnRotate()
        {
            if (!canRotate)
            {
                return;
            }
        }
    }
}
