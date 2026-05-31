using System.Collections;
using UnityEngine;

namespace SignalTowerLogic.Gameplay
{
    /// <summary>
    /// Rotatable reflector that redirects signals from an existing tile.
    /// </summary>
    public class SignalReflector : InteractableObject
    {
        [Header("Reflector")]
        [SerializeField] private SignalDirection currentDirection;
        [SerializeField] private SignalTile reflectorTile;
        [SerializeField] private int rotationCost = 1;
        [SerializeField] private GameObject selectedVisual;

        [Header("Rotation Animation")]
        [SerializeField] private Transform visualRoot;
        [SerializeField] private float rotationDuration = 0.18f;

        private SignalDirection startDirection;
        private Coroutine rotationRoutine;

        public SignalDirection CurrentDirection => currentDirection;
        public SignalTile ReflectorTile => reflectorTile;
        public int RotationCost => rotationCost;
        public GameObject SelectedVisual => selectedVisual;

        private void Awake()
        {
            startDirection = currentDirection;

            if (selectedVisual != null)
            {
                selectedVisual.SetActive(false);
            }
        }

        public override void OnSelected()
        {
            base.OnSelected();

            if (selectedVisual != null)
            {
                selectedVisual.SetActive(true);
            }
        }

        public override void OnDeselected()
        {
            if (selectedVisual != null)
            {
                selectedVisual.SetActive(false);
            }
        }

        public override void OnRotate()
        {
            base.OnRotate();
            RotateReflector();
        }

        public void RotateReflector()
        {
            currentDirection = SignalDirectionUtility.RotateClockwise(currentDirection);
            SmoothRotateTo(currentDirection);
        }

        public void SetDirection(SignalDirection direction, bool animate = true)
        {
            currentDirection = direction;

            if (animate)
            {
                SmoothRotateTo(currentDirection);
                return;
            }

            Transform target = visualRoot != null ? visualRoot : transform;
            target.localRotation = Quaternion.Euler(SignalDirectionUtility.ToEulerAngles(currentDirection));
        }

        public void ResetReflector()
        {
            if (!Application.isPlaying)
            {
                SetDirection(currentDirection, false);
                return;
            }

            currentDirection = startDirection;
            SmoothRotateTo(currentDirection);
        }

        private void SmoothRotateTo(SignalDirection direction)
        {
            Transform target = visualRoot != null ? visualRoot : transform;
            Quaternion targetRotation = Quaternion.Euler(SignalDirectionUtility.ToEulerAngles(direction));

            if (!Application.isPlaying || !isActiveAndEnabled)
            {
                target.localRotation = targetRotation;
                return;
            }

            if (rotationRoutine != null)
            {
                StopCoroutine(rotationRoutine);
            }

            rotationRoutine = StartCoroutine(SmoothRotation(target, targetRotation));
        }

        private IEnumerator SmoothRotation(Transform target, Quaternion targetRotation)
        {
            Quaternion startRotation = target.localRotation;
            float elapsed = 0f;
            float duration = Mathf.Max(0.01f, rotationDuration);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                target.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
                yield return null;
            }

            target.localRotation = targetRotation;
            rotationRoutine = null;
        }
    }
}
