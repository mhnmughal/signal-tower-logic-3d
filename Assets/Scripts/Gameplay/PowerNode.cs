using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Optional power node that can gate receivers or later puzzle systems.
    /// </summary>
    public class PowerNode : InteractableObject
    {
        [Header("Power Node")]
        [SerializeField] private int activationCost = 1;
        [SerializeField] private bool isActivated;
        [SerializeField] private SignalTile nodeTile;

        public int ActivationCost => activationCost;
        public bool IsActivated => isActivated;
        public SignalTile NodeTile => nodeTile;

        public override void OnActivate()
        {
            base.OnActivate();
            ActivateNode();
        }

        public void ActivateNode()
        {
            isActivated = true;
        }

        public void SetActivated(bool activated)
        {
            isActivated = activated;
        }

        public void ResetNode()
        {
            isActivated = false;
        }
    }
}
