using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Marker component for an existing tile or object that blocks signal paths.
    /// </summary>
    public class BlockerTile : MonoBehaviour
    {
        [SerializeField] private SignalTile blockerTile;

        public SignalTile BlockerTileReference => blockerTile;
    }
}
