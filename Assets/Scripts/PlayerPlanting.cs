using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [Header("Setup")]
    public GameObject plantPrefab;
    public Camera mainCamera; // Still kept just in case, though we use player position now
    public LayerMask plantableLayer; // IMPORTANT: Assign "PlantableGround" here in Inspector

    [Header("Settings")]
    // How far down we check for a tile. 2.0f is usually enough to reach the ground.
    public float interactionDistance = 2.0f;

    private PlayerAnimation playerAnimation;

    void Start()
    {
        // Safety checks to prevent errors if things aren't assigned
        if (mainCamera == null) mainCamera = Camera.main;

        if (plantPrefab == null)
        {
            Debug.LogError("PlayerPlanting: Missing Plant Prefab!");
            this.enabled = false;
            return;
        }

        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        // Inputs
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryPlant();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TryHarvest();
        }

        // Debug Visualization: Draws a red line in the Scene view showing where the player is checking
        // Origin: Feet + slightly up. Direction: Down.
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * interactionDistance, Color.red);
    }

    public void TryPlant()
    {
        // 1. Create a ray starting at the player's center (slightly up) pointing straight DOWN
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        // 2. Cast the ray. Note: We only hit objects on the 'plantableLayer'
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, plantableLayer))
        {
            // 3. Check if the object we hit has the script
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                // 4. Attempt to plant
                if (tile.TryPlant(plantPrefab, hit.point))
                {
                    Debug.Log($"Success! Planted on {tile.name}");

                    // Trigger animation if it exists
                    if (playerAnimation != null) playerAnimation.TriggerPickingFruit();
                }
                else
                {
                    Debug.Log("Could not plant (Tile is occupied).");
                }
            }
        }
        else
        {
            Debug.Log("TryPlant: No plantable tile found beneath player.");
        }
    }

    public void TryHarvest()
    {
        // 1. Same logic: Cast a ray DOWN from the player
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, plantableLayer))
        {
            PlantableTile tile = hit.collider.GetComponent<PlantableTile>();
            if (tile != null)
            {
                // 2. Attempt to harvest
                if (tile.TryHarvest())
                {
                    Debug.Log($"Harvested from {tile.name}");
                    if (playerAnimation != null) playerAnimation.TriggerHarvesting();
                }
            }
        }
        else
        {
            Debug.Log("TryHarvest: No tile found beneath player.");
        }
    }
}