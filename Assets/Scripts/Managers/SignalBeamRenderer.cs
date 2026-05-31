using System.Collections.Generic;
using SignalTowerLogic.Gameplay;
using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Updates existing assigned LineRenderer beams from calculated signal paths.
    /// </summary>
    public class SignalBeamRenderer : MonoBehaviour
    {
        [Header("Existing Beam Renderers")]
        [SerializeField] private List<LineRenderer> beamRenderers = new();

        [Header("Colours")]
        [SerializeField] private Color red = new(1f, 0.08f, 0.06f);
        [SerializeField] private Color blue = new(0.08f, 0.4f, 1f);
        [SerializeField] private Color green = new(0.08f, 1f, 0.35f);
        [SerializeField] private Color yellow = new(1f, 0.85f, 0.08f);

        [Header("Pulse")]
        [SerializeField] private bool pulseEnabled = true;
        [SerializeField] private float baseWidth = 0.14f;
        [SerializeField] private float pulseWidth = 0.04f;
        [SerializeField] private float pulseSpeed = 4f;
        [SerializeField] private float beamHeightOffset = 0.22f;

        private int activeBeamCount;

        private void Awake()
        {
            DisableAllBeams();
        }

        private void Update()
        {
            if (!pulseEnabled)
            {
                return;
            }

            float width = baseWidth + Mathf.Sin(Time.time * pulseSpeed) * pulseWidth;
            for (int i = 0; i < activeBeamCount && i < beamRenderers.Count; i++)
            {
                LineRenderer lineRenderer = beamRenderers[i];
                if (lineRenderer != null && lineRenderer.enabled)
                {
                    lineRenderer.widthMultiplier = Mathf.Max(0.01f, width);
                }
            }
        }

        public void RenderPaths(SignalPathResult result)
        {
            DisableAllBeams();

            if (result == null)
            {
                return;
            }

            int count = Mathf.Min(result.Segments.Count, beamRenderers.Count);
            for (int i = 0; i < count; i++)
            {
                LineRenderer lineRenderer = beamRenderers[i];
                if (lineRenderer == null)
                {
                    continue;
                }

                SignalPathSegment segment = result.Segments[i];
                Color colour = GetColour(segment.Colour);
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                lineRenderer.useWorldSpace = true;
                lineRenderer.alignment = LineAlignment.View;
                lineRenderer.numCapVertices = 4;
                lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lineRenderer.receiveShadows = false;
                lineRenderer.widthMultiplier = baseWidth;
                lineRenderer.startColor = colour;
                lineRenderer.endColor = colour;
                lineRenderer.SetPosition(0, GetVisibleBeamPosition(segment.StartPosition));
                lineRenderer.SetPosition(1, GetVisibleBeamPosition(segment.EndPosition));
            }

            activeBeamCount = count;
        }

        public void UseLevelBeamRenderers(LevelConfig levelConfig)
        {
            DisableAllBeams();
            beamRenderers.Clear();

            if (levelConfig == null)
            {
                return;
            }

            for (int i = 0; i < levelConfig.SignalBeamRenderers.Count; i++)
            {
                if (levelConfig.SignalBeamRenderers[i] is LineRenderer lineRenderer)
                {
                    beamRenderers.Add(lineRenderer);
                }
            }
        }

        public void DisableAllBeams()
        {
            activeBeamCount = 0;

            for (int i = 0; i < beamRenderers.Count; i++)
            {
                if (beamRenderers[i] != null)
                {
                    beamRenderers[i].enabled = false;
                }
            }
        }

        private Color GetColour(SignalColour signalColour)
        {
            return signalColour switch
            {
                SignalColour.Red => red,
                SignalColour.Blue => blue,
                SignalColour.Green => green,
                SignalColour.Yellow => yellow,
                _ => Color.white
            };
        }

        private Vector3 GetVisibleBeamPosition(Vector3 tilePosition)
        {
            return tilePosition + Vector3.up * beamHeightOffset;
        }
    }
}
