using UnityEngine;

public class InventoryTestAdder : MonoBehaviour
{
    public ItemData testItem;   // drag an item here in Inspector
    public ItemData testItem2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Adding test item...");
            InventoryManager.Instance.AddItem(testItem, 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Adding testitem...");
            InventoryManager.Instance.AddItem(testItem2, 1);
        }
    }
}
