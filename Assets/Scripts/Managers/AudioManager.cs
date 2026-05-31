using UnityEngine;

namespace SignalTowerLogic.Managers
{
    /// <summary>
    /// Plays assigned music and SFX clips through existing manually placed AudioSource objects.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Existing Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource uiSfxSource;

        [Header("Manager References")]
        [SerializeField] private SaveManager saveManager;

        [Header("Music Clips")]
        [Tooltip("Assign a CC0 or clearly free-for-commercial-use looping music clip before publishing.")]
        [SerializeField] private AudioClip backgroundMusic;

        [Header("UI Clips")]
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip uiButtonClickClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip hintOpenedClip;

        [Header("Gameplay Clips")]
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip reflectorSelectClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip reflectorRotateClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip signalUpdatePulseClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip receiverActivatedClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip wrongColourBlockedClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip gateOpenClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip gateBlockedClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip powerNodeActivatedClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip notEnoughPowerClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip undoActionClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip levelCompleteClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip gameOverClip;
        [Tooltip("Placeholder slot. Assign CC0/free-commercial audio before publishing.")]
        [SerializeField] private AudioClip starRewardClip;

        [Header("Runtime Volume")]
        [Range(0f, 1f)]
        [SerializeField] private float musicVolume = 1f;
        [Range(0f, 1f)]
        [SerializeField] private float sfxVolume = 1f;

        public float MusicVolume => musicVolume;
        public float SFXVolume => sfxVolume;

        private void Awake()
        {
            LoadSavedVolumes();
            ApplyVolumes();
            PrepareMusicSource();
        }

        private void Start()
        {
            PlayMusic();
        }

        public void SetMusicVolume(float value)
        {
            musicVolume = Mathf.Clamp01(value);
            ApplyMusicVolume();
            saveManager?.SaveMusicVolume(musicVolume);
        }

        public void SetSFXVolume(float value)
        {
            sfxVolume = Mathf.Clamp01(value);
            ApplySFXVolume();
            saveManager?.SaveSFXVolume(sfxVolume);
        }

        public void PlayMusic()
        {
            if (musicSource == null || backgroundMusic == null)
            {
                return;
            }

            if (musicSource.clip != backgroundMusic)
            {
                musicSource.clip = backgroundMusic;
            }

            musicSource.loop = true;

            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }

        public void StopMusic()
        {
            musicSource?.Stop();
        }

        public void PlayUIButtonClick()
        {
            PlayUISFX(uiButtonClickClip);
        }

        public void PlayReflectorSelect()
        {
            PlaySFX(reflectorSelectClip);
        }

        public void PlayReflectorRotate()
        {
            PlaySFX(reflectorRotateClip);
        }

        public void PlaySignalUpdatePulse()
        {
            PlaySFX(signalUpdatePulseClip);
        }

        public void PlayReceiverActivated()
        {
            PlaySFX(receiverActivatedClip);
        }

        public void PlayWrongColourBlocked()
        {
            PlaySFX(wrongColourBlockedClip);
        }

        public void PlayGateOpen()
        {
            PlaySFX(gateOpenClip);
        }

        public void PlayGateBlocked()
        {
            PlaySFX(gateBlockedClip);
        }

        public void PlayPowerNodeActivated()
        {
            PlaySFX(powerNodeActivatedClip);
        }

        public void PlayNotEnoughPower()
        {
            PlaySFX(notEnoughPowerClip);
        }

        public void PlayUndoAction()
        {
            PlaySFX(undoActionClip);
        }

        public void PlayHintOpened()
        {
            PlayUISFX(hintOpenedClip);
        }

        public void PlayLevelComplete()
        {
            PlaySFX(levelCompleteClip);
        }

        public void PlayGameOver()
        {
            PlaySFX(gameOverClip);
        }

        public void PlayStarReward()
        {
            PlaySFX(starRewardClip);
        }

        public void PlayFeedbackCue(string message)
        {
            switch (message)
            {
                case "Reflector selected":
                    PlayReflectorSelect();
                    break;
                case "Reflector rotated":
                    PlayReflectorRotate();
                    break;
                case "Receiver powered":
                    PlayReceiverActivated();
                    break;
                case "Wrong colour":
                    PlayWrongColourBlocked();
                    break;
                case "Signal blocked":
                case "Gate locked":
                    PlayGateBlocked();
                    break;
                case "Gate opened":
                    PlayGateOpen();
                    break;
                case "Power node active":
                    PlayPowerNodeActivated();
                    break;
                case "Not enough power":
                    PlayNotEnoughPower();
                    break;
                case "Undo complete":
                    PlayUndoAction();
                    break;
                case "Hint used":
                    PlayHintOpened();
                    break;
                case "Level complete":
                    PlayLevelComplete();
                    PlayStarReward();
                    break;
                default:
                    break;
            }
        }

        private void LoadSavedVolumes()
        {
            if (saveManager == null)
            {
                return;
            }

            musicVolume = saveManager.GetMusicVolume();
            sfxVolume = saveManager.GetSFXVolume();
        }

        private void PrepareMusicSource()
        {
            if (musicSource == null)
            {
                return;
            }

            musicSource.playOnAwake = false;
            musicSource.loop = true;

            if (backgroundMusic != null)
            {
                musicSource.clip = backgroundMusic;
            }
        }

        private void ApplyVolumes()
        {
            ApplyMusicVolume();
            ApplySFXVolume();
        }

        private void ApplyMusicVolume()
        {
            if (musicSource != null)
            {
                musicSource.volume = musicVolume;
            }
        }

        private void ApplySFXVolume()
        {
            if (sfxSource != null)
            {
                sfxSource.volume = sfxVolume;
            }

            if (uiSfxSource != null)
            {
                uiSfxSource.volume = sfxVolume;
            }
        }

        private void PlaySFX(AudioClip clip)
        {
            if (sfxSource != null && clip != null)
            {
                sfxSource.PlayOneShot(clip, sfxVolume);
            }
        }

        private void PlayUISFX(AudioClip clip)
        {
            if (uiSfxSource != null && clip != null)
            {
                uiSfxSource.PlayOneShot(clip, sfxVolume);
            }
        }
    }
}
