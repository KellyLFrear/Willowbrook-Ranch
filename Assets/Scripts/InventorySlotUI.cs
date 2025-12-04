using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text amountText;

    [HideInInspector] public int slotIndex;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    public void Refresh()
    {
        var inv = InventoryManager.Instance;
        var slot = inv.GetSlot(slotIndex);

        if (slot == null || slot.IsEmpty)
        {
            iconImage.enabled = false;
            amountText.text = "";
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;

            // Only show number if stacked
            amountText.text = (slot.item.maxStack > 1 && slot.amount > 1)
                ? slot.amount.ToString()
                : "";
        }
    }

    public void OnClick()
    {
        // Optional: we can add equip/drag/drop logic here later
        Debug.Log($"Clicked slot {slotIndex}");
    }
}
