using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainThemePlayer : MonoBehaviour
{
    private static MainThemePlayer instance;
    private AudioSource audioSource;

    [Tooltip("Drag your main-theme AudioClip here")]
    [SerializeField] private AudioClip themeClip;

    private const string MusicPrefKey = "MusicEnabled";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = themeClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        LoadMusicSetting();
    }

    /// <summary>
    /// Enables or disables the background music and saves the setting.
    /// </summary>
    /// <param name="enabled">True to play music, false to stop.</param>
    public void SetMusicEnabled(bool enabled)
    {
        if (audioSource == null) return;

        if (enabled)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause(); // or Stop() if you want to reset it
        }

        PlayerPrefs.SetInt(MusicPrefKey, enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadMusicSetting()
    {
        bool musicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1; // Default is ON

        if (musicEnabled)
        {
            audioSource.Play();
        }
    }
}
