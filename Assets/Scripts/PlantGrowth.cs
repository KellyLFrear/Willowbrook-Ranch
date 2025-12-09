using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [Header("Stage Visuals")]
    public GameObject stage0_Dead;//dead stage
    public GameObject stage1_Sprout;//sprout stage
    public GameObject stage2_Growing;//growing stage
    public GameObject stage3_Mature;//mature stage

    [Header("State")]
    private bool isWatered = false;//tracks if player watered the plant
    private int currentStage = 1;//sets current stage to sprout
    private PlantableTile myTile; //reference back to the tile we are planted on

    [Header("Harvest Reward")]
    public ItemData harvestItemData; //item data for resulting crop
    public int harvestAmount = 1; //amount of crop to give on harvest
    
    void Start()
    {
        // ensures dead plant stage is inactive when starting
        if(stage0_Dead) stage0_Dead.SetActive(false); 
        ShowStage(currentStage);//starts the plant at the sprout stage
    }

    // The PlantableTile script will call this right after planting
    public void SetTile(PlantableTile tile)
    {
        myTile = tile;
    }

    // Public check to see if we are mature
    public bool IsMature()
    {
        return currentStage >= 3;
    }

    /// <summary>
    /// Attempts to water the plant, setting the 'isWatered' flag for the day.
    /// </summary>
    public bool Water(){
        // prevents watering if the plant is dead or mature
        if(currentStage == 0 || currentStage >= 3){
            Debug.Log("This plant can't be watered!");
            return false;
        }
        // prevents watering if the plant has already been watered today
        if(isWatered){
            Debug.Log("This plant has already been watered today!");
            return false;
        }

        isWatered = true;
        Debug.Log("You watered the plant!");
        return true;
    }

    /// <summary>
    /// Executes the daily growth/death logic based on the 'isWatered' state.
    /// </summary>
    public void AdvanceDay(){
        // 1. Check death of plant (if not watered and currently alive)
        if(!isWatered && currentStage > 0){
            Die();
            isWatered = false; // Reset just in case, though Die() handles it
            return;
        }
        
        // 2. Check growth of plant (if watered and not mature yet)
        if (isWatered && currentStage < 3)
        {
            currentStage++;
            ShowStage(currentStage);
            Debug.Log("The plant has grown to stage " + currentStage);
        }
        else if(currentStage == 3)
        {
            Debug.Log("The plant is already mature!");
        }
        
        // 3. Reset watered state for the next day
        isWatered = false;
    }

    public void Die()
    {
        currentStage = 0;
        ShowStage(currentStage);
        Debug.Log("The plant has died!");
        
        // OPTIONAL: You might want to remove the plant from the manager here too, 
        // depending on whether the dead plant stays registered until the player clears it.
    }
    
    // Removed the redundant public void Grow() method. All growth now uses AdvanceDay().

    private void ShowStage(int stage)
    {
        // Deactivate all stages first
        if (stage0_Dead) stage0_Dead.SetActive(false);
        if (stage1_Sprout) stage1_Sprout.SetActive(false);
        if (stage2_Growing) stage2_Growing.SetActive(false);
        if (stage3_Mature) stage3_Mature.SetActive(false);

        // Activate the correct stage
        switch (stage)
        {
            case 0:
                if (stage0_Dead) stage0_Dead.SetActive(true);
                break;
            case 1:
                if (stage1_Sprout) stage1_Sprout.SetActive(true);
                break;
            case 2:
                if (stage2_Growing) stage2_Growing.SetActive(true);
                break;
            case 3:
                if (stage3_Mature) stage3_Mature.SetActive(true);
                break;
        }
    }

    // Called by the PlantableTile
    public void Harvest()
    {
  //must be mature to harvest
        if (currentStage != 3)
        {
            Debug.Log("The plant isn't mature!");
            return;
        }
        
        //harvest logic
        if (harvestItemData != null)
        {
            // Add the crop to the player's inventory
            bool added = InventoryManager.Instance.AddItem(harvestItemData, harvestAmount);
            if (!added)
            {
                Debug.LogWarning($"Harvested, but failed to add all {harvestItemData.displayName} to inventory (Inventory Full).");
                // You might want to drop the item on the ground here instead
            }
        }
        else
        {
            Debug.LogError("Harvest Item Data is missing from PlantGrowth script!");
        }
        
        Debug.Log("Harvested the plant!");
        
        //clean up tile, remove from the manager, destroy object
        if(myTile != null){
            myTile.ClearTile();
        }
        if(PlantManager.Instance != null){
            PlantManager.Instance.UnregisterPlant(this);
        }
        Destroy(gameObject);
    }
}