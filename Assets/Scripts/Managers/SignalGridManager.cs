using System.Collections.Generic;
using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Builds lookup tables from Inspector-assigned tiles and gameplay objects.
    /// </summary>
    public class SignalGridManager : MonoBehaviour
    {
        [Header("Manual Grid References")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private LevelConfig activeLevelConfig;
        [SerializeField] private List<SignalTile> tiles = new();

        private readonly Dictionary<Vector2Int, SignalTile> tilesByPosition = new();
        private readonly Dictionary<SignalTile, SignalReceiver> receiversByTile = new();
        private readonly Dictionary<SignalTile, SignalReflector> reflectorsByTile = new();
        private readonly Dictionary<SignalTile, SignalGate> gatesByTile = new();
        private readonly Dictionary<SignalTile, SignalSplitter> splittersByTile = new();
        private readonly Dictionary<SignalTile, PowerNode> powerNodesByTile = new();
        private readonly Dictionary<SignalTile, PulseGate> pulseGatesByTile = new();
        private readonly HashSet<SignalTile> blockerTiles = new();

        public LevelConfig ActiveLevelConfig => activeLevelConfig;
        public IReadOnlyList<SignalTile> Tiles => tiles;

        public void LoadLevel(LevelConfig levelConfig)
        {
            activeLevelConfig = levelConfig;
            RebuildLookups();
        }

        public void RebuildFromCurrentLevel()
        {
            if (levelManager != null)
            {
                activeLevelConfig = levelManager.CurrentLevelConfig;
            }

            RebuildLookups();
        }

        public SignalTile GetTile(Vector2Int gridPosition)
        {
            tilesByPosition.TryGetValue(gridPosition, out SignalTile tile);
            return tile;
        }

        public bool TryGetReceiver(SignalTile tile, out SignalReceiver receiver)
        {
            return receiversByTile.TryGetValue(tile, out receiver);
        }

        public bool TryGetReflector(SignalTile tile, out SignalReflector reflector)
        {
            return reflectorsByTile.TryGetValue(tile, out reflector);
        }

        public bool TryGetGate(SignalTile tile, out SignalGate gate)
        {
            return gatesByTile.TryGetValue(tile, out gate);
        }

        public bool TryGetSplitter(SignalTile tile, out SignalSplitter splitter)
        {
            return splittersByTile.TryGetValue(tile, out splitter);
        }

        public bool TryGetPowerNode(SignalTile tile, out PowerNode powerNode)
        {
            return powerNodesByTile.TryGetValue(tile, out powerNode);
        }

        public bool TryGetPulseGate(SignalTile tile, out PulseGate pulseGate)
        {
            return pulseGatesByTile.TryGetValue(tile, out pulseGate);
        }

        public bool IsBlocked(SignalTile tile)
        {
            return tile == null || tile.IsBlocked || blockerTiles.Contains(tile);
        }

        private void RebuildLookups()
        {
            ClearLookups();

            if (activeLevelConfig == null)
            {
                return;
            }

            LoadTiles(activeLevelConfig.SignalTiles);
            MapReceivers(activeLevelConfig.Receivers);
            MapReflectors(activeLevelConfig.Reflectors);
            MapGates(activeLevelConfig.Gates);
            MapSplitters(activeLevelConfig.Splitters);
            MapPowerNodes(activeLevelConfig.PowerNodes);
            MapPulseGates(activeLevelConfig.PulseGates);
            MapBlockers(activeLevelConfig.Blockers);
        }

        private void ClearLookups()
        {
            tilesByPosition.Clear();
            receiversByTile.Clear();
            reflectorsByTile.Clear();
            gatesByTile.Clear();
            splittersByTile.Clear();
            powerNodesByTile.Clear();
            pulseGatesByTile.Clear();
            blockerTiles.Clear();
        }

        private void LoadTiles(IReadOnlyList<SignalTile> configuredTiles)
        {
            tiles.Clear();

            for (int i = 0; i < configuredTiles.Count; i++)
            {
                SignalTile tile = configuredTiles[i];
                if (tile == null)
                {
                    continue;
                }

                tiles.Add(tile);
                tilesByPosition[tile.GridPosition] = tile;
            }
        }

        private void MapReceivers(IReadOnlyList<SignalReceiver> receivers)
        {
            for (int i = 0; i < receivers.Count; i++)
            {
                SignalReceiver receiver = receivers[i];
                if (receiver != null && receiver.ReceiverTile != null)
                {
                    receiversByTile[receiver.ReceiverTile] = receiver;
                }
            }
        }

        private void MapReflectors(IReadOnlyList<SignalReflector> reflectors)
        {
            for (int i = 0; i < reflectors.Count; i++)
            {
                SignalReflector reflector = reflectors[i];
                if (reflector != null && reflector.ReflectorTile != null)
                {
                    reflectorsByTile[reflector.ReflectorTile] = reflector;
                }
            }
        }

        private void MapGates(IReadOnlyList<SignalGate> gates)
        {
            for (int i = 0; i < gates.Count; i++)
            {
                SignalGate gate = gates[i];
                if (gate != null && gate.GateTile != null)
                {
                    gatesByTile[gate.GateTile] = gate;
                }
            }
        }

        private void MapSplitters(IReadOnlyList<SignalSplitter> splitters)
        {
            for (int i = 0; i < splitters.Count; i++)
            {
                SignalSplitter splitter = splitters[i];
                if (splitter != null && splitter.SplitterTile != null)
                {
                    splittersByTile[splitter.SplitterTile] = splitter;
                }
            }
        }

        private void MapPowerNodes(IReadOnlyList<PowerNode> powerNodes)
        {
            for (int i = 0; i < powerNodes.Count; i++)
            {
                PowerNode powerNode = powerNodes[i];
                if (powerNode != null && powerNode.NodeTile != null)
                {
                    powerNodesByTile[powerNode.NodeTile] = powerNode;
                }
            }
        }

        private void MapPulseGates(IReadOnlyList<PulseGate> pulseGates)
        {
            for (int i = 0; i < pulseGates.Count; i++)
            {
                PulseGate pulseGate = pulseGates[i];
                if (pulseGate != null && pulseGate.PulseGateTile != null)
                {
                    pulseGatesByTile[pulseGate.PulseGateTile] = pulseGate;
                }
            }
        }

        private void MapBlockers(IReadOnlyList<BlockerTile> blockers)
        {
            for (int i = 0; i < blockers.Count; i++)
            {
                BlockerTile blocker = blockers[i];
                if (blocker != null && blocker.BlockerTileReference != null)
                {
                    blockerTiles.Add(blocker.BlockerTileReference);
                }
            }
        }
    }
}
