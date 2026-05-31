using System.Collections;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Adds a tiny show animation to existing UI panels when they are enabled.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelShowAnimator : MonoBehaviour
    {
        [SerializeField] private float duration = 0.12f;
        [SerializeField] private float startScale = 0.985f;

        private CanvasGroup canvasGroup;
        private Coroutine animationRoutine;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (animationRoutine != null)
            {
                StopCoroutine(animationRoutine);
            }

            animationRoutine = StartCoroutine(ShowRoutine());
        }

        private IEnumerator ShowRoutine()
        {
            float elapsed = 0f;
            canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * startScale;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, duration));
                canvasGroup.alpha = t;
                transform.localScale = Vector3.Lerp(Vector3.one * startScale, Vector3.one, t);
                yield return null;
            }

            canvasGroup.alpha = 1f;
            transform.localScale = Vector3.one;
            animationRoutine = null;
        }
    }
}
