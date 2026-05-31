using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Calculates completion stars from authored level limits and runtime performance.
    /// </summary>
    public class StarRatingManager : MonoBehaviour
    {
        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private UndoManager undoManager;
        [SerializeField] private HintManager hintManager;
        [SerializeField] private UIManager uiManager;

        [Header("3 Star Tuning")]
        [SerializeField] private float goodPowerRemainingRatio = 0.25f;

        [Header("Runtime Mistakes")]
        [SerializeField] private int minorMistakes;
        [SerializeField] private int majorMistakes;

        public int MinorMistakes => minorMistakes;
        public int MajorMistakes => majorMistakes;

        public void ResetForCurrentLevel()
        {
            minorMistakes = 0;
            majorMistakes = 0;
        }

        public void RegisterMinorMistake()
        {
            minorMistakes++;
        }

        public void RegisterMajorMistake()
        {
            majorMistakes++;
        }

        public int CalculateStars()
        {
            LevelConfig config = levelManager != null ? levelManager.CurrentLevelConfig : null;
            if (config == null)
            {
                return 1;
            }

            int actionsUsed = undoManager != null ? undoManager.CurrentActionCount : 0;
            int powerSpent = undoManager != null ? undoManager.CurrentPowerSpent : 0;
            int powerRemaining = Mathf.Max(0, config.PowerBudget - powerSpent);
            bool hintUsed = hintManager != null && hintManager.HintUsedThisAttempt;
            bool goodPowerRemaining = config.PowerBudget <= 0 || powerRemaining >= Mathf.CeilToInt(config.PowerBudget * goodPowerRemainingRatio);

            int stars;
            if (WithinLimit(actionsUsed, config.Star3ActionLimit) && goodPowerRemaining && majorMistakes == 0 && minorMistakes == 0 && !hintUsed)
            {
                stars = 3;
            }
            else if (WithinLimit(actionsUsed, config.Star2ActionLimit) && majorMistakes <= 1)
            {
                stars = 2;
            }
            else
            {
                stars = 1;
            }

            if (hintUsed && hintManager != null && hintManager.HintCapsStarsAtTwo)
            {
                stars = Mathf.Min(stars, 2);
            }

            return Mathf.Clamp(stars, 1, 3);
        }

        public void CompleteCurrentLevel(int starsOverride = 0)
        {
            LevelConfig config = levelManager != null ? levelManager.CurrentLevelConfig : null;
            int stars = starsOverride > 0 ? Mathf.Clamp(starsOverride, 1, 3) : CalculateStars();
            int level = levelManager != null ? levelManager.CurrentLevelNumber : 1;
            int powerSpent = undoManager != null ? undoManager.CurrentPowerSpent : 0;
            int actionsUsed = undoManager != null ? undoManager.CurrentActionCount : 0;
            int powerRemaining = config != null ? Mathf.Max(0, config.PowerBudget - powerSpent) : 0;
            int receiversPowered = CountPoweredReceivers(config);

            saveManager?.SaveStars(level, stars);
            uiManager?.ShowLevelComplete(stars, powerRemaining, actionsUsed, receiversPowered);
            uiManager?.ShowFeedback("Level complete");
        }

        private static bool WithinLimit(int value, int limit)
        {
            return limit <= 0 || value <= limit;
        }

        private static int CountPoweredReceivers(LevelConfig config)
        {
            if (config == null)
            {
                return 0;
            }

            int powered = 0;
            for (int i = 0; i < config.Receivers.Count; i++)
            {
                if (config.Receivers[i] != null && config.Receivers[i].IsPowered)
                {
                    powered++;
                }
            }

            return powered;
        }
    }
}
