using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Shows LevelConfig hint text through existing UI and tracks hint use for scoring.
    /// </summary>
    public class HintManager : MonoBehaviour
    {
        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private GameObject feedbackTarget;

        [Header("Scoring")]
        [SerializeField] private bool hintCapsStarsAtTwo = true;

        private int currentLevelNumber = 1;
        private bool hintUsedThisAttempt;

        public bool HintUsedThisAttempt => hintUsedThisAttempt;
        public bool HintCapsStarsAtTwo => hintCapsStarsAtTwo;

        private void Awake()
        {
            ResetForCurrentLevel();
        }

        public void ResetForCurrentLevel()
        {
            currentLevelNumber = levelManager != null ? levelManager.CurrentLevelNumber : currentLevelNumber;
            hintUsedThisAttempt = false;
        }

        public void ShowHint()
        {
            LevelConfig config = levelManager != null ? levelManager.CurrentLevelConfig : null;
            string hint = config != null && !string.IsNullOrWhiteSpace(config.HintText)
                ? config.HintText
                : "No hint available for this level yet.";

            hintUsedThisAttempt = true;
            currentLevelNumber = levelManager != null ? levelManager.CurrentLevelNumber : currentLevelNumber;
            saveManager?.SaveHintUsed(currentLevelNumber, true);
            ShowFeedback(hint);
            ShowFeedback("Hint used");
        }

        private void ShowFeedback(string message)
        {
            if (feedbackTarget != null)
            {
                feedbackTarget.SendMessage("ShowFeedback", message, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
