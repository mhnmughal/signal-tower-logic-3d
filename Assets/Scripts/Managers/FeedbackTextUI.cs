using System.Collections;
using TMPro;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Displays short feedback strings through existing TextMeshPro UI.
    /// </summary>
    public class FeedbackTextUI : MonoBehaviour
    {
        [Header("Existing UI")]
        [SerializeField] private GameObject feedbackPanel;
        [SerializeField] private TextMeshProUGUI feedbackText;

        [Header("Timing")]
        [SerializeField] private float visibleSeconds = 1.8f;
        [SerializeField] private bool keepPanelVisible;

        [Header("Optional Scoring")]
        [SerializeField] private StarRatingManager starRatingManager;

        [Header("Optional Audio")]
        [SerializeField] private AudioManager audioManager;

        [Header("Optional Polish")]
        [SerializeField] private PolishFeedbackController polishFeedbackController;

        private Coroutine hideRoutine;
        private int minorMistakeCount;
        private int majorMistakeCount;

        public int MinorMistakeCount => minorMistakeCount;
        public int MajorMistakeCount => majorMistakeCount;

        public void ShowFeedback(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            if (feedbackPanel != null)
            {
                feedbackPanel.SetActive(true);
            }

            if (feedbackText != null)
            {
                feedbackText.text = message;
            }

            TrackMistake(message);
            audioManager?.PlayFeedbackCue(message);
            polishFeedbackController?.HandleFeedbackMessage(message);

            if (hideRoutine != null)
            {
                StopCoroutine(hideRoutine);
            }

            if (!Application.isPlaying)
            {
                return;
            }

            if (!keepPanelVisible)
            {
                hideRoutine = StartCoroutine(HideAfterDelay());
            }
        }

        public void ResetFeedbackStats()
        {
            minorMistakeCount = 0;
            majorMistakeCount = 0;
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSecondsRealtime(Mathf.Max(0.1f, visibleSeconds));

            if (feedbackText != null)
            {
                feedbackText.text = string.Empty;
            }

            if (feedbackPanel != null && !keepPanelVisible)
            {
                feedbackPanel.SetActive(false);
            }

            hideRoutine = null;
        }

        private void TrackMistake(string message)
        {
            bool major = message == "Wrong colour" || message == "Signal loop detected";
            bool minor = message == "Signal blocked" || message == "Gate locked" || message == "Invalid action" || message == "Not enough power";

            if (major)
            {
                majorMistakeCount++;
                starRatingManager?.RegisterMajorMistake();
            }
            else if (minor)
            {
                minorMistakeCount++;
                starRatingManager?.RegisterMinorMistake();
            }
        }
    }
}
