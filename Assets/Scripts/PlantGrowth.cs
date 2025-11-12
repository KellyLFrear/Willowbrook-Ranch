using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject stage1_Sprout;
    public GameObject stage2_Growing;
    public GameObject stage3_Mature;
    private int currentStage = 1;

    // A reference back to the tile we are planted on
    private PlantableTile myTile;

    void Start()
    {
        ShowStage(1);
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

    public void Grow()
    {
        if (currentStage >= 3)
        {
            return;
        }
        currentStage++;
        ShowStage(currentStage);
    }

    private void ShowStage(int stage)
    {
        // Deactivate all stages first
        if (stage1_Sprout) stage1_Sprout.SetActive(false);
        if (stage2_Growing) stage2_Growing.SetActive(false);
        if (stage3_Mature) stage3_Mature.SetActive(false);

        // Activate the correct stage
        switch (stage)
        {
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
        // For now, we only harvest if mature.
        if (!IsMature())
        {
            Debug.Log("This plant isn't mature yet!");
            return;
        }

        Debug.Log("Harvested the plant!");
        // (Here you would add code to give the player items)

        // Tell our tile that it is now empty
        if (myTile != null)
        {
            myTile.ClearTile();
        }

        // Remove this plant from the PlantManager's list
        if (PlantManager.Instance != null)
        {
            PlantManager.Instance.UnregisterPlant(this);
        }

        // Destroy the plant object
        Destroy(gameObject);
    }
}