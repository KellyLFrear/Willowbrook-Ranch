using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static FundsAmount Instance; // References The Player's Gold

    // SEED PRICES
    [Header("Seed Prices")]
    private int tomatoSeedPrice = 15; 
    private int eggplantSeedPrice = 25;
    private int mushroomSeedPrice = 8;
    private int carrotSeedPrice = 12;

    // CROP PROFITS
    [Header("Crop Prices")]
    private int tomatoCropProfit = 25;
    private int eggplantSeedProfit = 40;
    private int mushroomSeedProfit = 12;
    private int carrotSeedProfit = 30;

    // FUNCTION TO BUY TOMATO SEEDS
    public void BuyTomatoSeeds
    {

    }
}
