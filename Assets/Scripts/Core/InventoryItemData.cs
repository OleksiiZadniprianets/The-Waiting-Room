using UnityEngine;

[CreateAssetMenu(
    fileName = "NewInventoryItem",
    menuName = "Escape Room/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private string itemId;
    [SerializeField] private string displayName;

    [Header("Visual")]
    [SerializeField] private Sprite icon;

    public string ItemId => itemId;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}