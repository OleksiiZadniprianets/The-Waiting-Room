using UnityEngine;

[CreateAssetMenu(
    fileName = "NewCombinationRecipe",
    menuName = "Escape Room/Inventory Combination Recipe")]
public class InventoryCombinationRecipe : ScriptableObject
{
    [SerializeField] private InventoryItemData firstItem;
    [SerializeField] private InventoryItemData secondItem;
    [SerializeField] private InventoryItemData resultItem;

    public InventoryItemData ResultItem => resultItem;

    public bool Matches(InventoryItemData a, InventoryItemData b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        return
            (a == firstItem && b == secondItem) ||
            (a == secondItem && b == firstItem);
    }
}