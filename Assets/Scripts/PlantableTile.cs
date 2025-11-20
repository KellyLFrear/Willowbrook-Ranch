using UnityEngine;

public class PlantableTile : MonoBehaviour
{
    public bool isOccupied = false;
    private PlantGrowth currentPlant = null;

    [Header("Watering Settings")]
    public bool isWatered = false;
    public MeshRenderer tileRenderer;
    public Color dryColor = new Color(1f, 1f, 1f);
    public Color wetColor = new Color(0.6f, 0.6f, 0.6f);

    void Start()
    {
        if (tileRenderer == null) tileRenderer = GetComponent<MeshRenderer>();
        UpdateColor();
    } 
    public void WaterTile()
    {
        if (isWatered) return;
        isWatered = true;
        UpdateColor();
    }

    public void DryTile()
    {
        isWatered = false;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (tileRenderer != null)
        {
            tileRenderer.material.color = isWatered ? wetColor : dryColor;
        }
    }

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

        // Spawn slightly above the tile
        Vector3 spawnPos = hitPoint + Vector3.up * 0.1f;
        GameObject plantObject = Instantiate(plantPrefab, spawnPos, Quaternion.identity);

        currentPlant = plantObject.GetComponent<PlantGrowth>();
        if (currentPlant != null)
        {
            currentPlant.SetTile(this);
        }

        isOccupied = true;
        return true;
    }

    public bool TryHarvest()
    {
        if (!isOccupied || currentPlant == null) return false;
        if (!currentPlant.IsMature()) return false;

        currentPlant.Harvest();
        return true;
    }

    public void ClearTile()
    {
        isOccupied = false;
        currentPlant = null;
    }
} 