using UnityEngine;

public class FishMarketManager : MonoBehaviour
{
    [SerializeField] private FundsAmount funds; // Reference To The FundsAmount Script

    // FISH PRICES
    [Header("Fish Prices")]
    private int carpPrice = 18;
    private int largemouthBassPrice = 35;

    // FISH PROFITS
    [Header("Fish Profits")]
    private int carpProfit = 15;
    private int largemouthBassProfit = 30;

    // TEMPORARY HARD CODED FISH AMOUNTS FOR SELLING FUNCTION UNTIL INVENTORY IS IMPLEMENTED
    [Header("Temporary Fish Amounts")]
    private int tempCarpAmount = 5;
    private int tempLargemouthBassAmount = 5;

    private void Awake()
    {
        if (funds == null)
        {
            funds = FundsAmount.Instance; // Finds the FundsAmount Instance
        }
    }

    // FUNCTION TO BUY CARP FOR FOOD
    public void BuyCarp()
    {
        Debug.Log("Attempting to buy Carp for " + carpPrice + " gold.");
        if (FundsAmount.Instance.playerMoney >= carpPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= carpPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("Carp", 1);
            Debug.Log("Carp Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
            Debug.Log("Not enough gold to buy Carp."); // Insufficient funds
    }

    // FUNCTION TO BUY LARGEMOUTH BASS FOR FOOD
    public void BuyLargemouthBass()
    {
        Debug.Log("Attempting to buy Largemouth Bass for " + largemouthBassPrice + " gold.");
        if (FundsAmount.Instance.playerMoney >= largemouthBassPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= largemouthBassPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("LargemouthBass", 1);
            Debug.Log("Largemouth Bass Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
            Debug.Log("Not enough gold to buy Largemouth Bass."); // Insufficient funds
    }

    // FUNCTION TO SELL CARP
    public void SellCarp()
    {
        if (tempCarpAmount > 0)
        {
            Debug.Log("Selling a Carp");
            tempCarpAmount--; // Subtract One From The Temporary Carp Amount
            FundsAmount.Instance.playerMoney += carpProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Carp Available To Sell.");
        }
    }

    // FUNCTION TO SELL LARGEMOUTH BASS
    public void SellLargemouthBass()
    {
        if (tempLargemouthBassAmount > 0)
        {
            Debug.Log("Selling a Largemouth Bass");
            tempLargemouthBassAmount--; // Subtract One From The Temporary Largemouth Bass Amount
            FundsAmount.Instance.playerMoney += largemouthBassProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Largemouth Bass Available To Sell.");
        }
    }
}
