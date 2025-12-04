using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private FundsAmount funds; // Reference to the FundsAmount script

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


    // FUNCTION TO BUY TOMATO SEEDS
    public void BuyTomatoSeeds()
    {
        Debug.Log("Attempting to buy Tomato Seeds for " + tomatoSeedPrice + " gold.");

        if(FundsAmount.Instance.playerMoney >= tomatoSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= tomatoSeedPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("TomatoSeed", 1);
            Debug.Log("Tomato Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase

        }

        else
            Debug.Log("Not enough gold to buy Tomato Seeds."); // Insufficient funds
    }

    // FUNCTION TO BUY EGGPLANT SEEDS
    public void BuyEggplantSeeds()
    {
        Debug.Log("Attempting to buy Eggplant Seeds for " + eggplantSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= eggplantSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= eggplantSeedPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("EggplantSeed", 1);
            Debug.Log("Eggplant Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
            Debug.Log("Not enough gold to buy Eggplant Seeds."); // Insufficient funds
    }

    // FUNCTION TO BUY MUSHROOM SEEDS
    public void BuyMushroomSeeds()
    {
        Debug.Log("Attempting to buy Mushroom Seeds for " + mushroomSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= mushroomSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= mushroomSeedPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("MushroomSeed", 1);
            Debug.Log("Mushroom Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
            Debug.Log("Not enough gold to buy Mushroom Seeds."); // Insufficient funds
    }

    // FUNCTION TO BUY CARROT SEEDS
    public void BuyCarrotSeeds()
    {
        Debug.Log("Attempting to buy Carrot Seeds for " + carrotSeedPrice + " gold.");
        if(FundsAmount.Instance.playerMoney >= carrotSeedPrice) // Check If The Player Has Enough Gold
        {
            FundsAmount.Instance.playerMoney -= carrotSeedPrice; // If The Player Has Enough Gold, Deduct The Price
            // CHANGE THIS LINE WHEN WE HAVE THE INVENTORY:
            // InventoryManager.Instance.AddItem("CarrotSeed", 1);
            Debug.Log("Carrot Seeds Purchased. Remaining Gold: " + FundsAmount.Instance.playerMoney); // Successful purchase
        }
        else
            Debug.Log("Not enough gold to buy Carrot Seeds."); // Insufficient funds
    }

    // FUNCTION TO SELL TOMATO CROPS
    public void SellTomatoCrops()
    {
        if(tempTomatoCropsAmount > 0)
        {
            Debug.Log("Selling a Tomato Crop.");
            tempTomatoCropsAmount--; // Subtract One From The Temporary Crop Amount
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
        if (tempEggplantCropsAmount > 0)
        {
            Debug.Log("Selling a Eggplant Crop.");
            tempEggplantCropsAmount--; // Subtract One From The Temporary Crop Amount
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
        if (tempMushroomCropsAmount > 0)
        {
            Debug.Log("Selling a Mushroom Crop.");
            tempMushroomCropsAmount--; // Subtract One From The Temporary Crop Amount
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
        if (tempCarrotCropsAmount > 0)
        {
            Debug.Log("Selling a Carrot Crop.");
            tempCarrotCropsAmount--; // Subtract One From The Temporary Crop Amount
            FundsAmount.Instance.playerMoney += carrotCropProfit; // Add The Profit To The Player's Gold
        }
        else
        {
            Debug.Log("No Carrot Crops Available To Sell.");
        }
    }
}
