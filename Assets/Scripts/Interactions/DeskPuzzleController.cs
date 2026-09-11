using UnityEngine;
using UnityEngine.UI;

public class DeskPuzzleController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private GameObject deskCloseupPanel;
    [SerializeField] private GameObject deskInteriorPanel;

    [Header("Desk")]
    [SerializeField] private Image deskImage;
    [SerializeField] private Sprite deskClosedSprite;
    [SerializeField] private Sprite deskOpenSprite;

    [Header("Hotspots")]
    [SerializeField] private GameObject drawerOpenHotspot;
    [SerializeField] private GameObject drawerInteriorHotspot;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private bool isDrawerOpen;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }
    }

    public void ShowDeskCloseup()
    {
        if (closeupLayer == null)
        {
            return;
        }

        closeupLayer.SetActive(true);

        ShowDeskPanel();

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void OpenDrawer()
    {
        if (isDrawerOpen)
        {
            return;
        }

        isDrawerOpen = true;

        if (deskImage != null && deskOpenSprite != null)
        {
            deskImage.sprite = deskOpenSprite;
        }

        if (drawerOpenHotspot != null)
        {
            drawerOpenHotspot.SetActive(false);
        }

        if (drawerInteriorHotspot != null)
        {
            drawerInteriorHotspot.SetActive(true);
        }
    }

    public void OpenDrawerInterior()
    {
        if (!isDrawerOpen)
        {
            return;
        }

        if (deskCloseupPanel != null)
        {
            deskCloseupPanel.SetActive(false);
        }

        if (deskInteriorPanel != null)
        {
            deskInteriorPanel.SetActive(true);
        }
    }

    public void Back()
    {
        if (deskInteriorPanel != null &&
            deskInteriorPanel.activeSelf)
        {
            ShowDeskPanel();
            return;
        }

        CloseDeskCloseup();
    }

    private void ShowDeskPanel()
    {
        if (deskCloseupPanel != null)
        {
            deskCloseupPanel.SetActive(true);
        }

        if (deskInteriorPanel != null)
        {
            deskInteriorPanel.SetActive(false);
        }

        if (deskImage != null)
        {
            deskImage.sprite = isDrawerOpen
                ? deskOpenSprite
                : deskClosedSprite;
        }

        if (drawerOpenHotspot != null)
        {
            drawerOpenHotspot.SetActive(!isDrawerOpen);
        }

        if (drawerInteriorHotspot != null)
        {
            drawerInteriorHotspot.SetActive(isDrawerOpen);
        }
    }

    private void CloseDeskCloseup()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        if (deskCloseupPanel != null)
        {
            deskCloseupPanel.SetActive(true);
        }

        if (deskInteriorPanel != null)
        {
            deskInteriorPanel.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }
}