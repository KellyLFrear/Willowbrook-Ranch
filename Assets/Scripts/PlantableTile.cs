using UnityEngine;

public class PlantableTile : MonoBehaviour
{
    public bool isOccupied = false;
    private PlantGrowth currentPlant = null;

    /// <summary>
    /// Returns the PlantGrowth component currently on this tile (if any).
    /// This is needed for interactions like watering.
    /// </summary>
    public PlantGrowth GetCurrentPlant()
    {
        return currentPlant;
    }

    /// Attempts to spawn a plant on the tile.
    public bool TryPlant(PlantData data, Vector3 hitPoint)
    {
        if (isOccupied)
        {
            Debug.Log($"[TILE] {name} is already occupied.");
            return false;
        }

        if (data == null || data.sproutStagePrefab == null)
        {
            Debug.LogError("[TILE] No plant prefab assigned or plant data!");
            return false;
        }

        // Calculate spawn position slightly above the tile surface
        Vector3 spawnPos = transform.position + Vector3.up * 0.1f;
        //instantiate the plant prefab
        GameObject plantObject = Instantiate(data.sproutStagePrefab, spawnPos, Quaternion.identity);
        // Get the PlantGrowth script, set its tile reference, and register it
        currentPlant = plantObject.GetComponent<PlantGrowth>();
        
        if (currentPlant != null)
        {
            currentPlant.Initialize(data, this);// Set plant data and tile reference
            
            // Register the new plant with the global manager
            if (PlantManager.Instance != null)
            {
                PlantManager.Instance.RegisterPlant(currentPlant);
            }
        }
        else
        {
            Debug.LogError("[TILE] Plant prefab is missing PlantGrowth script!");
            Destroy(plantObject);// Clean up
            return false;// Failed to plant
        }

        isOccupied = true;
        return true;
    }


    
    /// Attempts to harvest the plant on the tile.
        public bool TryHarvest()
    {
        if (!isOccupied || currentPlant == null)
        {
            Debug.Log($"Nothing to harvest on {name}");
            return false;
        }

        // Check if the plant is mature before allowing harvest
        if (!currentPlant.IsMature())
        {
            Debug.Log($"Plant on {name} not mature yet.");
            return false;
        }

        currentPlant.Harvest();
        return true;
    }

    
    /// Clears the tile state after a plant is harvested or destroyed.
    public void ClearTile()
    {
        isOccupied = false;
        currentPlant = null;
    }
}