using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Plays authored tutorial messages from the active LevelConfig through existing UI.
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private SignalPathCalculator signalPathCalculator;

        [Header("Behaviour")]
        [SerializeField] private bool showTutorialOnlyOnce = true;

        private LevelConfig activeConfig;
        private int messageIndex;
        private bool tutorialOpen;

        public bool TutorialOpen => tutorialOpen;

        public void TryShowTutorialForCurrentLevel()
        {
            activeConfig = levelManager != null ? levelManager.CurrentLevelConfig : null;
            messageIndex = 0;

            if (activeConfig == null || activeConfig.TutorialMessages.Count == 0)
            {
                CloseTutorial(false);
                return;
            }

            if (showTutorialOnlyOnce && saveManager != null && saveManager.GetTutorialSeen())
            {
                CloseTutorial(false);
                return;
            }

            tutorialOpen = true;
            uiManager?.SetTutorialMessage(activeConfig.TutorialMessages[messageIndex]);
        }

        public void ContinueTutorial()
        {
            if (!tutorialOpen || activeConfig == null)
            {
                CloseTutorial(true);
                return;
            }

            messageIndex++;
            if (messageIndex >= activeConfig.TutorialMessages.Count)
            {
                CloseTutorial(true);
                return;
            }

            uiManager?.SetTutorialMessage(activeConfig.TutorialMessages[messageIndex]);
        }

        public void SkipTutorial()
        {
            CloseTutorial(true);
        }

        private void CloseTutorial(bool markSeen)
        {
            tutorialOpen = false;

            if (markSeen)
            {
                saveManager?.SaveTutorialSeen(true);
            }

            uiManager?.ShowGameplayPanels();
            signalPathCalculator?.RecalculateSignalPaths();
        }
    }
}
