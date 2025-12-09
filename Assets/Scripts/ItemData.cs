using UnityEngine;

public enum ItemCategory
{
    Tool,
    Seed,
    Resource,
    Other
}

[CreateAssetMenu(menuName = "FarmingGame/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Sprite icon;
    public ItemCategory category;
    public int maxStack = 99;

    [Header("Seed Data")]
    public GameObject plantPrefab; // Prefab to spawn when planting this seed
    public ItemData cropResultItem; // ItemData for the crop this seed grows into
}
