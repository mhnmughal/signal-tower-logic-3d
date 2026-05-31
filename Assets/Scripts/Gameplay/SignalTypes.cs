using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Shared signal colours used by manually placed gameplay objects.
    /// </summary>
    public enum SignalColour
    {
        None,
        Red,
        Blue,
        Green,
        Yellow
    }

    /// <summary>
    /// Cardinal board directions for sources, reflectors, gates, and splitters.
    /// </summary>
    public enum SignalDirection
    {
        North,
        East,
        South,
        West
    }

    public static class SignalDirectionUtility
    {
        public static SignalDirection RotateClockwise(SignalDirection direction)
        {
            return direction switch
            {
                SignalDirection.North => SignalDirection.East,
                SignalDirection.East => SignalDirection.South,
                SignalDirection.South => SignalDirection.West,
                _ => SignalDirection.North
            };
        }

        public static Vector3 ToEulerAngles(SignalDirection direction)
        {
            return direction switch
            {
                SignalDirection.North => Vector3.zero,
                SignalDirection.East => new Vector3(0f, 90f, 0f),
                SignalDirection.South => new Vector3(0f, 180f, 0f),
                _ => new Vector3(0f, 270f, 0f)
            };
        }

        public static Vector2Int ToGridOffset(SignalDirection direction)
        {
            return direction switch
            {
                SignalDirection.North => new Vector2Int(0, 1),
                SignalDirection.East => new Vector2Int(1, 0),
                SignalDirection.South => new Vector2Int(0, -1),
                _ => new Vector2Int(-1, 0)
            };
        }
    }
}
