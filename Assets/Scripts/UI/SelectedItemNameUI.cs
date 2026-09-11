using TMPro;
using UnityEngine;

public class SelectedItemNameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private TMP_Text itemNameText;

    private void Start()
    {
        if (namePanel != null)
        {
            namePanel.SetActive(false);
        }

        UpdateItemName(
            gameState != null
                ? gameState.SelectedItem
                : null);
    }

    private void OnEnable()
    {
        if (gameState != null)
        {
            gameState.SelectedItemChanged +=
                UpdateItemName;
        }
    }

    private void OnDisable()
    {
        if (gameState != null)
        {
            gameState.SelectedItemChanged -=
                UpdateItemName;
        }
    }

    private void UpdateItemName(
        InventoryItemData item)
    {
        if (namePanel == null ||
            itemNameText == null)
        {
            return;
        }

        if (item == null)
        {
            namePanel.SetActive(false);
            return;
        }

        itemNameText.text = item.DisplayName;
        namePanel.SetActive(true);
    }
}