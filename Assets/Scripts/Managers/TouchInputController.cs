using SignalTowerLogic.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Handles mobile-first tap selection and forwards button actions to existing managers.
    /// </summary>
    public class TouchInputController : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private Camera gameplayCamera;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private GameObject selectionRing;
        [SerializeField] private GameObject validTargetHighlight;
        [SerializeField] private GameObject invalidActionMarker;

        [Header("Manager References")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UndoManager undoManager;
        [SerializeField] private SignalPathCalculator signalPathCalculator;

        [Tooltip("Optional existing feedback object. This controller sends ShowFeedback(string) if present.")]
        [SerializeField] private GameObject feedbackTarget;

        [Header("Raycast")]
        [SerializeField] private LayerMask interactableMask = ~0;
        [SerializeField] private float maxRaycastDistance = 120f;
        [SerializeField] private Vector3 selectionRingOffset = new(0f, 0.04f, 0f);

        private InteractableObject selectedObject;

        private void Awake()
        {
            HideSelectionVisuals();
        }

        private void Update()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            HandleTouchInput();
#elif ENABLE_INPUT_SYSTEM
            HandleInputSystemPointerInput();
#endif

#if UNITY_EDITOR
            HandleEditorInput();
#endif
        }

        public void RotateSelectedButton()
        {
            if (selectedObject is not SignalReflector reflector)
            {
                ShowInvalidAction();
                return;
            }

            if (undoManager != null && undoManager.TryRotateReflector(reflector))
            {
                PositionSelectionVisuals(reflector.transform.position);
            }
        }

        public void ActivateSelectedButton()
        {
            if (selectedObject is PowerNode powerNode)
            {
                if (undoManager != null)
                {
                    undoManager.TryActivatePowerNode(powerNode);
                }

                return;
            }

            if (selectedObject is SignalGate or PulseGate)
            {
                selectedObject.OnActivate();
                signalPathCalculator?.RecalculateSignalPaths();
                if (selectedObject is SignalGate gate)
                {
                    ShowFeedback(gate.IsOpen ? "Gate opened" : "Gate locked");
                }
                else if (selectedObject is PulseGate pulseGate)
                {
                    ShowFeedback(pulseGate.IsOpen ? "Gate opened" : "Gate locked");
                }

                return;
            }

            ShowInvalidAction();
        }

        public void UndoButton()
        {
            undoManager?.UndoLastAction();
        }

        public void RestartButton()
        {
            DeselectCurrent();
            gameManager?.RestartCurrentLevel();
        }

        public void PauseButton()
        {
            gameManager?.PauseGame();
        }

        private void HandleTouchInput()
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase != UnityEngine.TouchPhase.Ended || IsPointerOverUI(touch.fingerId))
                {
                    continue;
                }

                TrySelectAtScreenPosition(touch.position);
                return;
            }
        }

#if ENABLE_INPUT_SYSTEM
        private void HandleInputSystemPointerInput()
        {
            Touchscreen touchscreen = Touchscreen.current;
            if (touchscreen != null)
            {
                for (int i = 0; i < touchscreen.touches.Count; i++)
                {
                    UnityEngine.InputSystem.Controls.TouchControl touch = touchscreen.touches[i];
                    if (!touch.press.wasReleasedThisFrame)
                    {
                        continue;
                    }

                    if (IsPointerOverUI(touch.touchId.ReadValue()))
                    {
                        continue;
                    }

                    TrySelectAtScreenPosition(touch.position.ReadValue());
                    return;
                }
            }

            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.wasReleasedThisFrame && !IsPointerOverUI(-1))
            {
                TrySelectAtScreenPosition(mouse.position.ReadValue());
            }
        }
#endif

#if UNITY_EDITOR
        private void HandleEditorInput()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUI(-1))
            {
                TrySelectAtScreenPosition(Input.mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateSelectedButton();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ActivateSelectedButton();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                UndoButton();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseButton();
            }
#elif ENABLE_INPUT_SYSTEM
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.rKey.wasPressedThisFrame)
            {
                RotateSelectedButton();
            }

            if (keyboard.aKey.wasPressedThisFrame)
            {
                ActivateSelectedButton();
            }

            if (keyboard.uKey.wasPressedThisFrame)
            {
                UndoButton();
            }

            if (keyboard.escapeKey.wasPressedThisFrame)
            {
                PauseButton();
            }
#endif
        }
#endif

        private bool IsPointerOverUI(int pointerId)
        {
            return eventSystem != null && eventSystem.IsPointerOverGameObject(pointerId);
        }

        private void TrySelectAtScreenPosition(Vector2 screenPosition)
        {
            if (!IsGameplayInputAllowed() || gameplayCamera == null)
            {
                ShowInvalidAction();
                return;
            }

            Ray ray = gameplayCamera.ScreenPointToRay(screenPosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance, interactableMask, QueryTriggerInteraction.Ignore))
            {
                ShowInvalidAction();
                return;
            }

            InteractableObject interactable = hit.collider.GetComponentInParent<InteractableObject>();
            if (interactable == null || !interactable.CanSelect)
            {
                ShowInvalidAction(hit.point);
                return;
            }

            SelectObject(interactable);
        }

        private bool IsGameplayInputAllowed()
        {
            return gameManager == null || gameManager.CurrentState == GameManager.GameState.Playing;
        }

        private void SelectObject(InteractableObject interactable)
        {
            if (selectedObject == interactable)
            {
                PositionSelectionVisuals(interactable.transform.position);
                return;
            }

            DeselectCurrent();
            selectedObject = interactable;
            selectedObject.OnSelected();
            PositionSelectionVisuals(selectedObject.transform.position);
            string selectedName = string.IsNullOrWhiteSpace(selectedObject.ObjectName) ? "Object" : selectedObject.ObjectName;
            ShowFeedback(selectedObject is SignalReflector ? "Reflector selected" : $"{selectedName} selected");
        }

        private void DeselectCurrent()
        {
            if (selectedObject != null)
            {
                selectedObject.OnDeselected();
                selectedObject = null;
            }

            HideSelectionVisuals();
        }

        private void PositionSelectionVisuals(Vector3 worldPosition)
        {
            if (selectionRing != null)
            {
                selectionRing.transform.position = worldPosition + selectionRingOffset;
                selectionRing.SetActive(true);
            }

            if (validTargetHighlight != null)
            {
                validTargetHighlight.transform.position = worldPosition + selectionRingOffset;
                validTargetHighlight.SetActive(true);
            }

            if (invalidActionMarker != null)
            {
                invalidActionMarker.SetActive(false);
            }
        }

        private void HideSelectionVisuals()
        {
            if (selectionRing != null)
            {
                selectionRing.SetActive(false);
            }

            if (validTargetHighlight != null)
            {
                validTargetHighlight.SetActive(false);
            }

            if (invalidActionMarker != null)
            {
                invalidActionMarker.SetActive(false);
            }
        }

        private void ShowInvalidAction()
        {
            Vector3 markerPosition = selectedObject != null ? selectedObject.transform.position : transform.position;
            ShowInvalidAction(markerPosition);
        }

        private void ShowInvalidAction(Vector3 worldPosition)
        {
            if (invalidActionMarker != null)
            {
                invalidActionMarker.transform.position = worldPosition + selectionRingOffset;
                invalidActionMarker.SetActive(true);
            }

            ShowFeedback("Invalid action");
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
