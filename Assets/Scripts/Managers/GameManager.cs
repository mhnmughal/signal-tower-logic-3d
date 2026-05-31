using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Coordinates high-level game flow without creating UI, levels, or scene objects.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Title,
            MainMenu,
            LevelSelect,
            Playing,
            Paused,
            LevelComplete,
            GameOver
        }

        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private UndoManager undoManager;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private HintManager hintManager;
        [SerializeField] private StarRatingManager starRatingManager;
        [SerializeField] private SignalPathCalculator signalPathCalculator;

        [Tooltip("Optional existing UI manager object. UI methods are notified through this reference only.")]
        [SerializeField] private GameObject uiManager;

        [Header("Startup")]
        [SerializeField] private GameState startingState = GameState.Title;
        [SerializeField] private int startingLevel = 1;

        private GameState currentState;
        private GameState previousState;

        public GameState CurrentState => currentState;
        public GameState PreviousState => previousState;

        private void Awake()
        {
            ChangeState(startingState);
            Time.timeScale = 1f;
        }

        public void ShowTitle()
        {
            Time.timeScale = 1f;
            ChangeState(GameState.Title);
        }

        public void ShowMainMenu()
        {
            Time.timeScale = 1f;
            ChangeState(GameState.MainMenu);
        }

        public void ShowLevelSelect()
        {
            Time.timeScale = 1f;
            ChangeState(GameState.LevelSelect);
        }

        public void StartGameFromMainMenu()
        {
            int levelToLoad = saveManager != null
                ? Mathf.Clamp(saveManager.GetHighestUnlockedLevel(), 1, Mathf.Max(1, levelManager != null ? levelManager.LevelCount : startingLevel))
                : startingLevel;

            StartLevel(levelToLoad);
        }

        public void StartLevel(int levelNumber)
        {
            if (levelManager == null)
            {
                Debug.LogWarning("GameManager cannot start a level because LevelManager is not assigned.", this);
                return;
            }

            if (levelManager.ActivateLevel(levelNumber))
            {
                Time.timeScale = 1f;
                undoManager?.SetUndoEnabled(true);
                ChangeState(GameState.Playing);
                hintManager?.ResetForCurrentLevel();
                starRatingManager?.ResetForCurrentLevel();
                tutorialManager?.TryShowTutorialForCurrentLevel();
                if (tutorialManager == null || !tutorialManager.TutorialOpen)
                {
                    signalPathCalculator?.RecalculateSignalPaths();
                }
            }
        }

        public void PauseGame()
        {
            if (currentState != GameState.Playing)
            {
                return;
            }

            Time.timeScale = 0f;
            ChangeState(GameState.Paused);
        }

        public void ResumeGame()
        {
            if (currentState != GameState.Paused)
            {
                return;
            }

            Time.timeScale = 1f;
            ChangeState(GameState.Playing);
        }

        public void RestartCurrentLevel()
        {
            if (levelManager == null)
            {
                Debug.LogWarning("GameManager cannot restart because LevelManager is not assigned.", this);
                return;
            }

            Time.timeScale = 1f;
            levelManager.RestartCurrentLevel();
            undoManager?.SetUndoEnabled(true);
            ChangeState(GameState.Playing);
            hintManager?.ResetForCurrentLevel();
            starRatingManager?.ResetForCurrentLevel();
            tutorialManager?.TryShowTutorialForCurrentLevel();
            if (tutorialManager == null || !tutorialManager.TutorialOpen)
            {
                signalPathCalculator?.RecalculateSignalPaths();
            }
        }

        public void TriggerLevelComplete()
        {
            int starsEarned = starRatingManager != null ? starRatingManager.CalculateStars() : 1;
            TriggerLevelComplete(starsEarned);
        }

        public void TriggerLevelComplete(int starsEarned)
        {
            Time.timeScale = 1f;
            undoManager?.SetUndoEnabled(false);

            if (levelManager != null)
            {
                int completedLevel = levelManager.CurrentLevelNumber;
                levelManager.UnlockNextLevel();

                if (saveManager != null)
                {
                    saveManager.SaveStars(completedLevel, starsEarned);
                    saveManager.UnlockLevel(completedLevel + 1);
                }
            }

            ChangeState(GameState.LevelComplete);
            starRatingManager?.CompleteCurrentLevel(starsEarned);
        }

        public void TriggerGameOver()
        {
            Time.timeScale = 1f;
            undoManager?.SetUndoEnabled(false);
            ChangeState(GameState.GameOver);
        }

        private void ChangeState(GameState nextState)
        {
            previousState = currentState;
            currentState = nextState;
            NotifyUIStateChanged(nextState);
        }

        private void NotifyUIStateChanged(GameState state)
        {
            if (uiManager == null)
            {
                return;
            }

            uiManager.SendMessage("OnGameStateChanged", state, SendMessageOptions.DontRequireReceiver);
        }
    }
}
