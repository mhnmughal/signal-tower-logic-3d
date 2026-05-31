using System.Collections.Generic;
using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Tracks reversible player actions against existing scene objects.
    /// </summary>
    public class UndoManager : MonoBehaviour
    {
        private enum UndoActionType
        {
            ReflectorRotation,
            PowerNodeActivation
        }

        private sealed class UndoRecord
        {
            public UndoActionType ActionType;
            public SignalReflector Reflector;
            public SignalDirection ReflectorDirection;
            public PowerNode PowerNode;
            public bool PowerNodeActivated;
            public int ActionCount;
            public int PowerSpent;
            public readonly List<PulseGate> PulseGates = new();
            public readonly List<bool> PulseGateStates = new();
        }

        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SignalPathCalculator signalPathCalculator;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameManager gameManager;

        [Tooltip("Optional existing feedback object. This manager sends ShowFeedback(string) if present.")]
        [SerializeField] private GameObject feedbackTarget;

        [Header("Runtime State")]
        [SerializeField] private int currentActionCount;
        [SerializeField] private int currentPowerSpent;
        [SerializeField] private bool undoEnabled = true;

        private readonly Stack<UndoRecord> undoStack = new();
        private LevelConfig activeLevelConfig;

        public int CurrentActionCount => currentActionCount;
        public int CurrentPowerSpent => currentPowerSpent;
        public bool CanUndo => undoEnabled && undoStack.Count > 0;

        private void Awake()
        {
            if (levelManager != null)
            {
                ResetForCurrentLevel(levelManager.CurrentLevelConfig);
            }
        }

        public void ResetForCurrentLevel(LevelConfig levelConfig)
        {
            activeLevelConfig = levelConfig;
            currentActionCount = 0;
            currentPowerSpent = 0;
            undoStack.Clear();
            undoEnabled = true;
            UpdateHUD();
        }

        public void SetUndoEnabled(bool enabled)
        {
            undoEnabled = enabled;

            if (!enabled)
            {
                undoStack.Clear();
            }
        }

        public bool TryRotateReflector(SignalReflector reflector)
        {
            if (reflector == null || !undoEnabled)
            {
                ShowFeedback("Invalid action");
                return false;
            }

            int actionCost = Mathf.Max(0, reflector.RotationCost);
            if (!CanSpendAction(actionCost))
            {
                ShowFeedback("Invalid action");
                return false;
            }

            UndoRecord record = CreateBaseRecord(UndoActionType.ReflectorRotation);
            record.Reflector = reflector;
            record.ReflectorDirection = reflector.CurrentDirection;
            undoStack.Push(record);

            reflector.RotateReflector();
            currentActionCount += actionCost;
            TogglePulseGatesAfterAction();
            RecalculateSignals();
            UpdateHUD();
            ShowFeedback("Reflector rotated");
            CheckGameOverAfterAction();
            return true;
        }

        public bool TryActivatePowerNode(PowerNode powerNode)
        {
            if (powerNode == null || !undoEnabled || powerNode.IsActivated)
            {
                ShowFeedback("Invalid action");
                return false;
            }

            int powerCost = Mathf.Max(0, powerNode.ActivationCost);
            if (!CanSpendAction(1))
            {
                ShowFeedback("Invalid action");
                return false;
            }

            if (!CanSpendPower(powerCost))
            {
                ShowFeedback("Not enough power");
                return false;
            }

            UndoRecord record = CreateBaseRecord(UndoActionType.PowerNodeActivation);
            record.PowerNode = powerNode;
            record.PowerNodeActivated = powerNode.IsActivated;
            undoStack.Push(record);

            powerNode.ActivateNode();
            currentActionCount++;
            currentPowerSpent += powerCost;
            TogglePulseGatesAfterAction();
            RecalculateSignals();
            UpdateHUD();
            ShowFeedback("Power node active");
            CheckGameOverAfterAction();
            return true;
        }

        public void UndoLastAction()
        {
            if (!CanUndo)
            {
                ShowFeedback("No action to undo");
                return;
            }

            UndoRecord record = undoStack.Pop();
            currentActionCount = record.ActionCount;
            currentPowerSpent = record.PowerSpent;

            if (record.ActionType == UndoActionType.ReflectorRotation && record.Reflector != null)
            {
                record.Reflector.SetDirection(record.ReflectorDirection);
            }

            if (record.ActionType == UndoActionType.PowerNodeActivation && record.PowerNode != null)
            {
                record.PowerNode.SetActivated(record.PowerNodeActivated);
            }

            RestorePulseGateStates(record);
            RecalculateSignals();
            UpdateHUD();
            ShowFeedback("Undo complete");
        }

        public void ClearUndoHistory()
        {
            undoStack.Clear();
        }

        private UndoRecord CreateBaseRecord(UndoActionType actionType)
        {
            UndoRecord record = new()
            {
                ActionType = actionType,
                ActionCount = currentActionCount,
                PowerSpent = currentPowerSpent
            };

            CapturePulseGateStates(record);
            return record;
        }

        private bool CanSpendAction(int actionCost)
        {
            LevelConfig config = GetActiveConfig();
            return config == null || config.ActionLimit <= 0 || currentActionCount + actionCost <= config.ActionLimit;
        }

        private bool CanSpendPower(int powerCost)
        {
            LevelConfig config = GetActiveConfig();
            return config == null || config.PowerBudget <= 0 || currentPowerSpent + powerCost <= config.PowerBudget;
        }

        private LevelConfig GetActiveConfig()
        {
            if (levelManager != null && levelManager.CurrentLevelConfig != null)
            {
                activeLevelConfig = levelManager.CurrentLevelConfig;
            }

            return activeLevelConfig;
        }

        private void CapturePulseGateStates(UndoRecord record)
        {
            LevelConfig config = GetActiveConfig();
            if (config == null)
            {
                return;
            }

            for (int i = 0; i < config.PulseGates.Count; i++)
            {
                PulseGate pulseGate = config.PulseGates[i];
                if (pulseGate == null)
                {
                    continue;
                }

                record.PulseGates.Add(pulseGate);
                record.PulseGateStates.Add(pulseGate.IsOpen);
            }
        }

        private void RestorePulseGateStates(UndoRecord record)
        {
            for (int i = 0; i < record.PulseGates.Count && i < record.PulseGateStates.Count; i++)
            {
                if (record.PulseGates[i] != null)
                {
                    record.PulseGates[i].SetOpen(record.PulseGateStates[i]);
                }
            }
        }

        private void TogglePulseGatesAfterAction()
        {
            LevelConfig config = GetActiveConfig();
            if (config == null)
            {
                return;
            }

            for (int i = 0; i < config.PulseGates.Count; i++)
            {
                PulseGate pulseGate = config.PulseGates[i];
                if (pulseGate != null && pulseGate.PulseAfterEachAction)
                {
                    pulseGate.TogglePulseState();
                }
            }
        }

        private void RecalculateSignals()
        {
            signalPathCalculator?.RecalculateSignalPaths();
        }

        private void UpdateHUD()
        {
            LevelConfig config = GetActiveConfig();
            if (config == null || uiManager == null)
            {
                return;
            }

            uiManager.UpdateActionCount(currentActionCount, config.ActionLimit);
            uiManager.UpdatePower(currentPowerSpent, config.PowerBudget);
        }

        private void CheckGameOverAfterAction()
        {
            LevelConfig config = GetActiveConfig();
            if (!Application.isPlaying || config == null || gameManager == null || gameManager.CurrentState != GameManager.GameState.Playing)
            {
                return;
            }

            if (config.ActionLimit > 0 && currentActionCount >= config.ActionLimit && !HasPoweredRequiredReceivers(config))
            {
                gameManager.TriggerGameOver();
            }
        }

        private static bool HasPoweredRequiredReceivers(LevelConfig config)
        {
            int powered = 0;
            for (int i = 0; i < config.Receivers.Count; i++)
            {
                if (config.Receivers[i] != null && config.Receivers[i].IsPowered)
                {
                    powered++;
                }
            }

            return config.RequiredReceiverCount > 0 && powered >= config.RequiredReceiverCount;
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
