using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Emits a signal from an existing source object assigned to a tile.
    /// </summary>
    public class SignalSource : InteractableObject
    {
        [Header("Source")]
        [SerializeField] private SignalColour signalColour;
        [SerializeField] private SignalDirection outputDirection;
        [SerializeField] private SignalTile sourceTile;
        [SerializeField] private bool isActive = true;

        public SignalColour SignalColour => signalColour;
        public SignalDirection OutputDirection => outputDirection;
        public SignalTile SourceTile => sourceTile;
        public bool IsActive => isActive;

        public void SetActiveState(bool active)
        {
            isActive = active;
        }
    }
}
