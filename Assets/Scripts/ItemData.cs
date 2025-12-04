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
}
