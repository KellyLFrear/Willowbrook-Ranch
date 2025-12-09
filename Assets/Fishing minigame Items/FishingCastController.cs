using System.Collections;
using UnityEngine;
using TMPro;

public class FishingCastController : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public FishingMinigame minigame;
    public GameObject minigameRoot;
    public KeyCode biteKey = KeyCode.Space;
    public float minBiteTime = 3f;
    public float maxBiteTime = 10f;

    bool isCasting;
    Coroutine castRoutine;

    // ------------------ ADDED: FISHING SOUNDS ------------------

    [Header("Fishing SFX")]
    public AudioClip castClip;        // Splash / rod cast sound
    public AudioClip biteClip;        // Fish bite alert sound
    public AudioClip minigameClip;    // Reel / minigame start sound

    [Header("Fishing SFX Volume")]
    [Range(0f, 1f)] public float castVolume = 0.8f;
    [Range(0f, 1f)] public float biteVolume = 1.0f;
    [Range(0f, 1f)] public float minigameVolume = 0.8f;

    private AudioSource audioSource;

    // ----------------------------------------------------------

    void Start()
    {
        // Grab AudioSource on this same GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("FishingCastController: No AudioSource found! Sounds will not play.");
        }
    }

    // Small helper to safely play sounds
    private void PlaySFX(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public void StartCasting()
    {
        if (!isCasting)
        {
            if (castRoutine != null)
                StopCoroutine(castRoutine);

            //  Play CAST sound immediately when casting starts
            PlaySFX(castClip, castVolume);

            castRoutine = StartCoroutine(CastFlow());
        }
    }

    IEnumerator CastFlow()
    {
        isCasting = true;

        if (promptText != null)
            promptText.text = "Casting...";

        float wait = Random.Range(minBiteTime, maxBiteTime);
        yield return new WaitForSeconds(wait);

        if (promptText != null)
            promptText.text = "PRESS SPACE!";

        //  Play BITE alert sound when fish is ready
        PlaySFX(biteClip, biteVolume);

        bool pressed = false;
        while (!pressed)
        {
            if (Input.GetKeyDown(biteKey))
                pressed = true;
            yield return null;
        }

        if (promptText != null)
            promptText.text = "";

        if (minigameRoot != null)
            minigameRoot.SetActive(true);

        if (minigame != null)
            minigame.StartMinigame();

        //  Play MINIGAME START sound
        PlaySFX(minigameClip, minigameVolume);

        isCasting = false;
    }
}
