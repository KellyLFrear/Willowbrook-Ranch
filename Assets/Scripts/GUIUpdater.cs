using UnityEngine;
using TMPro;

public class GUIUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text GUIFundsMsg;
    [SerializeField] private TMP_Text GUIClockTimeMsg;
    [SerializeField] private TMP_Text GUIDayMsg;

    public GameObject passedOutPopUpPanel; // Reference to the passed out pop-up panel

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
        if(GUIClockTimeMsg == null)
            Debug.LogError("GUIUpdater: GUIDayMsg is not assigned in the Inspector!");

        if (GUIClockTimeMsg == null)
            Debug.LogError("GUIUpdater: GUIClockTimeMsg is not assigned in the Inspector!");

        if (GUIFundsMsg == null)
            Debug.LogError("GUIUpdater: GUIFundsMsg is not assigned in the Inspector!");

        HidePassedOutPopUp(); // Hides The Passed Out Pop-Up At The Start Of The Game

    }

    void Update()
    {
        // Updates The Day GUI
        if (GUIDayMsg != null && LightingManager.Instance != null)
        {
            GUIDayMsg.text = "Day: " + LightingManager.Instance.CurrentDay;
        }

        // Updates The Funds GUI
        if (GUIFundsMsg != null && FundsAmount.Instance != null)
        {
            GUIFundsMsg.text = "Gold: " + FundsAmount.Instance.playerMoney + "g";
        }

        // Updates The Clock GUI
        if (GUIClockTimeMsg != null && LightingManager.Instance != null)
        {
            int hour24 = LightingManager.Instance.CurrentHour; // Sets The Hour In 24-Hour Format
            int minute = LightingManager.Instance.CurrentMinute; // Sets The Minute

            hour24 = hour24 % 24; // Ensures Hour Is Within 0-23 Range

            int hour12;
            string suffix;

            if (hour24 == 0) // From 00:00 To 12am
            {
                hour12 = 12; // Sets Hour To 12
                suffix = "am"; // Sets Suffix To "am"
            }
            else if (hour24 < 12)  // From 01:00 To 11:59
            {
                hour12 = hour24; // Since It's Before Noon, Keeps The Hour Is Same As 24-Hour Format
                suffix = "am"; // Sets Suffix To "am"
            }
            else if (hour24 == 12) // Noon
            {
                hour12 = 12; // Keeps Hour As 12
                suffix = "pm"; // Sets Suffix To "pm"
            }
            else // From 1 To 11:59pm
            {
                hour12 = hour24 - 12; // Converts To 12-Hour Format
                suffix = "pm"; // Sets Suffix To "pm"
            }

            GUIClockTimeMsg.text = $"Time: {hour12:00}:{minute:00}{suffix}"; // Updates The Clock GUI With Formatted Time
        }
    }


    // Function Show The Passed Out Pop-Up Window
    public void ShowPassedOutPopUp()
    {
        if (passedOutPopUpPanel != null)
            passedOutPopUpPanel.SetActive(true); // Shows The Pop-Up Panel That Appears When Passing Out
    }

    // Function Hide Both Pop-Up Windows
    public void HidePassedOutPopUp()
    {
        if (passedOutPopUpPanel != null)
            passedOutPopUpPanel.SetActive(false); // Hides The Pop-Up Panel That Appears When Passing Out
    }
}
