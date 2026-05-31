using System.Collections;
using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Receives a matching signal colour and tracks powered state.
    /// </summary>
    public class SignalReceiver : InteractableObject
    {
        [Header("Receiver")]
        [SerializeField] private SignalColour requiredColour;
        [SerializeField] private SignalTile receiverTile;
        [SerializeField] private bool isPowered;
        [SerializeField] private bool requiresPowerNode;
        [SerializeField] private PowerNode requiredPowerNode;

        [Header("Polish")]
        [SerializeField] private Renderer[] glowRenderers;
        [SerializeField] private Color poweredGlowColour = Color.white;
        [SerializeField] private float glowPulseScale = 1.08f;
        [SerializeField] private float glowPulseDuration = 0.18f;

        private Vector3 originalScale;
        private Coroutine glowRoutine;

        public SignalColour RequiredColour => requiredColour;
        public SignalTile ReceiverTile => receiverTile;
        public bool IsPowered => isPowered;
        public bool RequiresPowerNode => requiresPowerNode;
        public PowerNode RequiredPowerNode => requiredPowerNode;

        private void Awake()
        {
            originalScale = transform.localScale;

            if (glowRenderers == null || glowRenderers.Length == 0)
            {
                glowRenderers = GetComponentsInChildren<Renderer>(true);
            }
        }

        public void SetPowered(bool powered)
        {
            bool wasPowered = isPowered;

            if (requiresPowerNode && requiredPowerNode != null && !requiredPowerNode.IsActivated)
            {
                isPowered = false;
                return;
            }

            isPowered = powered;

            if (isPowered && !wasPowered)
            {
                PlayActivationGlow();
            }
        }

        public void ResetReceiver()
        {
            isPowered = false;
            transform.localScale = originalScale == Vector3.zero ? transform.localScale : originalScale;
        }

        private void PlayActivationGlow()
        {
            ApplyGlowColour();

            if (!Application.isPlaying)
            {
                return;
            }

            if (glowRoutine != null)
            {
                StopCoroutine(glowRoutine);
            }

            glowRoutine = StartCoroutine(GlowPulseRoutine());
        }

        private void ApplyGlowColour()
        {
            if (glowRenderers == null)
            {
                return;
            }

            for (int i = 0; i < glowRenderers.Length; i++)
            {
                if (glowRenderers[i] != null)
                {
                    glowRenderers[i].material.color = poweredGlowColour;
                }
            }
        }

        private IEnumerator GlowPulseRoutine()
        {
            Vector3 startScale = originalScale == Vector3.zero ? transform.localScale : originalScale;
            Vector3 peakScale = startScale * glowPulseScale;
            float elapsed = 0f;

            while (elapsed < glowPulseDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, glowPulseDuration));
                transform.localScale = Vector3.Lerp(startScale, peakScale, Mathf.Sin(t * Mathf.PI));
                yield return null;
            }

            transform.localScale = startScale;
            glowRoutine = null;
        }
    }
}
