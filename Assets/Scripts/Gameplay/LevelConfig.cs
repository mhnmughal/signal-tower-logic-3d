using System.Collections.Generic;
using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Inspector-authored configuration for one manually assembled level.
    /// </summary>
    public class LevelConfig : MonoBehaviour
    {
        [Header("Level Info")]
        [SerializeField] private int levelNumber = 1;
        [SerializeField] private string levelName;
        [TextArea]
        [SerializeField] private string objectiveText;
        [SerializeField] private int powerBudget;
        [SerializeField] private int actionLimit;
        [SerializeField] private int requiredReceiverCount;

        [Header("Star Ratings")]
        [SerializeField] private int star3ActionLimit;
        [SerializeField] private int star2ActionLimit;
        [SerializeField] private int star1ActionLimit;

        [Header("Hints and Tutorial")]
        [TextArea]
        [SerializeField] private string hintText;
        [TextArea]
        [SerializeField] private List<string> tutorialMessages = new();

        [Header("Gameplay Objects")]
        [SerializeField] private List<SignalTile> signalTiles = new();
        [SerializeField] private List<SignalSource> sources = new();
        [SerializeField] private List<SignalReceiver> receivers = new();
        [SerializeField] private List<SignalReflector> reflectors = new();
        [SerializeField] private List<SignalGate> gates = new();
        [SerializeField] private List<SignalSplitter> splitters = new();
        [SerializeField] private List<PowerNode> powerNodes = new();
        [SerializeField] private List<PulseGate> pulseGates = new();
        [SerializeField] private List<BlockerTile> blockers = new();

        [Header("Scene Visuals")]
        [SerializeField] private List<Renderer> signalBeamRenderers = new();
        [SerializeField] private List<GameObject> levelEffects = new();

        public int LevelNumber => levelNumber;
        public string LevelName => levelName;
        public string ObjectiveText => objectiveText;
        public int PowerBudget => powerBudget;
        public int ActionLimit => actionLimit;
        public int RequiredReceiverCount => requiredReceiverCount;
        public int Star3ActionLimit => star3ActionLimit;
        public int Star2ActionLimit => star2ActionLimit;
        public int Star1ActionLimit => star1ActionLimit;
        public string HintText => hintText;
        public IReadOnlyList<string> TutorialMessages => tutorialMessages;
        public IReadOnlyList<SignalTile> SignalTiles => signalTiles;
        public IReadOnlyList<SignalSource> Sources => sources;
        public IReadOnlyList<SignalReceiver> Receivers => receivers;
        public IReadOnlyList<SignalReflector> Reflectors => reflectors;
        public IReadOnlyList<SignalGate> Gates => gates;
        public IReadOnlyList<SignalSplitter> Splitters => splitters;
        public IReadOnlyList<PowerNode> PowerNodes => powerNodes;
        public IReadOnlyList<PulseGate> PulseGates => pulseGates;
        public IReadOnlyList<BlockerTile> Blockers => blockers;
        public IReadOnlyList<Renderer> SignalBeamRenderers => signalBeamRenderers;
        public IReadOnlyList<GameObject> LevelEffects => levelEffects;
    }
}
