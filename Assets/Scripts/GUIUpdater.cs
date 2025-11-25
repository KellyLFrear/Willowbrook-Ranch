using UnityEngine;
using TMPro;

public class GUIUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text GUIFundsMsg;
    [SerializeField] private TMP_Text GUIClockTimeMsg;
    [SerializeField] private TMP_Text GUIDayMsg;

    public GameObject passedOutPopUpPanel; // Reference to the passed out pop-up panel
    public GameObject creditsPopUpPanel;   // Reference to the credits pop-up panel
    public GameObject marketPopUpPanel;    // Reference to the market pop-up panel

    public static GUIUpdater Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (GUIDayMsg == null)
            Debug.LogError("GUIUpdater: GUIDayMsg is not assigned in the Inspector!");

        if (GUIClockTimeMsg == null)
            Debug.LogError("GUIUpdater: GUIClockTimeMsg is not assigned in the Inspector!");

        if (GUIFundsMsg == null)
            Debug.LogError("GUIUpdater: GUIFundsMsg is not assigned in the Inspector!");

        // Hide all popups at the start
        HidePassedOutPopUp();
        HideCreditsPopUp();
        HideGeneralStoreGUI();
    }

    void Update()
    {
        // Updates The Day GUI
        if (GUIDayMsg != null && LightingManager.Instance != null)
        {
            GUIDayMsg.text = " " + LightingManager.Instance.CurrentDay;
        }

        // Updates The Funds GUI
        if (GUIFundsMsg != null && FundsAmount.Instance != null)
        {
            GUIFundsMsg.text = FundsAmount.Instance.playerMoney + "g";
        }

        // Updates The Clock GUI
        if (GUIClockTimeMsg != null && LightingManager.Instance != null)
        {
            int hour24 = LightingManager.Instance.CurrentHour;
            int minute = LightingManager.Instance.CurrentMinute;

            hour24 = hour24 % 24;

            int hour12;
            string suffix;

            if (hour24 == 0)
            {
                hour12 = 12;
                suffix = "am";
            }
            else if (hour24 < 12)
            {
                hour12 = hour24;
                suffix = "am";
            }
            else if (hour24 == 12)
            {
                hour12 = 12;
                suffix = "pm";
            }
            else
            {
                hour12 = hour24 - 12;
                suffix = "pm";
            }

            GUIClockTimeMsg.text = $"{hour12:00}:{minute:00}{suffix}";
        }
    }

    // FUNCTION TO SHOW PASSED OUT POPUP
    public void ShowPassedOutPopUp()
    {
        if (passedOutPopUpPanel != null)
            passedOutPopUpPanel.SetActive(true);
    }

    // FUNCTION TO HIDE PASSED OUT POPUP
    public void HidePassedOutPopUp()
    {
        if (passedOutPopUpPanel != null)
            passedOutPopUpPanel.SetActive(false);
    }

    // FUNCTION TO SHOW CREDITS POPUP
    public void ShowCreditsPopUp()
    {
        if (creditsPopUpPanel != null)
            creditsPopUpPanel.SetActive(true);
    }

    // FUNCTION TO HIDE CREDITS POPUP
    public void HideCreditsPopUp()
    {
        if (creditsPopUpPanel != null)
            creditsPopUpPanel.SetActive(false);
    }

    // FUNCTION TO SHOW MARKET GUI
    public void LoadGeneralStoreGUI()
    {
        if (marketPopUpPanel != null)
            marketPopUpPanel.SetActive(true);
    }

    // FUNCTION TO HIDE MARKET GUI
    public void HideGeneralStoreGUI()
    {
        if (marketPopUpPanel != null)
            marketPopUpPanel.SetActive(false);
    }
}
