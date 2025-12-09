using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textComponent;

    [Header("Settings")]
    public float textSpeed = 0.03f;

    private string[] lines;
    private int index = 0;
    private Coroutine typingCoroutine;

    public Action onDialogueEnd;

    // ------------------ NEW: DIALOGUE AUDIO ------------------
    [Header("Audio")]
    public AudioClip typeClip;          // sound for each character (or every few chars)
    public AudioClip advanceClip;       // sound when moving to next line
    public AudioClip endClip;           // sound when dialogue ends

    [Header("Audio Volume")]
    [Range(0f, 1f)] public float typeVolume = 0.5f;
    [Range(0f, 1f)] public float advanceVolume = 0.8f;
    [Range(0f, 1f)] public float endVolume = 0.8f;

    [Header("Typing Audio Settings")]
    public int charsPerTypeSound = 2;   // play a sound every N characters to avoid spam

    private AudioSource audioSource;
    // ---------------------------------------------------------

    void Awake()
    {
        if (textComponent == null)
        {
            Debug.LogError("[Dialogue] TextMeshProUGUI not assigned.");
        }

        textComponent.text = string.Empty;

        // Optional: make sure wrapping/overflow are safe
        if (textComponent != null)
        {
            textComponent.enableWordWrapping = true;
            textComponent.overflowMode = TextOverflowModes.Overflow;
        }

        // NEW: grab AudioSource on this GameObject (if present)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("[Dialogue] No AudioSource found. Dialogue sounds will not play.");
        }
    }

    void Update()
    {
        if (lines == null || lines.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If currently typing → skip to full line
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                textComponent.text = lines[index];
            }
            else
            {
                // Full line already shown → go to next
                NextLine();
            }
        }
    }

    public void PlayLines(string[] newLines)
    {
        if (newLines == null || newLines.Length == 0)
        {
            Debug.LogWarning("[Dialogue] Tried to start empty dialogue.");
            return;
        }

        lines = newLines;
        index = 0;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = string.Empty;

        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        string line = lines[index];

        int charCount = 0;

        foreach (char c in line)
        {
            textComponent.text += c;
            charCount++;

            // NEW: play a small sound every 'charsPerTypeSound' characters
            if (charsPerTypeSound > 0 && (charCount % charsPerTypeSound == 0))
            {
                PlaySFX(typeClip, typeVolume);
            }

            yield return new WaitForSeconds(textSpeed);
        }

        typingCoroutine = null;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            // NEW: play advance sound when moving to next line
            PlaySFX(advanceClip, advanceVolume);

            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            // END OF DIALOGUE
            // NEW: play end sound when dialogue finishes
            PlaySFX(endClip, endVolume);

            onDialogueEnd?.Invoke();
        }
    }

    // ------------------ NEW: helper for audio ------------------
    private void PlaySFX(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
