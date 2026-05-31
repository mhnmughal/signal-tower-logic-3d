using SignalTowerLogic.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Shows existing UI panels and updates assigned UI widgets.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private LevelSelectUI levelSelectUI;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private HintManager hintManager;
        [SerializeField] private FeedbackTextUI feedbackTextUI;
        [SerializeField] private AudioManager audioManager;

        [Header("Panels")]
        [SerializeField] private GameObject titleScreen;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject levelSelectPanel;
        [SerializeField] private GameObject gameplayHUD;
        [SerializeField] private GameObject mobileControlsPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private GameObject feedbackTextPanel;

        [Header("HUD Text")]
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private TextMeshProUGUI powerNumberText;
        [SerializeField] private TextMeshProUGUI actionCountText;
        [SerializeField] private TextMeshProUGUI activeReceiversCountText;
        [SerializeField] private TextMeshProUGUI requiredReceiversCountText;
        [SerializeField] private TextMeshProUGUI signalColourIndicatorText;
        [SerializeField] private TextMeshProUGUI feedbackText;

        [Header("HUD Visuals")]
        [SerializeField] private Slider powerSlider;
        [SerializeField] private Image powerBarFill;
        [SerializeField] private Image[] hudStarIcons = new Image[3];
        [SerializeField] private Color starOnColour = new(1f, 0.82f, 0.18f, 1f);
        [SerializeField] private Color starOffColour = new(0.2f, 0.22f, 0.28f, 1f);

        [Header("Settings")]
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Toggle vibrationToggle;

        [Header("Level Complete")]
        [SerializeField] private Image[] completeStarIcons = new Image[3];
        [SerializeField] private TextMeshProUGUI powerRemainingText;
        [SerializeField] private TextMeshProUGUI actionsUsedText;
        [SerializeField] private TextMeshProUGUI receiversPoweredText;

        [Header("Game Over")]
        [SerializeField] private TextMeshProUGUI failureReasonText;

        [Header("Tutorial")]
        [SerializeField] private TextMeshProUGUI tutorialMessageText;

        private int lastStarsEarned;
        private Coroutine lowPowerPulseRoutine;
        private Coroutine starPulseRoutine;

        private void Start()
        {
            LoadSettingsWidgets();
            RefreshHUD();
        }

        public void OnGameStateChanged(GameManager.GameState state)
        {
            ShowPanelForState(state);
            RefreshHUD();
        }

        public void ShowFeedback(string message)
        {
            if (feedbackTextUI != null)
            {
                feedbackTextUI.ShowFeedback(message);
                return;
            }

            if (feedbackTextPanel != null)
            {
                feedbackTextPanel.SetActive(true);
            }

            if (feedbackText != null)
            {
                feedbackText.text = message;
            }
        }

        public void RefreshHUD()
        {
            LevelConfig config = levelManager != null ? levelManager.CurrentLevelConfig : null;
            int levelNumber = levelManager != null ? levelManager.CurrentLevelNumber : 1;
            int poweredReceivers = CountPoweredReceivers(config);
            int requiredReceivers = config != null ? config.RequiredReceiverCount : 0;
            int powerBudget = config != null ? config.PowerBudget : 0;

            SetText(currentLevelText, $"Level {levelNumber:00}");
            SetText(objectiveText, config != null ? config.ObjectiveText : "Route the signals.");
            SetText(powerNumberText, powerBudget > 0 ? $"{powerBudget}" : "--");
            SetText(actionCountText, "Actions 0");
            SetText(activeReceiversCountText, poweredReceivers.ToString());
            SetText(requiredReceiversCountText, requiredReceivers > 0 ? requiredReceivers.ToString() : "--");
            SetText(signalColourIndicatorText, "Signal: Red / Blue / Green / Yellow");

            if (powerSlider != null)
            {
                powerSlider.minValue = 0f;
                powerSlider.maxValue = Mathf.Max(1, powerBudget);
                powerSlider.value = Mathf.Max(0, powerBudget);
            }

            if (powerBarFill != null)
            {
                powerBarFill.fillAmount = powerBudget > 0 ? 1f : 0f;
            }
        }

        public void UpdateActionCount(int usedActions, int actionLimit)
        {
            string limit = actionLimit > 0 ? actionLimit.ToString() : "--";
            SetText(actionCountText, $"Actions {usedActions}/{limit}");
        }

        public void UpdatePower(int powerSpent, int powerBudget)
        {
            int remaining = Mathf.Max(0, powerBudget - powerSpent);
            SetText(powerNumberText, $"{remaining}/{Mathf.Max(0, powerBudget)}");

            if (powerSlider != null)
            {
                powerSlider.maxValue = Mathf.Max(1, powerBudget);
                powerSlider.value = remaining;
            }

            if (powerBarFill != null)
            {
                powerBarFill.fillAmount = powerBudget > 0 ? Mathf.Clamp01((float)remaining / powerBudget) : 0f;
            }

            UpdateLowPowerPulse(remaining, powerBudget);
        }

        public void UpdateReceiverCounts(int poweredReceivers, int requiredReceivers)
        {
            SetText(activeReceiversCountText, poweredReceivers.ToString());
            SetText(requiredReceiversCountText, requiredReceivers.ToString());
        }

        public void UpdateStarDisplay(int stars)
        {
            lastStarsEarned = Mathf.Clamp(stars, 0, 3);
            SetStars(hudStarIcons, lastStarsEarned);
            SetStars(completeStarIcons, lastStarsEarned);
            StartStarRewardPulse();
        }

        public void ShowLevelComplete(int stars, int powerRemaining, int actionsUsed, int receiversPowered)
        {
            UpdateStarDisplay(stars);
            SetText(powerRemainingText, $"Power remaining: {powerRemaining}");
            SetText(actionsUsedText, $"Actions used: {actionsUsed}");
            SetText(receiversPoweredText, $"Receivers powered: {receiversPowered}");
            ShowOnly(levelCompletePanel);
        }

        public void ShowGameOver(string reason)
        {
            SetText(failureReasonText, reason);
            ShowOnly(gameOverPanel);
        }

        public void SetTutorialMessage(string message)
        {
            SetText(tutorialMessageText, message);
            ShowOnly(tutorialPanel);
        }

        public void StartButton()
        {
            PlayUIButtonClick();
            gameManager?.ShowMainMenu();
        }

        public void PlayButton()
        {
            PlayUIButtonClick();
            gameManager?.StartGameFromMainMenu();
        }

        public void LevelSelectButton()
        {
            PlayUIButtonClick();
            levelSelectUI?.Refresh();
            gameManager?.ShowLevelSelect();
        }

        public void SettingsButton()
        {
            PlayUIButtonClick();
            LoadSettingsWidgets();
            ShowOnly(settingsPanel);
        }

        public void CreditsButton()
        {
            PlayUIButtonClick();
            ShowOnly(creditsPanel);
        }

        public void BackToMainMenuButton()
        {
            PlayUIButtonClick();
            gameManager?.ShowMainMenu();
        }

        public void ResumeButton()
        {
            PlayUIButtonClick();
            gameManager?.ResumeGame();
        }

        public void RestartButton()
        {
            PlayUIButtonClick();
            gameManager?.RestartCurrentLevel();
        }

        public void NextLevelButton()
        {
            PlayUIButtonClick();
            int nextLevel = levelManager != null ? levelManager.CurrentLevelNumber + 1 : 1;
            gameManager?.StartLevel(nextLevel);
        }

        public void RetryButton()
        {
            PlayUIButtonClick();
            gameManager?.RestartCurrentLevel();
        }

        public void HintButton()
        {
            PlayUIButtonClick();
            hintManager?.ShowHint();
        }

        public void QuitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SaveMusicVolume(float value)
        {
            audioManager?.SetMusicVolume(value);
            if (audioManager == null)
            {
                saveManager?.SaveMusicVolume(value);
            }
        }

        public void SaveSFXVolume(float value)
        {
            audioManager?.SetSFXVolume(value);
            if (audioManager == null)
            {
                saveManager?.SaveSFXVolume(value);
            }
        }

        public void SaveVibration(bool value)
        {
            saveManager?.SaveVibration(value);
        }

        public void ResetProgressButton()
        {
            PlayUIButtonClick();
            saveManager?.ResetProgress();
            levelSelectUI?.Refresh();
            LoadSettingsWidgets();
        }

        public void TutorialContinueButton()
        {
            PlayUIButtonClick();
            tutorialManager?.ContinueTutorial();
        }

        public void TutorialSkipButton()
        {
            PlayUIButtonClick();
            tutorialManager?.SkipTutorial();
        }

        public void ShowGameplayPanels()
        {
            ShowGameplay();
        }

        private void ShowPanelForState(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.Title:
                    ShowOnly(titleScreen);
                    break;
                case GameManager.GameState.MainMenu:
                    ShowOnly(mainMenuPanel);
                    break;
                case GameManager.GameState.LevelSelect:
                    levelSelectUI?.Refresh();
                    ShowOnly(levelSelectPanel);
                    break;
                case GameManager.GameState.Playing:
                    ShowGameplay();
                    break;
                case GameManager.GameState.Paused:
                    ShowOnly(pausePanel);
                    break;
                case GameManager.GameState.LevelComplete:
                    ShowOnly(levelCompletePanel);
                    break;
                case GameManager.GameState.GameOver:
                    ShowOnly(gameOverPanel);
                    break;
            }
        }

        private void ShowGameplay()
        {
            SetAllPanels(false);
            SetActive(gameplayHUD, true);
            SetActive(mobileControlsPanel, true);
            SetActive(feedbackTextPanel, true);
        }

        private void ShowOnly(GameObject panel)
        {
            SetAllPanels(false);
            SetActive(panel, true);
        }

        private void SetAllPanels(bool active)
        {
            SetActive(titleScreen, active);
            SetActive(mainMenuPanel, active);
            SetActive(levelSelectPanel, active);
            SetActive(gameplayHUD, active);
            SetActive(mobileControlsPanel, active);
            SetActive(pausePanel, active);
            SetActive(settingsPanel, active);
            SetActive(levelCompletePanel, active);
            SetActive(gameOverPanel, active);
            SetActive(creditsPanel, active);
            SetActive(tutorialPanel, active);
            SetActive(feedbackTextPanel, active);
        }

        private void LoadSettingsWidgets()
        {
            if (saveManager == null)
            {
                return;
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.SetValueWithoutNotify(saveManager.GetMusicVolume());
            }

            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.SetValueWithoutNotify(saveManager.GetSFXVolume());
            }

            if (vibrationToggle != null)
            {
                vibrationToggle.SetIsOnWithoutNotify(saveManager.GetVibration());
            }
        }

        private void PlayUIButtonClick()
        {
            audioManager?.PlayUIButtonClick();
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

        private void SetStars(Image[] stars, int activeStars)
        {
            if (stars == null)
            {
                return;
            }

            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] != null)
                {
                    stars[i].color = i < activeStars ? starOnColour : starOffColour;
                }
            }
        }

        private void UpdateLowPowerPulse(int remaining, int powerBudget)
        {
            if (!Application.isPlaying || powerBarFill == null || powerBudget <= 0)
            {
                return;
            }

            bool lowPower = remaining <= Mathf.Max(1, Mathf.CeilToInt(powerBudget * 0.25f));
            if (lowPower && lowPowerPulseRoutine == null)
            {
                lowPowerPulseRoutine = StartCoroutine(LowPowerPulseRoutine());
            }
            else if (!lowPower && lowPowerPulseRoutine != null)
            {
                StopCoroutine(lowPowerPulseRoutine);
                lowPowerPulseRoutine = null;
                powerBarFill.transform.localScale = Vector3.one;
            }
        }

        private System.Collections.IEnumerator LowPowerPulseRoutine()
        {
            while (true)
            {
                float pulse = 1f + Mathf.Sin(Time.unscaledTime * 8f) * 0.045f;
                powerBarFill.transform.localScale = new Vector3(pulse, pulse, 1f);
                yield return null;
            }
        }

        private void StartStarRewardPulse()
        {
            if (!Application.isPlaying || completeStarIcons == null || completeStarIcons.Length == 0)
            {
                return;
            }

            if (starPulseRoutine != null)
            {
                StopCoroutine(starPulseRoutine);
            }

            starPulseRoutine = StartCoroutine(StarRewardPulseRoutine());
        }

        private System.Collections.IEnumerator StarRewardPulseRoutine()
        {
            float elapsed = 0f;
            const float duration = 0.45f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float pulse = 1f + Mathf.Sin(t * Mathf.PI) * 0.18f;

                for (int i = 0; i < completeStarIcons.Length; i++)
                {
                    if (completeStarIcons[i] != null)
                    {
                        completeStarIcons[i].transform.localScale = new Vector3(pulse, pulse, 1f);
                    }
                }

                yield return null;
            }

            for (int i = 0; i < completeStarIcons.Length; i++)
            {
                if (completeStarIcons[i] != null)
                {
                    completeStarIcons[i].transform.localScale = Vector3.one;
                }
            }

            starPulseRoutine = null;
        }

        private static void SetText(TextMeshProUGUI text, string value)
        {
            if (text != null)
            {
                text.text = value;
            }
        }

        private static void SetActive(GameObject panel, bool active)
        {
            if (panel != null)
            {
                panel.SetActive(active);
            }
        }
    }
}
