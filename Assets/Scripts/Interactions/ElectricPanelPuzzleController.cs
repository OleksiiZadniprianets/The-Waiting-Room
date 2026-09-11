using UnityEngine;
using UnityEngine.UI;

public class ElectricPanelPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image panelImage;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite noFuseSprite;
    [SerializeField] private Sprite poweredSprite;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private MouseZoomController zoomController;

    private bool isPanelOpen;
    private bool isPowered;

    public bool IsPowered => isPowered;

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

    public void TryInteract()
    {
        if (gameState == null)
        {
            return;
        }

        if (isPowered)
        {
            return;
        }

        if (!isPanelOpen)
        {
            TryOpenWithScrewdriver();
            return;
        }

        TryInstallFuse();
    }

    private void TryOpenWithScrewdriver()
    {
        if (!gameState.IsItemSelected("screwdriver"))
        {
            return;
        }

        isPanelOpen = true;

        UpdateVisual();
    }

    private void TryInstallFuse()
    {
        if (!gameState.IsItemSelected("fuse"))
        {
            return;
        }

        isPowered = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (panelImage == null)
        {
            return;
        }

        if (isPowered)
        {
            panelImage.sprite = poweredSprite;
            return;
        }

        panelImage.sprite = isPanelOpen
            ? noFuseSprite
            : closedSprite;
    }
}