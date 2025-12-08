using UnityEngine;

public class FishMarketManager : MonoBehaviour
{
    [SerializeField] private FundsAmount funds; // Reference To The FundsAmount Script

    // FISH ITEM DATA REFERENCES
    [Header("Fish Item Data")]
    [SerializeField] private ItemData carpItem;
    [SerializeField] private ItemData largemouthBassItem;

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

    private void Start()
    {
        // Add starting fish to inventory (replacing temp amounts)
        if (carpItem != null)
            InventoryManager.Instance.AddItem(carpItem, tempCarpAmount);
        if (largemouthBassItem != null)
            InventoryManager.Instance.AddItem(largemouthBassItem, tempLargemouthBassAmount);
    }

    // FUNCTION TO BUY CARP FOR FOOD
    public void BuyCarp()
    {
        Debug.Log("Attempting to buy Carp for " + carpPrice + " gold.");
        if (FundsAmount.Instance.playerMoney >= carpPrice)
        {
            FundsAmount.Instance.playerMoney -= carpPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(carpItem, 1); // Add to inventory
            Debug.Log("Carp Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney);
        }
        else
        {
            Debug.Log("Not enough gold to buy Carp.");
        }
    }

    // FUNCTION TO BUY LARGEMOUTH BASS FOR FOOD
    public void BuyLargemouthBass()
    {
        Debug.Log("Attempting to buy Largemouth Bass for " + largemouthBassPrice + " gold.");
        if (FundsAmount.Instance.playerMoney >= largemouthBassPrice)
        {
            FundsAmount.Instance.playerMoney -= largemouthBassPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(largemouthBassItem, 1); // Add to inventory
            Debug.Log("Largemouth Bass Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney);
        }
        else
        {
            Debug.Log("Not enough gold to buy Largemouth Bass.");
        }
    }

    // FUNCTION TO SELL CARP
    public void SellCarp()
    {
        if (carpItem == null)
        {
            Debug.LogError("Carp ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(carpItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Carp");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
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
        if (largemouthBassItem == null)
        {
            Debug.LogError("Largemouth Bass ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(largemouthBassItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Largemouth Bass");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
            FundsAmount.Instance.playerMoney += largemouthBassProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Largemouth Bass Available To Sell.");
        }
    }

    // HELPER FUNCTION TO FIND ITEM IN INVENTORY
    private int FindItemInInventory(ItemData item)
    {
        for (int i = 0; i < InventoryManager.Instance.slots.Count; i++)
        {
            var slot = InventoryManager.Instance.GetSlot(i);
            if (slot != null && !slot.IsEmpty && slot.item == item)
            {
                return i;
            }
        }
        return -1; // Not found
    }
}
