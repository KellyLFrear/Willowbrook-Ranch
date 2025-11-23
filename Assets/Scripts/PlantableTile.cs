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

    /// <summary>
    /// Attempts to spawn a plant on the tile.
    /// </summary>
    public bool TryPlant(GameObject plantPrefab, Vector3 hitPoint)
    {
        if (isOccupied)
        {
            Debug.Log($"[TILE] {name} is already occupied.");
            return false;
        }

        if (plantPrefab == null)
        {
            Debug.LogError("[TILE] No plant prefab assigned!");
            return false;
        }

        // Calculate spawn position slightly above the tile surface
        Vector3 spawnPos = transform.position + Vector3.up * 0.1f;
        GameObject plantObject = Instantiate(plantPrefab, spawnPos, Quaternion.identity);

        // Get the PlantGrowth script, set its tile reference, and register it
        currentPlant = plantObject.GetComponent<PlantGrowth>();
        if (currentPlant != null)
        {
            currentPlant.SetTile(this);
            
            // Register the new plant with the global manager
            if (PlantManager.Instance != null)
            {
                PlantManager.Instance.RegisterPlant(currentPlant);
            }
        }
        else
        {
            Debug.LogError("[TILE] Plant prefab is missing PlantGrowth script!");
        }

        isOccupied = true;
        return true;
    }


    /// <summary>
    /// Attempts to harvest the plant on the tile.
    /// </summary>
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

    /// <summary>
    /// Clears the tile state after a plant is harvested or destroyed.
    /// </summary>
    public void ClearTile()
    {
        isOccupied = false;
        currentPlant = null;
    }
}