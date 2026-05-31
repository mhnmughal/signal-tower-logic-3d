using System.Collections;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Applies tiny temporary shake to the existing manually placed camera.
    /// </summary>
    public class CameraShakeOnly : MonoBehaviour
    {
        [Header("Shake")]
        [SerializeField] private float invalidActionDuration = 0.12f;
        [SerializeField] private float invalidActionStrength = 0.035f;
        [SerializeField] private float levelCompleteDuration = 0.22f;
        [SerializeField] private float levelCompleteStrength = 0.055f;

        private Vector3 originalLocalPosition;
        private Coroutine shakeRoutine;

        private void Start()
        {
            originalLocalPosition = transform.localPosition;
        }

        public void ShakeInvalidAction()
        {
            Shake(invalidActionDuration, invalidActionStrength);
        }

        public void ShakeLevelComplete()
        {
            Shake(levelCompleteDuration, levelCompleteStrength);
        }

        private void Shake(float duration, float strength)
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (shakeRoutine != null)
            {
                StopCoroutine(shakeRoutine);
                transform.localPosition = originalLocalPosition;
            }

            shakeRoutine = StartCoroutine(ShakeRoutine(duration, strength));
        }

        private IEnumerator ShakeRoutine(float duration, float strength)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float fade = 1f - Mathf.Clamp01(elapsed / Mathf.Max(0.01f, duration));
                Vector2 offset = Random.insideUnitCircle * strength * fade;
                transform.localPosition = originalLocalPosition + new Vector3(offset.x, offset.y, 0f);
                yield return null;
            }

            transform.localPosition = originalLocalPosition;
            shakeRoutine = null;
        }
    }
}
