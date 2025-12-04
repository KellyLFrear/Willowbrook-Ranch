using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Sizes")]
    public int totalSlots = 24;   // 3 rows × 8 columns
    public int hotbarSize = 8;    // first 8 are hotbar

    [Header("Slots")]
    public List<InventorySlot> slots = new List<InventorySlot>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        // Singleton + persist across scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Init slots list
        if (slots.Count != totalSlots)
        {
            slots = new List<InventorySlot>(totalSlots);
            for (int i = 0; i < totalSlots; i++)
                slots.Add(new InventorySlot());
        }
    }

    private void NotifyChanged() => OnInventoryChanged?.Invoke();

    public InventorySlot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return null;
        return slots[index];
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        if (item == null || amount <= 0) return false;

        // 1) Try stacking into existing stacks
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (!slot.IsEmpty && slot.item == item && slot.amount < item.maxStack)
            {
                int space = item.maxStack - slot.amount;
                int toAdd = Mathf.Min(space, amount);
                slot.amount += toAdd;
                amount -= toAdd;

                if (amount <= 0)
                {
                    NotifyChanged();
                    return true;
                }
            }
        }

        // 2) Put into empty slots
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (slot.IsEmpty)
            {
                int toAdd = Mathf.Min(item.maxStack, amount);
                slot.item = item;
                slot.amount = toAdd;
                amount -= toAdd;

                if (amount <= 0)
                {
                    NotifyChanged();
                    return true;
                }
            }
        }

        NotifyChanged();
        return false; // inventory full
    }

    public void RemoveFromSlot(int index, int amount = 1)
    {
        var slot = GetSlot(index);
        if (slot == null || slot.IsEmpty) return;

        slot.amount -= amount;
        if (slot.amount <= 0) slot.Clear();
        NotifyChanged();
    }
}
