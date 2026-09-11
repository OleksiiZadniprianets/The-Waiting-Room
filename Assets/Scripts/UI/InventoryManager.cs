using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventorySlot[] slots;
    [Header("Combinations")]
    [SerializeField] private InventoryCombinationRecipe[] combinationRecipes;
    private InventorySlot selectedSlot;

    private void Awake()
    {
        if (gameState == null)
        {
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot != null)
            {
                slot.Initialize(this);
            }
        }
    }

    public bool AddItem(InventoryItemData item)
    {
        if (item == null)
        {
            return false;
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot != null && slot.IsEmpty)
            {
                slot.SetItem(item);
                return true;
            }
        }
        return false;
    }

    public void SelectSlot(InventorySlot slot)
    {
        if (slot == null || slot.IsEmpty)
        {
            return;
        }

        if (selectedSlot == slot)
        {
            ClearSelection();
            return;
        }

        if (selectedSlot != null)
        {
            InventoryCombinationRecipe recipe =
                FindCombination(
                    selectedSlot.CurrentItem,
                    slot.CurrentItem
                );

            if (recipe != null)
            {
                CombineSlots(selectedSlot, slot, recipe);
                return;
            }
        }

        ClearSlotHighlights();

        selectedSlot = slot;
        selectedSlot.SetSelected(true);

        if (gameState != null)
        {
            gameState.SelectItem(selectedSlot.CurrentItem);
        }
    }

    public void ClearSelection()
    {
        ClearSlotHighlights();
        selectedSlot = null;

        if (gameState != null)
        {
            gameState.ClearSelectedItem();
        }
    }

    public bool RemoveSelectedItem()
    {
        if (selectedSlot == null)
        {
            return false;
        }

        selectedSlot.ClearSlot();
        selectedSlot = null;

        if (gameState != null)
        {
            gameState.ClearSelectedItem();
        }

        return true;
    }

    private void ClearSlotHighlights()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != null)
            {
                slot.SetSelected(false);
            }
        }
    }
    private InventoryCombinationRecipe FindCombination(
    InventoryItemData first,
    InventoryItemData second)
    {
        if (combinationRecipes == null)
        {
            return null;
        }

        foreach (InventoryCombinationRecipe recipe in combinationRecipes)
        {
            if (recipe != null && recipe.Matches(first, second))
            {
                return recipe;
            }
        }

        return null;
    }

    private void CombineSlots(
        InventorySlot firstSlot,
        InventorySlot secondSlot,
        InventoryCombinationRecipe recipe)
    {
        if (firstSlot == null ||
            secondSlot == null ||
            recipe == null ||
            recipe.ResultItem == null)
        {
            return;
        }

        firstSlot.ClearSlot();
        secondSlot.ClearSlot();

        firstSlot.SetItem(recipe.ResultItem);

        selectedSlot = null;

        if (gameState != null)
        {
            gameState.ClearSelectedItem();
        }

        ClearSlotHighlights();
    }
}