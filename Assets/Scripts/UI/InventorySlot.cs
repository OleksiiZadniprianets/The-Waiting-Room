using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button slotButton;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image selectionHighlight;

    private InventoryManager inventoryManager;
    private InventoryItemData currentItem;

    public bool IsEmpty => currentItem == null;
    public InventoryItemData CurrentItem => currentItem;

    public void Initialize(InventoryManager manager)
    {
        inventoryManager = manager;

        if (slotButton == null)
        {
            return;
        }

        slotButton.onClick.RemoveListener(OnSlotClicked);
        slotButton.onClick.AddListener(OnSlotClicked);

        ClearSlot();
    }

    public void SetItem(InventoryItemData item)
    {
        if (item == null)
        {
            return;
        }

        currentItem = item;

        if (itemIcon != null)
        {
            itemIcon.sprite = item.Icon;
            itemIcon.enabled = true;
        }

        SetSelected(false);
    }

    public void ClearSlot()
    {
        currentItem = null;

        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }

        SetSelected(false);
    }

    public void SetSelected(bool isSelected)
    {
        if (selectionHighlight != null)
        {
            selectionHighlight.enabled = isSelected;
        }
    }

    private void OnSlotClicked()
    {
        if (inventoryManager == null || currentItem == null)
        {
            return;
        }

        inventoryManager.SelectSlot(this);
    }
}