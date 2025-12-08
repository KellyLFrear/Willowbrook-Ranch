using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private FundsAmount funds; // Reference to the FundsAmount script

    // ITEM DATA REFERENCES
    [Header("Seed Item Data")]
    [SerializeField] private ItemData tomatoSeedItem;
    [SerializeField] private ItemData eggplantSeedItem;
    [SerializeField] private ItemData mushroomSeedItem;
    [SerializeField] private ItemData carrotSeedItem;

    [Header("Crop Item Data")]
    [SerializeField] private ItemData tomatoCropItem;
    [SerializeField] private ItemData eggplantCropItem;
    [SerializeField] private ItemData mushroomCropItem;
    [SerializeField] private ItemData carrotCropItem;

    // SEED PRICES
    [Header("Seed Prices")]
    private int tomatoSeedPrice = 15;
    private int eggplantSeedPrice = 25;
    private int mushroomSeedPrice = 8;
    private int carrotSeedPrice = 12;

    // CROP PROFITS
    [Header("Crop Prices")]
    private int tomatoCropProfit = 25;
    private int eggplantCropProfit = 40;
    private int mushroomCropProfit = 12;
    private int carrotCropProfit = 30;

    // TEMPORARY HARD CODED CROP AMOUNTS FOR SELLING FUNCTION UNTIL INVENTORY IS IMPLEMENTED
    [Header("Temporary Crop Amounts")]
    private int tempTomatoCropsAmount = 5;
    private int tempEggplantCropsAmount = 5;
    private int tempMushroomCropsAmount = 5;
    private int tempCarrotCropsAmount = 5;

    private void Awake()
    {
        if (funds == null)
        {
            funds = FundsAmount.Instance; // Finds the FundsAmount instance
        }
    }

    private void Start()
    {
        // Add starting crops to inventory (replacing temp amounts)
        if (tomatoCropItem != null)
            InventoryManager.Instance.AddItem(tomatoCropItem, tempTomatoCropsAmount);
        if (eggplantCropItem != null)
            InventoryManager.Instance.AddItem(eggplantCropItem, tempEggplantCropsAmount);
        if (mushroomCropItem != null)
            InventoryManager.Instance.AddItem(mushroomCropItem, tempMushroomCropsAmount);
        if (carrotCropItem != null)
            InventoryManager.Instance.AddItem(carrotCropItem, tempCarrotCropsAmount);
    }


    // FUNCTION TO BUY TOMATO SEEDS
    public void BuyTomatoSeeds()
    {
        Debug.Log("Attempting to buy Tomato Seeds for " + tomatoSeedPrice + " gold.");

        if(FundsAmount.Instance.playerMoney >= tomatoSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= tomatoSeedPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(tomatoSeedItem, 1); // Add to inventory
            Debug.Log("Tomato Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
        {
            Debug.Log("Not enough gold to buy Tomato Seeds."); // Insufficient funds
        }
    }

    // FUNCTION TO BUY EGGPLANT SEEDS
    public void BuyEggplantSeeds()
    {
        Debug.Log("Attempting to buy Eggplant Seeds for " + eggplantSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= eggplantSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= eggplantSeedPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(eggplantSeedItem, 1); // Add to inventory
            Debug.Log("Eggplant Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
        {
            Debug.Log("Not enough gold to buy Eggplant Seeds."); // Insufficient funds
        }
    }

    // FUNCTION TO BUY MUSHROOM SEEDS
    public void BuyMushroomSeeds()
    {
        Debug.Log("Attempting to buy Mushroom Seeds for " + mushroomSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= mushroomSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= mushroomSeedPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(mushroomSeedItem, 1); // Add to inventory
            Debug.Log("Mushroom Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
        {
            Debug.Log("Not enough gold to buy Mushroom Seeds."); // Insufficient funds
        }
    }

    // FUNCTION TO BUY CARROT SEEDS
    public void BuyCarrotSeeds()
    {
        Debug.Log("Attempting to buy Carrot Seeds for " + carrotSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= carrotSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= carrotSeedPrice; // Deduct The Price
            InventoryManager.Instance.AddItem(carrotSeedItem, 1); // Add to inventory
            Debug.Log("Carrot Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
        {
            Debug.Log("Not enough gold to buy Carrot Seeds."); // Insufficient funds
        }
    }

    // FUNCTION TO SELL TOMATO CROPS
    public void SellTomatoCrops()
    {
        if (tomatoCropItem == null)
        {
            Debug.LogError("Tomato Crop ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(tomatoCropItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Tomato Crop.");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
            FundsAmount.Instance.playerMoney += tomatoCropProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Tomato Crops available to sell.");
        }
    }

    // FUNCTION TO SELL EGGPLANT CROPS
    public void SellEggplantCrops()
    {
        if (eggplantCropItem == null)
        {
            Debug.LogError("Eggplant Crop ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(eggplantCropItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Eggplant Crop.");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
            FundsAmount.Instance.playerMoney += eggplantCropProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Eggplant Crops Available To Sell.");
        }
    }

    // FUNCTION TO SELL MUSHROOMS CROPS
    public void SellMushroomCrops()
    {
        if (mushroomCropItem == null)
        {
            Debug.LogError("Mushroom Crop ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(mushroomCropItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Mushroom Crop.");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
            FundsAmount.Instance.playerMoney += mushroomCropProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Mushroom Crops Available To Sell.");
        }
    }

    // FUNCTION TO SELL CARROT CROPS
    public void SellCarrotCrops()
    {
        if (carrotCropItem == null)
        {
            Debug.LogError("Carrot Crop ItemData not assigned!");
            return;
        }

        // Find the item in inventory
        int slotIndex = FindItemInInventory(carrotCropItem);
        if (slotIndex >= 0)
        {
            Debug.Log("Selling a Carrot Crop.");
            InventoryManager.Instance.RemoveFromSlot(slotIndex, 1); // Remove one from inventory
            FundsAmount.Instance.playerMoney += carrotCropProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Carrot Crops Available To Sell.");
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
