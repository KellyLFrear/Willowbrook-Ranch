using System.Collections.Generic;
using UnityEngine;


public class PlantManager : MonoBehaviour
{
    // Instance type changed to PlantManager
    public static PlantManager Instance;

    private List<PlantGrowth> allPlants = new List<PlantGrowth>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterPlant(PlantGrowth plant)
    {
        if (!allPlants.Contains(plant)) allPlants.Add(plant);
    }

    public void UnregisterPlant(PlantGrowth plant)
    {
        if (allPlants.Contains(plant)) allPlants.Remove(plant);
    }

    // 4. The Update() function with the 'T' key is GONE.
    // This function will now be called by your Day/Night script.
    public void AdvanceDay()
    {
        Debug.Log("NEW DAY!!! Checking plant growth!");

        List<PlantGrowth> plantsToGrow = new List<PlantGrowth>(allPlants);

        foreach (PlantGrowth plant in plantsToGrow)
        {
            if (plant != null)
            {
                plant.CheckGrowthDaily();
            }
        }
    }
    //for testing next day, NEEDS TO BE REMOVED AFTER ALL TESTING
    void Update()
    {
        // skips day when pressing N
        if (Input.GetKeyDown(KeyCode.N))
        {
            AdvanceDay();
        }
    }
}