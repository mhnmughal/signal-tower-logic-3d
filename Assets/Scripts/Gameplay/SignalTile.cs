using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Represents one existing manually placed grid tile in a level.
    /// </summary>
    public class SignalTile : MonoBehaviour
    {
        [Header("Tile State")]
        [SerializeField] private Vector2Int gridPosition;
        [SerializeField] private bool isBlocked;
        [SerializeField] private InteractableObject currentOccupant;

        public Vector2Int GridPosition => gridPosition;
        public bool IsBlocked => isBlocked;
        public InteractableObject CurrentOccupant => currentOccupant;

        public void SetOccupant(InteractableObject occupant)
        {
            currentOccupant = occupant;
        }

        public void SetBlocked(bool blocked)
        {
            isBlocked = blocked;
        }
    }
}
