using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        // If another MusicManager already exists, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // This is the first / main MusicManager
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Grab the AudioSource on the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure looping is on
        if (audioSource != null)
        {
            audioSource.loop = true;

            if (!audioSource.isPlaying && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("MusicManager: No AudioSource found on this GameObject!");
        }
    }

    // Optional helpers if you want to control music later

    public void SetVolume(float volume)
    {
        if (audioSource != null)
            audioSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        if (audioSource != null)
            audioSource.Stop();
    }

    public void ChangeTrack(AudioClip newClip, bool playImmediately = true)
    {
        if (audioSource == null || newClip == null) return;

        audioSource.clip = newClip;

        if (playImmediately)
            audioSource.Play();
    }
}
