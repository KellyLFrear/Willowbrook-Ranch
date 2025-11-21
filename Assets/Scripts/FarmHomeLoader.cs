using UnityEngine;
using UnityEngine.UI;

public class FarmHomeLoader : MonoBehaviour
{
    public GameObject housePopUpPanel; // Reference to the pop-up panel

    private void Start()
    {
        HideHousePopUp(); // Hide the pop-up at game start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player enters the farm home trigger
        {
            ShowHousePopUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideHousePopUp(); // Hide the house pop-up when the player exits
        }
    }

    // Function Show The House Pop-Up Window
    public void ShowHousePopUp()
    {
        if (housePopUpPanel != null)
            housePopUpPanel.SetActive(true); // Shows The Pop-Up Panel That Appears When Entering The Farm Home
    }

    // Function Hide The House Pop-Up Window
    public void HideHousePopUp()
    {
        if (housePopUpPanel != null)
            housePopUpPanel.SetActive(false); // Hides The Pop-Up Panel That Appears When Entering The Farm Home
    }
}
