using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;   // InventoryPanel object
    public Transform slotsParent;       // SlotsParent under panel
    public InventorySlotUI slotPrefab;  // SlotUI (or InventorySlotUI) prefab

    private InventorySlotUI[] slotUIs;
    private bool isOpen;

    private void Start()
    {
        var inv = InventoryManager.Instance;
        int count = inv.totalSlots;

        slotUIs = new InventorySlotUI[count];

        // Create a SlotUI for each inventory slot
        for (int i = 0; i < count; i++)
        {
            InventorySlotUI ui = Instantiate(slotPrefab, slotsParent);
            ui.SetSlotIndex(i);
            ui.Refresh();
            slotUIs[i] = ui;
        }

        inv.OnInventoryChanged += RefreshAll;

        // Hide panel at runtime start
        inventoryPanel.SetActive(false);
        isOpen = false;
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= RefreshAll;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            if (isOpen)
                RefreshAll();
        }
    }

    private void RefreshAll()
    {
        foreach (var ui in slotUIs)
            ui.Refresh();
    }
}
