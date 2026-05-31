using System.Collections.Generic;
using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// One rendered signal branch between two existing tile positions.
    /// </summary>
    public readonly struct SignalPathSegment
    {
        public SignalPathSegment(Vector3 startPosition, Vector3 endPosition, SignalColour colour)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Colour = colour;
        }

        public Vector3 StartPosition { get; }
        public Vector3 EndPosition { get; }
        public SignalColour Colour { get; }
    }

    /// <summary>
    /// Result produced by SignalPathCalculator for visual systems and feedback.
    /// </summary>
    public class SignalPathResult
    {
        private readonly List<SignalPathSegment> segments = new();
        private readonly List<string> feedbackMessages = new();

        public IReadOnlyList<SignalPathSegment> Segments => segments;
        public IReadOnlyList<string> FeedbackMessages => feedbackMessages;

        public void AddSegment(Vector3 startPosition, Vector3 endPosition, SignalColour colour)
        {
            segments.Add(new SignalPathSegment(startPosition, endPosition, colour));
        }

        public void AddFeedback(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                feedbackMessages.Add(message);
            }
        }
    }
}
