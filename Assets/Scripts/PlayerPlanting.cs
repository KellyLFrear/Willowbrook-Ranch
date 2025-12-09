using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [Header("Setup")]
    // public GameObject plantPrefab; THIS CAN BE IGNORED
    public Camera mainCamera; // Still kept just in case, though we use player position now
    public LayerMask plantableLayer; // IMPORTANT: Assign "PlantableGround" here in Inspector

    [Header("Settings")]
    // How far down we check for a tile. 2.0f is usually enough to reach the ground.
    public float interactionDistance = 2.0f;

    private PlayerAnimation playerAnimation;

    void Start()
    {
        // Safety checks to prevent errors if things aren't assigned
        if (mainCamera == null) mainCamera = Camera.main;
        playerAnimation = GetComponent<PlayerAnimation>();
        /*
        if (plantPrefab == null)
        {
            Debug.LogError("PlayerPlanting: Missing Plant Prefab!");
        }
        */
    }

    void Update()
    {
        // Inputs
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPlant();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TryHarvest();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryWater();
        }

        // Debug Visualization: Draws a red line in the Scene view showing where the player is checking
        // Origin: Feet + slightly up. Direction: Down.
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * interactionDistance, Color.red);
    }
    
    //checks tile beneath player and waters plant if possible
    public void TryWater(){

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit hit, interactionDistance, plantableLayer)){
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if(tile != null && tile.isOccupied){
                //get the plamt growth component to call water method
                PlantGrowth plant = tile.GetCurrentPlant();
                if(plant != null){
                    //if watering was successful, trigger animation
                    if(plant.Water()){
                        if(playerAnimation != null) playerAnimation.TriggerWatering();
                        return;
                    }
                }
            }
        }
            Debug.Log("TryWater: No tile found beneath player.");
        
    }

    public void TryPlant()
    {
        // 1. Get the currently selected item (Placeholder: Assumes slot 0 is the active item)
        // You MUST implement GetCurrentHeldItem() in InventoryManager for this line to work.
        InventorySlot activeSlot = InventoryManager.Instance.GetSlot(0); 
        
        if (activeSlot == null || activeSlot.IsEmpty || activeSlot.item.category != ItemCategory.Seed)
        {
            Debug.Log("TryPlant: Player is not holding a seed item.");
            return;
        }

        ItemData seedItem = activeSlot.item;
        GameObject plantToSpawn = seedItem.plantPrefab;

        if (plantToSpawn == null)
        {
            Debug.LogError($"TryPlant: Seed item '{seedItem.displayName}' is missing a Plant Prefab reference!");
            return;
        }
        // 2. Raycast to find the tile 
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, plantableLayer))
        {
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                // 3. Attempt to plant with the specific prefab from the ItemData
                if (tile.TryPlant(plantToSpawn, hit.point))
                {
                    Debug.Log($"Success! Planted {seedItem.displayName} on {tile.name}");

                    // 4. Consume the seed from the inventory
                    InventoryManager.Instance.RemoveFromSlot(0, 1); // Remove 1 from the active slot

                    if (playerAnimation != null) playerAnimation.TriggerPickingFruit();
                }
                else
                {
                    Debug.Log("Could not plant (Tile is occupied).");
                }
            }
        }
        else
        {
            Debug.Log("TryPlant: No plantable tile found beneath player.");
        }
    }

    public void TryHarvest()
    {
        //Cast a ray DOWN from the player
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, plantableLayer))
        {
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                // 2. Attempt to harvest
                if (tile.TryHarvest())
                {
                    Debug.Log($"Harvested from {tile.name}");
                    if (playerAnimation != null) playerAnimation.TriggerHarvesting();
                }
            }
        }
        else
        {
            Debug.Log("TryHarvest: No tile found beneath player.");
        }
    }
}