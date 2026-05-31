using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Updates the manually placed level select buttons.
    /// </summary>
    public class LevelSelectUI : MonoBehaviour
    {
        [System.Serializable]
        public class LevelButtonView
        {
            [SerializeField] private Button button;
            [SerializeField] private TextMeshProUGUI levelNumberText;
            [SerializeField] private GameObject lockOverlay;
            [SerializeField] private Image[] starIcons = new Image[3];

            public Button Button => button;
            public TextMeshProUGUI LevelNumberText => levelNumberText;
            public GameObject LockOverlay => lockOverlay;
            public Image[] StarIcons => starIcons;
        }

        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private SaveManager saveManager;

        [Header("Manual Level Buttons")]
        [SerializeField] private LevelButtonView[] levelButtons = new LevelButtonView[12];

        [Header("Colours")]
        [SerializeField] private Color starOnColour = new(1f, 0.82f, 0.18f, 1f);
        [SerializeField] private Color starOffColour = new(0.2f, 0.22f, 0.28f, 1f);

        private void Awake()
        {
            BindButtons();
        }

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            int highestUnlocked = saveManager != null ? saveManager.GetHighestUnlockedLevel() : 1;

            for (int i = 0; i < levelButtons.Length; i++)
            {
                LevelButtonView view = levelButtons[i];
                if (view == null)
                {
                    continue;
                }

                int levelNumber = i + 1;
                bool unlocked = levelNumber <= highestUnlocked;
                int stars = saveManager != null ? saveManager.GetStars(levelNumber) : 0;

                if (view.LevelNumberText != null)
                {
                    view.LevelNumberText.text = levelNumber.ToString("00");
                }

                if (view.LockOverlay != null)
                {
                    view.LockOverlay.SetActive(!unlocked);
                }

                if (view.Button != null)
                {
                    view.Button.interactable = unlocked;
                }

                SetStars(view.StarIcons, stars);
            }
        }

        public void OpenLevel(int levelNumber)
        {
            int highestUnlocked = saveManager != null ? saveManager.GetHighestUnlockedLevel() : 1;
            if (levelNumber > highestUnlocked)
            {
                return;
            }

            gameManager?.StartLevel(levelNumber);
        }

        private void BindButtons()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                LevelButtonView view = levelButtons[i];
                if (view == null || view.Button == null)
                {
                    continue;
                }

                int levelNumber = i + 1;
                view.Button.onClick.RemoveListener(() => OpenLevel(levelNumber));
                view.Button.onClick.AddListener(() => OpenLevel(levelNumber));
            }
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
    }
}
