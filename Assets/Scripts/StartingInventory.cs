using System.Collections;
using UnityEngine;

/// <summary>
/// Gives the player starting items when the game begins.
/// Attach this to a GameObject in Scene1-Farm (the first gameplay scene).
/// Uses DontDestroyOnLoad pattern to ensure it only runs ONCE per game session.
/// Assign items in Inspector - any left null will be skipped.
/// </summary>
public class StartingInventory : MonoBehaviour
{
    public static StartingInventory Instance { get; private set; }

    [Header("Starting Seeds")]
    [SerializeField] private ItemData tomatoSeeds;
    [SerializeField] private int tomatoSeedsAmount = 5;
    [SerializeField] private ItemData eggplantSeeds;
    [SerializeField] private int eggplantSeedsAmount = 5;
    [SerializeField] private ItemData mushroomSeeds;
    [SerializeField] private int mushroomSeedsAmount = 5;
    [SerializeField] private ItemData carrotSeeds;
    [SerializeField] private int carrotSeedsAmount = 5;

    [Header("Starting Crops")]
    [SerializeField] private ItemData tomatoCrop;
    [SerializeField] private int tomatoCropAmount = 5;
    [SerializeField] private ItemData eggplantCrop;
    [SerializeField] private int eggplantCropAmount = 5;
    [SerializeField] private ItemData mushroomCrop;
    [SerializeField] private int mushroomCropAmount = 5;
    [SerializeField] private ItemData carrotCrop;
    [SerializeField] private int carrotCropAmount = 5;

    [Header("Starting Fish")]
    [SerializeField] private ItemData carp;
    [SerializeField] private int carpAmount = 5;
    [SerializeField] private ItemData largemouthBass;
    [SerializeField] private int largemouthBassAmount = 5;

    [Header("Starting Tools")]
    [SerializeField] private ItemData wateringCan;
    [SerializeField] private int wateringCanAmount = 1;
    [SerializeField] private ItemData shovel;
    [SerializeField] private int shovelAmount = 1;

    private void Awake()
    {
        // Singleton pattern - only run once per game session
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(AddItemsNextFrame());
    }

    private IEnumerator AddItemsNextFrame()
    {
        // Wait one frame to ensure all UI systems are initialized
        yield return null;

        // Add starting seeds
        AddItemIfNotNull(tomatoSeeds, tomatoSeedsAmount, "Tomato Seeds");
        AddItemIfNotNull(eggplantSeeds, eggplantSeedsAmount, "Eggplant Seeds");
        AddItemIfNotNull(mushroomSeeds, mushroomSeedsAmount, "Mushroom Seeds");
        AddItemIfNotNull(carrotSeeds, carrotSeedsAmount, "Carrot Seeds");

        // Add starting crops
        AddItemIfNotNull(tomatoCrop, tomatoCropAmount, "Tomato");
        AddItemIfNotNull(eggplantCrop, eggplantCropAmount, "Eggplant");
        AddItemIfNotNull(mushroomCrop, mushroomCropAmount, "Mushroom");
        AddItemIfNotNull(carrotCrop, carrotCropAmount, "Carrot");

        // Add starting fish
        AddItemIfNotNull(carp, carpAmount, "Carp");
        AddItemIfNotNull(largemouthBass, largemouthBassAmount, "Largemouth Bass");

        // Add starting tools
        AddItemIfNotNull(wateringCan, wateringCanAmount, "Watering Can");
        AddItemIfNotNull(shovel, shovelAmount, "Shovel");
    }

    private void AddItemIfNotNull(ItemData item, int amount, string itemName)
    {
        if (item != null)
        {
            InventoryManager.Instance.AddItem(item, amount);
        }
    }
}
