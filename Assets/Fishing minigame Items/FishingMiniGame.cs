using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingMinigame : MonoBehaviour
{
    public Image barFill;
    public TextMeshProUGUI keyPromptText;
    public TextMeshProUGUI percentText;
    public PlayerMove playerMove;
    public GameObject minigameRoot;

    [Range(0f, 1f)] public float startFill = 0.5f;
    public float drainPerSecond = 0.2f;
    public float gainOnCorrect = 0.15f;
    public float lossOnWrong = 0.1f;

    float currentFill;
    bool active;
    bool waitingForReplay;
    KeyCode currentKey;
    readonly KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public void StartMinigame()
    {
        if (playerMove != null) playerMove.enabled = false;
        waitingForReplay = false;
        currentFill = startFill;
        active = true;
        NewKey();
        UpdateUI();
    }

    void Update()
    {
        if (!active && waitingForReplay)
        {
            if (Input.GetKeyDown(KeyCode.Y)) RestartGame();
            if (Input.GetKeyDown(KeyCode.N)) ExitGame();
            return;
        }

        if (!active) return;

        currentFill -= drainPerSecond * Time.deltaTime;
        currentFill = Mathf.Clamp01(currentFill);

        if (Input.GetKeyDown(KeyCode.W)) Check(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.A)) Check(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.S)) Check(KeyCode.S);
        if (Input.GetKeyDown(KeyCode.D)) Check(KeyCode.D);

        if (currentFill <= 0f) Lose();
        else if (currentFill >= 1f) Win();

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
        if (keyPromptText != null) keyPromptText.text = "Press: " + currentKey;
    }

    void Win()
    {
        active = false;
        if (keyPromptText != null) keyPromptText.text = "You Win!";
        if (playerMove != null) playerMove.enabled = true;
    }

    void Lose()
    {
        active = false;
        waitingForReplay = true;
        if (keyPromptText != null) keyPromptText.text = "You lost! Play again? (Y/N)";
    }

    void RestartGame()
    {
        waitingForReplay = false;
        StartMinigame();
    }

    public void ExitGame()
    {
        waitingForReplay = false;
        if (playerMove != null) playerMove.enabled = true;
        if (minigameRoot != null) minigameRoot.SetActive(false);
    }

    void UpdateUI()
    {
        if (barFill != null) barFill.fillAmount = currentFill;
        if (percentText != null) percentText.text = Mathf.RoundToInt(currentFill * 100f) + "%";
    }
}
