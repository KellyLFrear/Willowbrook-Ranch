using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingMinigame : MonoBehaviour
{
    public Image barFill;
    public TextMeshProUGUI keyPromptText;
    public TextMeshProUGUI percentText;
    public PlayerMove playerMove;  

    [Range(0f, 1f)] public float startFill = 0.5f;
    public float drainPerSecond = 0.2f;
    public float gainOnCorrect = 0.15f;
    public float lossOnWrong = 0.1f;

    float currentFill;
    bool active;
    KeyCode currentKey;
    readonly KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public void StartMinigame()
    {
        if (playerMove != null) playerMove.enabled = false;
        currentFill = startFill;
        active = true;
        NewKey();
        UpdateUI();
    }

    void Update()
    {
        if (!active) return;

        currentFill -= drainPerSecond * Time.deltaTime;
        currentFill = Mathf.Clamp01(currentFill);

        if (Input.GetKeyDown(KeyCode.W)) Check(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.A)) Check(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.S)) Check(KeyCode.S);
        if (Input.GetKeyDown(KeyCode.D)) Check(KeyCode.D);

        if (currentFill <= 0f)
        {
            active = false;
            keyPromptText.text = "You Lost!";
            if (playerMove != null) playerMove.enabled = true;
        }
        else if (currentFill >= 1f)
        {
            active = false;
            keyPromptText.text = "You Win!";
            if (playerMove != null) playerMove.enabled = true;
        }

        UpdateUI();
    }

    void Check(KeyCode k)
    {
        if (k == currentKey) currentFill += gainOnCorrect;
        else currentFill -= lossOnWrong;
        currentFill = Mathf.Clamp01(currentFill);
        if (active) NewKey();
    }

    void NewKey()
    {
        currentKey = keys[Random.Range(0, keys.Length)];
        keyPromptText.text = "Press: " + currentKey;
    }

    void UpdateUI()
    {
        barFill.fillAmount = currentFill;
        percentText.text = Mathf.RoundToInt(currentFill * 100f) + "%";
    }
}
