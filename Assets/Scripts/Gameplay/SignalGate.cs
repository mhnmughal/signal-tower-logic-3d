using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Colour gate that allows only matching signals while open.
    /// </summary>
    public class SignalGate : InteractableObject
    {
        [Header("Gate")]
        [SerializeField] private SignalColour acceptedColour;
        [SerializeField] private SignalTile gateTile;
        [SerializeField] private bool isOpen = true;

        public SignalColour AcceptedColour => acceptedColour;
        public SignalTile GateTile => gateTile;
        public bool IsOpen => isOpen;

        public override void OnActivate()
        {
            base.OnActivate();
            SetOpen(!isOpen);
        }

        public bool CanPass(SignalColour signalColour)
        {
            return isOpen && (acceptedColour == SignalColour.None || acceptedColour == signalColour);
        }

        public void SetOpen(bool open)
        {
            isOpen = open;
        }
    }
}
