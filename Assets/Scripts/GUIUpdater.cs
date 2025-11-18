using UnityEngine;
using TMPro;

public class GUIUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text GUIFundsMsg;
    [SerializeField] private TMP_Text GUIClockTimeMsg;

    void Start()
    {
        if (GUIFundsMsg == null)
            Debug.LogError("GUIUpdater: GUIFundsMsg is not assigned in the Inspector!");

        if (GUIClockTimeMsg == null)
            Debug.LogError("GUIUpdater: GUIClockTimeMsg is not assigned in the Inspector!");
    }

    void Update()
    {
        // Updates The Funds GUI
        if (GUIFundsMsg != null && FundsAmount.Instance != null)
        {
            GUIFundsMsg.text = "Gold: " + FundsAmount.Instance.playerMoney + "g";
        }

        // Updates The Clock GUI
        if (GUIClockTimeMsg != null && LightingManager.Instance != null)
        {
            int hour24 = LightingManager.Instance.CurrentHour; // Grabs The Hour From The 24 Hour Cycle
            int minute = LightingManager.Instance.CurrentMinute; // Grabs The Minute

            string suffix = "am"; // Adds Am
            int hour12 = hour24; // Sets The 24Hour To 12Hour

            if (hour24 == 0) // From 8am-11:59am
            {
                hour12 = 12; // Midnight = 12am
                suffix = "am"; // Keeps Am
            }
            else if (hour24 == 12) // If It's Noon
            {
                hour12 = 12; // Noon = 12pm
                suffix = "pm"; // Changes To Pm
            }
            else if (hour24 > 12)
            {
                hour12 = hour24 - 12; // Convert Into 24 Hour Cycle To 12 Hour Cycle
                suffix = "pm"; // Changes to Pm
            }
            else
            {
                suffix = "am"; // Keeps Am
            }

            GUIClockTimeMsg.text = $"Time: {hour12:00}:{minute:00}{suffix}"; // Prints The Message
        }

    }
}
