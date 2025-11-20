using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public GameObject stage1_Sprout; //sprout stage
    public GameObject stage2_Growing; //growing stage
    public GameObject stage3_Mature; //mature stage
    public GameObject stage_Dead;//dead stage (for when player forgets to water)
    private int currentStage = 1; //sets the current stage to sprouting stage
    private bool isDead = false; //plant is not dead

    // A reference back to the tile we are planted on
    private PlantableTile myTile;

    void Start()
    {
        if (PlantManager.Instance != null) { PlantManager.Instance.RegisterPlant(this); } //confirms the manager exists
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

 

    //Function to check growth which will bec called by the manager
    public void CheckGrowthDaily() {
        if (isDead) return;

        if (myTile != null) {
            if (myTile.isWatered)
            {
                Grow(); //grows the plant
                myTile.DryTile(); //dry out the soil for the next day
            }
            else { Die(); }//dies if not watered 
        }
    }

    public void Grow()
    {
        if (currentStage >= 3)
        {
            return;
        }
        currentStage++;
        ShowStage(currentStage);
        Debug.Log("Plant gre to stage " + currentStage);
    }

    private void Die() { 
        isDead = true;
        Debug.Log("Plant died!!!");

        //disable all the stages
        if(stage1_Sprout) stage1_Sprout.SetActive(false);
        if(stage2_Growing) stage2_Growing.SetActive(false);
        if(stage3_Mature) stage3_Mature.SetActive(false);

        if (stage_Dead)
        {
            stage_Dead.SetActive(true);
        }
        else {
            // Fallback if you don't have a dead model yet: Turn red
            GetComponent<Renderer>().material.color = Color.brown;
        }
    }

    private void ShowStage(int stage)
    {
        // Deactivate all stages first
        if (stage1_Sprout) stage1_Sprout.SetActive(false);
        if (stage2_Growing) stage2_Growing.SetActive(false);
        if (stage3_Mature) stage3_Mature.SetActive(false);
        if (stage_Dead)  stage_Dead.SetActive(false); 

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
        if (isDead)
        {
            Debug.Log("Cleared dead plant.");
            // Logic to clear dead plant (maybe gives 0 crops)
        }
        else if (IsMature())
        {
            Debug.Log("Harvested fresh crops!");
            // Logic to give items
        }
        else
        {
            return; // Not ready
        }

        // Cleanup
        if (myTile != null) myTile.ClearTile();
        if (PlantManager.Instance != null) PlantManager.Instance.UnregisterPlant(this);
        Destroy(gameObject);
    }
}