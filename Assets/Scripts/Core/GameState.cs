using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public InventoryItemData SelectedItem { get; private set; }

    public event Action<InventoryItemData>
        SelectedItemChanged;

    public void SelectItem(
        InventoryItemData item)
    {
        SelectedItem = item;

        SelectedItemChanged?.Invoke(
            SelectedItem);
    }

    public void ClearSelectedItem()
    {
        SelectedItem = null;

        SelectedItemChanged?.Invoke(null);
    }

    public bool IsItemSelected(
        string itemId)
    {
        return SelectedItem != null &&
               SelectedItem.ItemId == itemId;
    }
}