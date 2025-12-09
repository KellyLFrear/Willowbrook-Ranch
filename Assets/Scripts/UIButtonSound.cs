using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("UI Sound Clips")]
    public AudioClip hoverClip;
    public AudioClip clickClip;

    [Header("Volume")]
    [Range(0f, 1f)] public float hoverVolume = 0.6f;
    [Range(0f, 1f)] public float clickVolume = 1f;

    private static AudioSource uiAudioSource;

    void Awake()
    {
        // Find the shared UI audio source once
        if (uiAudioSource == null)
        {
            GameObject audioObj = GameObject.Find("UIAudio");
            if (audioObj != null)
            {
                uiAudioSource = audioObj.GetComponent<AudioSource>();
            }
        }

        if (uiAudioSource == null)
        {
            Debug.LogWarning("UIButtonSound: No UIAudio AudioSource found in the scene.");
        }
    }

    //  When mouse/controller HOVERS over button
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayUI(hoverClip, hoverVolume);
    }

    //  When button is CLICKED
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayUI(clickClip, clickVolume);
    }

    private void PlayUI(AudioClip clip, float volume)
    {
        if (uiAudioSource != null && clip != null)
        {
            uiAudioSource.PlayOneShot(clip, volume);
        }
    }
}
