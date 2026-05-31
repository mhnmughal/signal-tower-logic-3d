using System.Collections.Generic;
using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Activates and resets existing manually assembled levels.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [Header("Manual Level References")]
        [Tooltip("Assign Level_01 through Level_12 parent GameObjects in order.")]
        [SerializeField] private List<GameObject> levelParents = new();

        [Tooltip("Assign matching LevelConfig components in the same order as levelParents.")]
        [SerializeField] private List<LevelConfig> levelConfigs = new();

        [Header("Manager References")]
        [SerializeField] private UndoManager undoManager;
        [SerializeField] private HintManager hintManager;
        [SerializeField] private StarRatingManager starRatingManager;
        [SerializeField] private FeedbackTextUI feedbackTextUI;

        [Header("Runtime State")]
        [SerializeField] private int currentLevelNumber = 1;
        [SerializeField] private int highestUnlockedLevel = 1;

        private LevelConfig currentLevelConfig;

        public int CurrentLevelNumber => currentLevelNumber;
        public int HighestUnlockedLevel => highestUnlockedLevel;
        public int LevelCount => levelParents.Count;
        public LevelConfig CurrentLevelConfig => currentLevelConfig;

        private void Awake()
        {
            highestUnlockedLevel = Mathf.Max(1, highestUnlockedLevel);
            ActivateLevel(currentLevelNumber);
        }

        public bool ActivateLevel(int levelNumber)
        {
            if (!IsLevelNumberValid(levelNumber))
            {
                Debug.LogWarning($"Cannot activate Level_{levelNumber:00}; no assigned level parent exists.", this);
                return false;
            }

            currentLevelNumber = levelNumber;

            for (int i = 0; i < levelParents.Count; i++)
            {
                GameObject levelParent = levelParents[i];
                if (levelParent != null)
                {
                    levelParent.SetActive(i == levelNumber - 1);
                }
            }

            currentLevelConfig = GetLevelConfig(levelNumber);
            ResetCurrentLevel();
            return true;
        }

        public void RestartCurrentLevel()
        {
            ActivateLevel(currentLevelNumber);
        }

        public void ResetCurrentLevel()
        {
            if (currentLevelConfig == null)
            {
                currentLevelConfig = GetLevelConfig(currentLevelNumber);
            }

            if (currentLevelConfig == null)
            {
                return;
            }

            ResetReceivers(currentLevelConfig.Receivers);
            ResetReflectors(currentLevelConfig.Reflectors);
            ResetPowerNodes(currentLevelConfig.PowerNodes);
            ResetPulseGates(currentLevelConfig.PulseGates);
            ClearSignalBeams(currentLevelConfig.SignalBeamRenderers);
            DeactivateLevelEffects(currentLevelConfig.LevelEffects);
            undoManager?.ResetForCurrentLevel(currentLevelConfig);
            hintManager?.ResetForCurrentLevel();
            starRatingManager?.ResetForCurrentLevel();
            feedbackTextUI?.ResetFeedbackStats();
        }

        public LevelConfig GetLevelConfig(int levelNumber)
        {
            int index = levelNumber - 1;
            if (index < 0 || index >= levelConfigs.Count)
            {
                return null;
            }

            return levelConfigs[index];
        }

        public void UnlockNextLevel()
        {
            UnlockLevel(currentLevelNumber + 1);
        }

        public void UnlockLevel(int levelNumber)
        {
            if (levelNumber < 1)
            {
                return;
            }

            highestUnlockedLevel = Mathf.Max(highestUnlockedLevel, levelNumber);
        }

        public bool IsLevelUnlocked(int levelNumber)
        {
            return levelNumber <= highestUnlockedLevel;
        }

        private bool IsLevelNumberValid(int levelNumber)
        {
            int index = levelNumber - 1;
            return index >= 0 && index < levelParents.Count && levelParents[index] != null;
        }

        private static void ResetReceivers(IReadOnlyList<SignalReceiver> receivers)
        {
            for (int i = 0; i < receivers.Count; i++)
            {
                if (receivers[i] != null)
                {
                    receivers[i].ResetReceiver();
                }
            }
        }

        private static void ResetReflectors(IReadOnlyList<SignalReflector> reflectors)
        {
            for (int i = 0; i < reflectors.Count; i++)
            {
                if (reflectors[i] != null)
                {
                    reflectors[i].ResetReflector();
                    reflectors[i].OnDeselected();
                }
            }
        }

        private static void ResetPowerNodes(IReadOnlyList<PowerNode> powerNodes)
        {
            for (int i = 0; i < powerNodes.Count; i++)
            {
                if (powerNodes[i] != null)
                {
                    powerNodes[i].ResetNode();
                }
            }
        }

        private static void ResetPulseGates(IReadOnlyList<PulseGate> pulseGates)
        {
            for (int i = 0; i < pulseGates.Count; i++)
            {
                if (pulseGates[i] != null)
                {
                    pulseGates[i].ResetPulseGate();
                }
            }
        }

        private static void ClearSignalBeams(IReadOnlyList<Renderer> signalBeamRenderers)
        {
            for (int i = 0; i < signalBeamRenderers.Count; i++)
            {
                if (signalBeamRenderers[i] != null)
                {
                    signalBeamRenderers[i].enabled = false;
                }
            }
        }

        private static void DeactivateLevelEffects(IReadOnlyList<GameObject> levelEffects)
        {
            for (int i = 0; i < levelEffects.Count; i++)
            {
                if (levelEffects[i] != null)
                {
                    levelEffects[i].SetActive(false);
                }
            }
        }
    }
}
