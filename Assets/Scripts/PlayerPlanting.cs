using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [Header("Setup (Drag These In!)")]
    public GameObject plantPrefab;
    public Camera mainCamera;

    [Header("Settings")]
    public float plantDistance = 15f;

    private PlayerAnimation playerAnimation;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("---!!! PLAYER PLANTING SCRIPT FAILED ---!!! You must assign a Main Camera!");
                this.enabled = false;
                return;
            }
        }

        if (plantPrefab == null)
        {
            Debug.LogError("---!!! PLAYER PLANTING SCRIPT FAILED ---!!! You must assign a Plant Prefab!");
            this.enabled = false;
            return;
        }

        playerAnimation = GetComponent<PlayerAnimation>();
        Debug.Log("PlayerPlanting.cs is running");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPlant();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TryHarvest();
        }

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * plantDistance, Color.cyan);
    }

    public void TryPlant()
    {
        Debug.Log("'P' key pressed. Trying to plant...");

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, plantDistance))
        {
            Debug.Log($"Raycast HIT: {hit.collider.name} at {hit.point}");

            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                Debug.Log($"Found PlantableTile on: {tile.name}");
                if (tile.TryPlant(plantPrefab, hit.point))
                {
                    Debug.Log(" Planting successful!");
                    playerAnimation?.TriggerPickingFruit();
                }
                else
                {
                    Debug.Log("Planting failed (tile occupied?)");
                }
            }
            else
            {
                Debug.Log("Ray hit something without a PlantableTile component.");
            }
        }
        else
        {
            Debug.Log("Raycast hit NOTHING.");
        }
    }

    public void TryHarvest()
    {
        Debug.Log("'H' key pressed. Trying to harvest...");

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, plantDistance))
        {
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                if (tile.TryHarvest())
                {
                    playerAnimation?.TriggerHarvesting();
                }
            }
        }
    }
}
