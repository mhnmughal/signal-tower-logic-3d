using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Routes feedback messages to existing lightweight effects and camera shake.
    /// </summary>
    public class PolishFeedbackController : MonoBehaviour
    {
        [Header("Existing Effects")]
        [SerializeField] private ParticleSystem receiverActivatedParticles;
        [SerializeField] private ParticleSystem gateBlockedParticles;
        [SerializeField] private ParticleSystem powerNodeParticles;
        [SerializeField] private ParticleSystem levelCompleteParticles;
        [SerializeField] private ParticleSystem wrongActionParticles;

        [Header("Existing Camera")]
        [SerializeField] private CameraShakeOnly cameraShake;

        public void HandleFeedbackMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            switch (message)
            {
                case "Receiver powered":
                    Play(receiverActivatedParticles);
                    break;
                case "Gate locked":
                case "Signal blocked":
                    Play(gateBlockedParticles);
                    cameraShake?.ShakeInvalidAction();
                    break;
                case "Power node active":
                    Play(powerNodeParticles);
                    break;
                case "Wrong colour":
                case "Invalid action":
                case "Not enough power":
                case "No action to undo":
                    Play(wrongActionParticles);
                    cameraShake?.ShakeInvalidAction();
                    break;
                case "Level complete":
                    Play(levelCompleteParticles);
                    cameraShake?.ShakeLevelComplete();
                    break;
            }
        }

        private static void Play(ParticleSystem particles)
        {
            if (particles == null)
            {
                return;
            }

            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particles.Play(true);
        }
    }
}
