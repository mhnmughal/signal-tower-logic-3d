using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Gate that toggles between open and closed states after configured actions.
    /// </summary>
    public class PulseGate : InteractableObject
    {
        [Header("Pulse Gate")]
        [SerializeField] private bool isOpen;
        [SerializeField] private bool startsOpen = true;
        [SerializeField] private bool pulseAfterEachAction = true;
        [SerializeField] private SignalTile pulseGateTile;

        public bool IsOpen => isOpen;
        public bool StartsOpen => startsOpen;
        public bool PulseAfterEachAction => pulseAfterEachAction;
        public SignalTile PulseGateTile => pulseGateTile;

        private void Awake()
        {
            ResetPulseGate();
        }

        public override void OnActivate()
        {
            base.OnActivate();
            TogglePulseState();
        }

        public void TogglePulseState()
        {
            isOpen = !isOpen;
        }

        public void SetOpen(bool open)
        {
            isOpen = open;
        }

        public void ResetPulseGate()
        {
            isOpen = startsOpen;
        }
    }
}
