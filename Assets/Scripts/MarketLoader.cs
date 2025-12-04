using UnityEngine;

public class MarketLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GeneralStoreTag"))
        {
            if (GUIUpdater.Instance != null)
            {
                GUIUpdater.Instance.LoadGeneralStoreGUI();
            }
            else
            {
                Debug.LogWarning("GUIUpdater.Instance is null – make sure a GUIUpdater is in the scene.");
            }
        }

        if (other.CompareTag("FishMarketTag"))
        {
            if (GUIUpdater.Instance != null)
            {
                GUIUpdater.Instance.LoadFishMarketGUI();
            }
            else
            {
                Debug.LogWarning("GUIUpdater.Instance is null – make sure a GUIUpdater is in the scene.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GeneralStoreTag"))
        {
            if (GUIUpdater.Instance != null)
            {
                GUIUpdater.Instance.HideGeneralStoreGUI(); // Hides Market Pop-Up
            }
        }

        if(other.CompareTag("FishMarketTag"))
        {
            if (GUIUpdater.Instance != null)
            {
                GUIUpdater.Instance.HideFishMarketGUI(); // Hides Fish Market Pop-Up
            }
        }    
    }
}
