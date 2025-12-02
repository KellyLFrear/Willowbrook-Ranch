using UnityEngine;
[CreateAssetMenu(fileName = "NewPlantData", menuName = "Farming/Plant Data")]
public class PlantData : ScriptableObject
{
    public GameObject sproutStagePrefab;//stage 1
    public GameObject growingStagePrefab;//stage 2
    public GameObject matureStagePrefab;//stage 3
    public GameObject deadStagePrefab;//stage 0
    public GameObject typeOfSeed;
    public int daysToGrow; //can be changed in inspector
}
