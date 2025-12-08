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

        foreach (char c in line)
        {
            textComponent.text += c;
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

            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            // END OF DIALOGUE
            onDialogueEnd?.Invoke();
        }
    }
}
