using UnityEngine;
using UnityEngine.UI;

public class VentPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image ventImage;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    [Header("Puzzle")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject ventUseHotspot;
    [SerializeField] private GameObject redFilterObject;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private bool isOpen;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        if (redFilterObject != null)
        {
            redFilterObject.SetActive(false);
        }

        UpdateVisual();
    }

    public void ShowCloseup()
    {
        if (closeupLayer == null)
        {
            return;
        }

        closeupLayer.SetActive(true);

        UpdateVisual();

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseCloseup()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryOpenVent()
    {
        if (isOpen)
        {
            return;
        }

        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("metal_token"))
        {
            return;
        }

        isOpen = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        if (redFilterObject != null)
        {
            redFilterObject.SetActive(true);
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (ventImage != null)
        {
            ventImage.sprite = isOpen
                ? openSprite
                : closedSprite;
        }

        if (ventUseHotspot != null)
        {
            ventUseHotspot.SetActive(!isOpen);
        }
    }
}