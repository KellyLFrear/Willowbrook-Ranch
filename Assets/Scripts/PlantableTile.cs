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
            Debug.Log($"[TILE] {name} is already occupied.");
            return false;
        }

        if (plantPrefab == null)
        {
            Debug.LogError("[TILE] No plant prefab assigned!");
            return false;
        }

        // The tile's world position
        Vector3 spawnPos = transform.position + Vector3.up * 0.1f;

        // DEBUG INFO — this is the important part
        Debug.Log(
            $"[TILE] {name}\n" +
            $" - Tile Position:      {transform.position}\n" +
            $" - Raycast Hit Point:  {hitPoint}\n" +
            $" - Calculated Spawn:   {spawnPos}"
        );

        // Instantiate plant
        GameObject plantObject = Instantiate(plantPrefab, spawnPos, Quaternion.identity);

        // Debug the actual spawned position
        Debug.Log($"[PLANT] Spawned plant object at: {plantObject.transform.position}");

        // Store reference + assign tile
        currentPlant = plantObject.GetComponent<PlantGrowth>();
        if (currentPlant != null)
        {
            currentPlant.SetTile(this);
        }
        else
        {
            Debug.LogError("[TILE] Plant prefab is missing PlantGrowth script!");
        }

        isOccupied = true;
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