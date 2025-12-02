using UnityEngine;

public class FundsAmount : MonoBehaviour
{
    public static FundsAmount Instance;   // So We Can Reference The Player's Gold
    public int playerMoney = 50;        // Player Starts Game With 50 Gold

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("FundsAmount created: " + gameObject.name);
        }
        else if (Instance != this)
        {
            // Sync data from the existing one before destroying
            Instance.playerMoney = Mathf.Max(Instance.playerMoney, playerMoney);
            Debug.Log("Duplicate FundsAmount found — syncing values, then destroying this one.");
            Destroy(gameObject);
        }
    }

}