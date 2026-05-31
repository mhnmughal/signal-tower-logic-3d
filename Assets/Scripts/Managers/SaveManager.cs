using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Stores local offline player progress and settings using PlayerPrefs.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        public const string HighestUnlockedLevelKey = "SignalTower.HighestUnlockedLevel";
        public const string MusicVolumeKey = "SignalTower.MusicVolume";
        public const string SfxVolumeKey = "SignalTower.SFXVolume";
        public const string VibrationKey = "SignalTower.Vibration";
        public const string TutorialSeenKey = "SignalTower.TutorialSeen";

        private const int DefaultHighestUnlockedLevel = 1;
        private const float DefaultVolume = 1f;

        public void SaveStars(int level, int stars)
        {
            int clampedStars = Mathf.Clamp(stars, 0, 3);
            int previousBest = GetStars(level);

            if (clampedStars > previousBest)
            {
                PlayerPrefs.SetInt(GetStarsKey(level), clampedStars);
                PlayerPrefs.Save();
            }
        }

        public int GetStars(int level)
        {
            return PlayerPrefs.GetInt(GetStarsKey(level), 0);
        }

        public void UnlockLevel(int level)
        {
            int highest = Mathf.Max(GetHighestUnlockedLevel(), level, DefaultHighestUnlockedLevel);
            PlayerPrefs.SetInt(HighestUnlockedLevelKey, highest);
            PlayerPrefs.Save();
        }

        public int GetHighestUnlockedLevel()
        {
            return PlayerPrefs.GetInt(HighestUnlockedLevelKey, DefaultHighestUnlockedLevel);
        }

        public void SaveMusicVolume(float value)
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, Mathf.Clamp01(value));
            PlayerPrefs.Save();
        }

        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MusicVolumeKey, DefaultVolume);
        }

        public void SaveSFXVolume(float value)
        {
            PlayerPrefs.SetFloat(SfxVolumeKey, Mathf.Clamp01(value));
            PlayerPrefs.Save();
        }

        public float GetSFXVolume()
        {
            return PlayerPrefs.GetFloat(SfxVolumeKey, DefaultVolume);
        }

        public void SaveVibration(bool value)
        {
            PlayerPrefs.SetInt(VibrationKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public bool GetVibration()
        {
            return PlayerPrefs.GetInt(VibrationKey, 1) == 1;
        }

        public void SaveTutorialSeen(bool value)
        {
            PlayerPrefs.SetInt(TutorialSeenKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public bool GetTutorialSeen()
        {
            return PlayerPrefs.GetInt(TutorialSeenKey, 0) == 1;
        }

        public void SaveHintUsed(int level, bool value)
        {
            PlayerPrefs.SetInt(GetHintUsedKey(level), value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public bool GetHintUsed(int level)
        {
            return PlayerPrefs.GetInt(GetHintUsedKey(level), 0) == 1;
        }

        public void ResetProgress()
        {
            int maxKnownLevel = Mathf.Max(GetHighestUnlockedLevel(), 12);

            PlayerPrefs.DeleteKey(HighestUnlockedLevelKey);
            PlayerPrefs.DeleteKey(TutorialSeenKey);

            for (int level = 1; level <= maxKnownLevel; level++)
            {
                PlayerPrefs.DeleteKey(GetStarsKey(level));
                PlayerPrefs.DeleteKey(GetHintUsedKey(level));
            }

            PlayerPrefs.SetInt(HighestUnlockedLevelKey, DefaultHighestUnlockedLevel);
            PlayerPrefs.Save();
        }

        public static string GetStarsKey(int level)
        {
            return $"SignalTower.Level.{level}.Stars";
        }

        public static string GetHintUsedKey(int level)
        {
            return $"SignalTower.Level.{level}.HintUsed";
        }
    }
}
