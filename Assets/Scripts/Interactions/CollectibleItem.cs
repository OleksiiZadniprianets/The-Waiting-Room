using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private InventoryItemData itemData;

    [Header("References")]
    [SerializeField] private InventoryManager inventoryManager;

    private bool isCollected;

    public void Collect()
    {
        if (isCollected)
        {
            return;
        }

        if (itemData == null)
        {
            return;
        }

        if (inventoryManager == null)
        {
            return;
        }

        bool wasAdded = inventoryManager.AddItem(itemData);

        if (!wasAdded)
        {
            return;
        }

        isCollected = true;
        gameObject.SetActive(false);
    }
}