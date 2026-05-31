using System.Collections.Generic;
using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Splits one incoming signal into configured output directions.
    /// </summary>
    public class SignalSplitter : InteractableObject
    {
        [Header("Splitter")]
        [SerializeField] private SignalDirection inputDirection;
        [SerializeField] private SignalTile splitterTile;
        [SerializeField] private List<SignalDirection> outputDirections = new();

        public SignalDirection InputDirection => inputDirection;
        public SignalTile SplitterTile => splitterTile;

        public IReadOnlyList<SignalDirection> GetOutputDirections()
        {
            return outputDirections;
        }
    }
}
