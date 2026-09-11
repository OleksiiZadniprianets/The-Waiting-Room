using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Default Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMusic(menuMusic);
            return;
        }

        PlayMusic(gameMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource == null || clip == null)
        {
            return;
        }

        if (audioSource.isPlaying &&
            audioSource.clip == clip)
        {
            return;
        }

        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.loop = true;

        audioSource.Play();
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}