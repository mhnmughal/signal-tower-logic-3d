using System.Collections.Generic;
using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Calculates signal paths through existing Inspector-assigned level objects.
    /// </summary>
    public class SignalPathCalculator : MonoBehaviour
    {
        private readonly struct TileDirectionKey
        {
            public TileDirectionKey(SignalTile tile, SignalDirection direction, SignalColour colour)
            {
                Tile = tile;
                Direction = direction;
                Colour = colour;
            }

            private SignalTile Tile { get; }
            private SignalDirection Direction { get; }
            private SignalColour Colour { get; }
        }

        [Header("Manager References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SignalGridManager signalGridManager;
        [SerializeField] private SignalBeamRenderer signalBeamRenderer;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private UndoManager undoManager;

        [Tooltip("Optional existing feedback object. The calculator sends ShowFeedback(string) if present.")]
        [SerializeField] private GameObject feedbackTarget;

        [Header("Safety")]
        [SerializeField] private int maxStepsPerBranch = 128;

        private SignalPathResult lastResult = new();

        public SignalPathResult LastResult => lastResult;

        public SignalPathResult RecalculateSignalPaths()
        {
            LevelConfig config = levelManager != null ? levelManager.CurrentLevelConfig : null;
            return RecalculateSignalPaths(config);
        }

        public SignalPathResult RecalculateSignalPaths(LevelConfig levelConfig)
        {
            SignalPathResult result = new();

            if (levelConfig == null || signalGridManager == null)
            {
                result.AddFeedback("Signal blocked");
                SendFeedback(result);
                return result;
            }

            signalBeamRenderer?.UseLevelBeamRenderers(levelConfig);
            signalGridManager.LoadLevel(levelConfig);
            ResetReceivers(levelConfig.Receivers);

            for (int i = 0; i < levelConfig.Sources.Count; i++)
            {
                SignalSource source = levelConfig.Sources[i];
                if (source == null || !source.IsActive || source.SourceTile == null || source.SignalColour == SignalColour.None)
                {
                    continue;
                }

                TraceBranch(source.SourceTile, source.OutputDirection, source.SignalColour, result, new HashSet<TileDirectionKey>(), 0);
            }

            SendFeedback(result);
            UpdateCompletionState(levelConfig);
            signalBeamRenderer?.RenderPaths(result);
            return result;
        }

        private void TraceBranch(
            SignalTile startTile,
            SignalDirection direction,
            SignalColour colour,
            SignalPathResult result,
            HashSet<TileDirectionKey> visited,
            int depth)
        {
            SignalTile currentTile = startTile;
            SignalDirection currentDirection = direction;
            int steps = 0;

            while (steps < maxStepsPerBranch)
            {
                steps++;
                Vector2Int nextPosition = currentTile.GridPosition + SignalDirectionUtility.ToGridOffset(currentDirection);
                SignalTile nextTile = signalGridManager.GetTile(nextPosition);

                if (nextTile == null)
                {
                    result.AddFeedback("Signal blocked");
                    return;
                }

                TileDirectionKey key = new(nextTile, currentDirection, colour);
                if (!visited.Add(key))
                {
                    result.AddFeedback("Signal loop detected");
                    return;
                }

                result.AddSegment(currentTile.transform.position, nextTile.transform.position, colour);

                if (signalGridManager.IsBlocked(nextTile))
                {
                    result.AddFeedback("Signal blocked");
                    return;
                }

                if (signalGridManager.TryGetPulseGate(nextTile, out PulseGate pulseGate) && !pulseGate.IsOpen)
                {
                    result.AddFeedback("Signal blocked");
                    return;
                }

                if (signalGridManager.TryGetGate(nextTile, out SignalGate gate))
                {
                    if (!gate.IsOpen)
                    {
                        result.AddFeedback("Gate locked");
                        return;
                    }

                    if (!gate.CanPass(colour))
                    {
                        result.AddFeedback("Wrong colour");
                        return;
                    }

                    result.AddFeedback("Gate opened");
                }

                if (signalGridManager.TryGetReceiver(nextTile, out SignalReceiver receiver))
                {
                    if (receiver.RequiredColour == colour)
                    {
                        receiver.SetPowered(true);
                        result.AddFeedback("Receiver powered");
                    }
                    else
                    {
                        result.AddFeedback("Wrong colour");
                    }
                }

                if (signalGridManager.TryGetSplitter(nextTile, out SignalSplitter splitter))
                {
                    IReadOnlyList<SignalDirection> outputs = splitter.GetOutputDirections();
                    for (int i = 0; i < outputs.Count; i++)
                    {
                        HashSet<TileDirectionKey> branchVisited = new(visited);
                        TraceBranch(nextTile, outputs[i], colour, result, branchVisited, depth + 1);
                    }

                    return;
                }

                if (signalGridManager.TryGetReflector(nextTile, out SignalReflector reflector))
                {
                    currentDirection = reflector.CurrentDirection;
                }

                currentTile = nextTile;
            }

            result.AddFeedback("Signal loop detected");
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

        private void SendFeedback(SignalPathResult result)
        {
            if (feedbackTarget == null || !Application.isPlaying)
            {
                return;
            }

            for (int i = 0; i < result.FeedbackMessages.Count; i++)
            {
                feedbackTarget.SendMessage("ShowFeedback", result.FeedbackMessages[i], SendMessageOptions.DontRequireReceiver);
            }
        }

        private void UpdateCompletionState(LevelConfig levelConfig)
        {
            int poweredReceivers = CountPoweredReceivers(levelConfig);
            int requiredReceivers = levelConfig != null ? levelConfig.RequiredReceiverCount : 0;
            uiManager?.UpdateReceiverCounts(poweredReceivers, requiredReceivers);

            if (!Application.isPlaying || gameManager == null || gameManager.CurrentState != GameManager.GameState.Playing)
            {
                return;
            }

            if (requiredReceivers > 0 && poweredReceivers >= requiredReceivers && HasPlayerTakenAction())
            {
                gameManager.TriggerLevelComplete();
            }
        }

        private bool HasPlayerTakenAction()
        {
            return undoManager == null || undoManager.CurrentActionCount > 0 || undoManager.CurrentPowerSpent > 0;
        }

        private static int CountPoweredReceivers(LevelConfig levelConfig)
        {
            if (levelConfig == null)
            {
                return 0;
            }

            int powered = 0;
            for (int i = 0; i < levelConfig.Receivers.Count; i++)
            {
                if (levelConfig.Receivers[i] != null && levelConfig.Receivers[i].IsPowered)
                {
                    powered++;
                }
            }

            return powered;
        }
    }
}
