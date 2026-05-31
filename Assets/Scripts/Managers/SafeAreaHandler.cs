using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Applies Screen.safeArea padding to an assigned RectTransform.
    /// </summary>
    public class SafeAreaHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform target;

        private Rect lastSafeArea;
        private Vector2Int lastScreenSize;

        private void OnEnable()
        {
            ApplySafeArea();
        }

        private void Update()
        {
            Vector2Int screenSize = new(Screen.width, Screen.height);
            if (lastSafeArea != Screen.safeArea || lastScreenSize != screenSize)
            {
                ApplySafeArea();
            }
        }

        public void ApplySafeArea()
        {
            if (target == null)
            {
                target = transform as RectTransform;
            }

            if (target == null)
            {
                return;
            }

            Rect safeArea = Screen.safeArea;
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            target.anchorMin = anchorMin;
            target.anchorMax = anchorMax;
            target.offsetMin = Vector2.zero;
            target.offsetMax = Vector2.zero;

            lastSafeArea = safeArea;
            lastScreenSize = new Vector2Int(Screen.width, Screen.height);
        }
    }
}
