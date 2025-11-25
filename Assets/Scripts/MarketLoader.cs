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
            Debug.Log("This Would Make The Fish Market Appear!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GeneralStoreTag"))
        {
            if (GUIUpdater.Instance != null)
            {
                GUIUpdater.Instance.HideGeneralStoreGUI(); // hide market popup
            }
        }
    }
}
