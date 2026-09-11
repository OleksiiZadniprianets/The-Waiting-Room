using UnityEngine;

public class CabinetPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private GameObject closedImage;
    [SerializeField] private GameObject openImage;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private bool isOpen;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
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

    public void TryUnlockCabinet()
    {
        if (isOpen)
        {
            return;
        }

        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("small_key"))
        {
            return;
        }

        isOpen = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (closedImage != null)
        {
            closedImage.SetActive(!isOpen);
        }

        if (openImage != null)
        {
            openImage.SetActive(isOpen);
        }
    }
}