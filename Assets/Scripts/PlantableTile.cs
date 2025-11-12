using UnityEngine;

public class PlantableTile : MonoBehaviour
{
    public bool isOccupied = false;
    private PlantGrowth currentPlant = null;

    // Updated TryPlant — now takes the hit position
    public bool TryPlant(GameObject plantPrefab, Vector3 hitPoint)
    {
        if (isOccupied)
        {
            Debug.Log($"Tile {name} is already occupied.");
            return false;
        }

        if (plantPrefab == null)
        {
            Debug.LogError("No plantPrefab provided!");
            return false;
        }

        // Debug info
        Debug.Log($"Planting on tile '{name}' at tilePos={transform.position}, hitPos={hitPoint}");

        // Spawn at hit position (slightly above ground)
        Vector3 spawnPos = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        GameObject plantObject = Instantiate(plantPrefab, spawnPos, Quaternion.identity);

        currentPlant = plantObject.GetComponent<PlantGrowth>();
        if (currentPlant != null)
        {
            currentPlant.SetTile(this);
        }
        else
        {
            Debug.LogError("The plant prefab has no PlantGrowth script attached!");
        }

        isOccupied = true;
        Debug.Log($"Plant successfully placed on {name} at {spawnPos}");
        return true;
    }

    public bool TryHarvest()
    {
        if (!isOccupied || currentPlant == null)
        {
            Debug.Log($"Nothing to harvest on {name}");
            return false;
        }

        if (!currentPlant.IsMature())
        {
            Debug.Log($"Plant on {name} not mature yet.");
            return false;
        }

        currentPlant.Harvest();
        return true;
    }

    public void ClearTile()
    {
        isOccupied = false;
        currentPlant = null;
    }
}
