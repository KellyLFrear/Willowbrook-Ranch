using System.Collections.Generic;
using UnityEngine;


public class PlantManager : MonoBehaviour
{
    // Singleton Instance
    public static PlantManager Instance;

    private List<PlantGrowth> allPlants = new List<PlantGrowth>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlant(PlantGrowth plant)
    {
        if (!allPlants.Contains(plant))
        {
            allPlants.Add(plant);
        }
    }

    public void UnregisterPlant(PlantGrowth plant)
    {
        // IMPORTANT: Plants that die or are harvested must unregister themselves.
        if (allPlants.Contains(plant))
        {
            allPlants.Remove(plant);
        }
    }

    /// <summary>
    /// Global day advance function. Must be called by your day/night system.
    /// This is where growth and death conditions are evaluated for all plants.
    /// </summary>
    public void AdvanceDay()
    {
        Debug.Log("--- NEW DAY --- Advancing plant growth and checking for death!");

        // Create a temporary list to iterate over (important in case a plant removes itself)
        List<PlantGrowth> plantsToAdvance = new List<PlantGrowth>(allPlants);

        foreach (PlantGrowth plant in plantsToAdvance)
        {
            // Check for null in case a plant was destroyed during an earlier iteration
            if (plant != null)
            {
                // Calls the plant's daily growth/death logic
                plant.AdvanceDay(); 
            }
        }
    }
}