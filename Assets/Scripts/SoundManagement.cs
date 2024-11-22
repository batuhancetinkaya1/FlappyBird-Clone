using UnityEngine;
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour 
{
    public static SoundManagement Instance { get; private set; }

    [SerializeField] private AudioSource soundFXPrefab;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider volumeSlider2;
    [SerializeField] private AudioClip musicClip;

    private const string VOLUME_PREF_KEY = "MasterVolume";
    private bool isUpdating = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudio()
    {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.enabled = true;
        AudioListener.volume = GetSavedVolume();
    }

    private void Start()
    {
        float savedVolume = GetSavedVolume();

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnFirstSliderChanged);
        }

        if (volumeSlider2 != null)
        {
            volumeSlider2.value = savedVolume;
            volumeSlider2.onValueChanged.AddListener(OnSecondSliderChanged);
        }
        StartMusic();
    }

    private void OnFirstSliderChanged(float value)
    {
        if (!isUpdating)
        {
            isUpdating = true;
            if (volumeSlider2 != null) volumeSlider2.value = value;
            SetVolume(value);
            isUpdating = false;
        }
    }

    private void OnSecondSliderChanged(float value)
    {
        if (!isUpdating)
        {
            isUpdating = true;
            if (volumeSlider != null) volumeSlider.value = value;
            SetVolume(value);
            isUpdating = false;
        }
    }

    private float GetSavedVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_PREF_KEY, 1f);
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VOLUME_PREF_KEY, value);
        PlayerPrefs.Save();
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        if (soundFXPrefab == null || audioClip == null) return;

        AudioSource audioSource = Instantiate(soundFXPrefab, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 0f; // 2D ses için
        audioSource.Play();


        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlayMusic(AudioClip musicClip, float volume = 0.15f)
    {
        if (musicClip == null) return;

        StopMusic();
        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.volume = volume;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void StopMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void StartMusic()
    {
        SoundManagement.Instance.PlayMusic(musicClip, 0.10f);
    }
}