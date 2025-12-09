using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishingMinigame : MonoBehaviour
{
    [Header("UI References")]
    public Image barFill;
    public TextMeshProUGUI keyPromptText;
    public TextMeshProUGUI percentText;

    [Header("Movement / Player References")]
    public PlayerMove playerMove;
    public Player player;
    public GameObject minigameRoot;

    [Header("Fish Item Data")]
    [SerializeField] private ItemData carpItem; // Easy fish
    [SerializeField] private ItemData largemouthBassItem; // Hard fish

    [Header("Standard Fish Settings")]
    [Range(0f, 1f)] public float standardStartFill = 0.5f;
    public float standardDrainPerSecond = 0.2f;
    public float standardGainOnCorrect = 0.15f;
    public float standardLossOnWrong = 0.1f;

    [Header("Hard Fish Settings (Largemouth Bass)")]
    [Range(0f, 1f)] public float hardStartFill = 0.4f;
    public float hardDrainPerSecond = 0.3f;
    public float hardGainOnCorrect = 0.1f;
    public float hardLossOnWrong = 0.12f;

    [Header("Energy Cost")]
    public int energyCostToStart = 10;

    float startFill;
    float drainPerSecond;
    float gainOnCorrect;
    float lossOnWrong;

    float currentFill;
    bool active;
    bool waitingForReplay;

    KeyCode currentKey;

    enum FishType { Standard, Hard }
    FishType currentFishType;

    readonly KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public void StartMinigame()
    {
        if (player != null && !player.HasEnoughEnergy(energyCostToStart))
        {
            if (keyPromptText != null)
                keyPromptText.text = "You are too tired to fish!";
            return;
        }

        if (player != null)
            player.UseEnergy(energyCostToStart);

        ChooseRandomFish();

        if (playerMove != null)
            playerMove.enabled = false;

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

    void ChooseRandomFish()
    {
        if (Random.value < 0.5f)
        {
            currentFishType = FishType.Standard;
            startFill = standardStartFill;
            drainPerSecond = standardDrainPerSecond;
            gainOnCorrect = standardGainOnCorrect;
            lossOnWrong = standardLossOnWrong;
        }
        else
        {
            currentFishType = FishType.Hard;
            startFill = hardStartFill;
            drainPerSecond = hardDrainPerSecond;
            gainOnCorrect = hardGainOnCorrect;
            lossOnWrong = hardLossOnWrong;
        }
    }

    void Check(KeyCode k)
    {
        if (k == currentKey)
            currentFill += gainOnCorrect;
        else
            currentFill -= lossOnWrong;

        currentFill = Mathf.Clamp01(currentFill);

        if (active) NewKey();
    }

    void NewKey()
    {
        currentKey = keys[Random.Range(0, keys.Length)];

        if (keyPromptText != null)
        {
            string fishLabel = currentFishType == FishType.Standard
                ? "Carp"
                : "Largemouth Bass";

            keyPromptText.text = fishLabel + " | Press: " + currentKey;
        }
    }

    void Win()
    {
        active = false;

        // Determine which fish was caught
        bool isStandard = currentFishType == FishType.Standard;
        ItemData caughtFish = isStandard ? carpItem : largemouthBassItem;
        string fishName = isStandard ? "Carp" : "Largemouth Bass";

        // Add fish to inventory
        if (caughtFish != null)
        {
            if (!InventoryManager.Instance.AddItem(caughtFish, 1))
            {
                Debug.LogWarning("Inventory full! Could not add " + fishName);
                fishName += " (Inventory Full!)";
            }
        }
        else
        {
            Debug.LogError("Fish ItemData not assigned in FishingMinigame!");
        }

        if (keyPromptText != null)
            keyPromptText.text = "You caught a " + fishName + "!";

        if (playerMove != null)
            playerMove.enabled = true;
    }

    void Lose()
    {
        active = false;
        waitingForReplay = true;

        if (keyPromptText != null)
            keyPromptText.text = "You lost! Play again? (Y/N)";
    }

    void RestartGame()
    {
        waitingForReplay = false;
        StartMinigame();
    }

    public void ExitGame()
    {
        waitingForReplay = false;

        if (playerMove != null)
            playerMove.enabled = true;

        if (minigameRoot != null)
            minigameRoot.SetActive(false);
    }

    void UpdateUI()
    {
        if (barFill != null)
            barFill.fillAmount = currentFill;

        if (percentText != null)
            percentText.text = Mathf.RoundToInt(currentFill * 100f) + "%";
    }
}
