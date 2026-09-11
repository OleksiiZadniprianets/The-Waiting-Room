using UnityEngine;
using UnityEngine.UI;

public class ControlPanelPuzzleController : MonoBehaviour
{
    [Header("Control Panel")]
    [SerializeField] private GameObject controlPanelCloseupLayer;
    [SerializeField] private GameObject panelOffImage;
    [SerializeField] private GameObject panelOnImage;

    [Header("Monitor")]
    [SerializeField] private GameObject monitorCloseupLayer;
    [SerializeField] private Image monitorImage;
    [SerializeField] private Sprite monitorGridSprite;
    [SerializeField] private Sprite monitorDecodedSprite;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private bool isPowered;
    private bool isDecoderActive;

    public bool IsPowered => isPowered;

    private void Start()
    {
        if (controlPanelCloseupLayer != null)
        {
            controlPanelCloseupLayer.SetActive(false);
        }

        if (monitorCloseupLayer != null)
        {
            monitorCloseupLayer.SetActive(false);
        }

        ResetMonitor();
        UpdatePanelVisual();
    }

    public void ShowControlPanel()
    {
        if (controlPanelCloseupLayer == null)
        {

            return;
        }

        controlPanelCloseupLayer.SetActive(true);

        UpdatePanelVisual();

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseControlPanel()
    {
        if (monitorCloseupLayer != null)
        {
            monitorCloseupLayer.SetActive(false);
        }

        ResetMonitor();

        if (controlPanelCloseupLayer != null)
        {
            controlPanelCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryInsertPowerCell()
    {
        if (isPowered)
        {
            return;
        }

        if (gameState == null)
        {

            return;
        }

        if (!gameState.IsItemSelected("power_cell"))
        {
            return;
        }

        isPowered = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        UpdatePanelVisual();
    }

    public void ShowMonitor()
    {
        if (!isPowered)
        {
            return;
        }

        ResetMonitor();

        if (monitorCloseupLayer != null)
        {
            monitorCloseupLayer.SetActive(true);
        }
    }

    public void TryUseDecoderCard()
    {
        if (isDecoderActive)
        {
            return;
        }

        if (gameState == null)
        {

            return;
        }

        if (!gameState.IsItemSelected("decoder_card"))
        {
            return;
        }

        isDecoderActive = true;

        if (monitorImage != null &&
            monitorDecodedSprite != null)
        {
            monitorImage.sprite = monitorDecodedSprite;
        }
    }

    public void CloseMonitor()
    {
        ResetMonitor();

        if (monitorCloseupLayer != null)
        {
            monitorCloseupLayer.SetActive(false);
        }
    }

    private void ResetMonitor()
    {
        isDecoderActive = false;

        if (monitorImage != null &&
            monitorGridSprite != null)
        {
            monitorImage.sprite = monitorGridSprite;
        }
    }

    private void UpdatePanelVisual()
    {
        if (panelOffImage != null)
        {
            panelOffImage.SetActive(!isPowered);
        }

        if (panelOnImage != null)
        {
            panelOnImage.SetActive(isPowered);
        }
    }
}