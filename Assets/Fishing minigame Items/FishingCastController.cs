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

    public void StartCasting()
    {
        if (!isCasting)
        {
            if (castRoutine != null)
                StopCoroutine(castRoutine);
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

        isCasting = false;
    }
}
