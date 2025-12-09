using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public InventorySlotUI[] hotbarSlots;  // size 8 in Inspector

    private void Start()
    {
        var inv = InventoryManager.Instance;

        // Link UI slots to inventory indices 0–7
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].SetSlotIndex(i);
            hotbarSlots[i].Refresh();
        }

        inv.OnInventoryChanged += RefreshAll;
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= RefreshAll;
    }

    private void RefreshAll()
    {
        foreach (var ui in hotbarSlots)
            ui.Refresh();
    }
}
