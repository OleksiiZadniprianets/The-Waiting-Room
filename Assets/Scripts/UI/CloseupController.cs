using UnityEngine;
using UnityEngine.UI;

public class CloseupController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject drawerCloseupPanel;
    [SerializeField] private GameObject drawerInteriorPanel;
    [SerializeField] private GameObject drawerInteriorHotspot;

    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image drawerImage;
    [SerializeField] private GameObject drawerOpenHotspot;

    [Header("Drawer Sprites")]
    [SerializeField] private Sprite drawerClosedSprite;
    [SerializeField] private Sprite drawerOpenSprite;

    [Header("Room Controls")]
    [SerializeField] private MouseZoomController zoomController;

    private bool isDrawerOpen;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        if (drawerInteriorPanel != null)
        {
            drawerInteriorPanel.SetActive(false);
        }
    }

    public void ShowDrawerCloseup()
    {
        closeupLayer.SetActive(true);

        if (drawerCloseupPanel != null)
        {
            drawerCloseupPanel.SetActive(true);
        }

        if (drawerInteriorPanel != null)
        {
            drawerInteriorPanel.SetActive(false);
        }

        drawerImage.sprite = isDrawerOpen
            ? drawerOpenSprite
            : drawerClosedSprite;

        if (drawerOpenHotspot != null)
        {
            drawerOpenHotspot.SetActive(!isDrawerOpen);
        }

        if (drawerInteriorHotspot != null)
        {
            drawerInteriorHotspot.SetActive(isDrawerOpen);
        }

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

    public void OpenDrawer()
    {
        if (isDrawerOpen)
        {
            return;
        }

        isDrawerOpen = true;
        drawerImage.sprite = drawerOpenSprite;

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

        if (drawerCloseupPanel != null)
        {
            drawerCloseupPanel.SetActive(false);
        }

        if (drawerInteriorPanel != null)
        {
            drawerInteriorPanel.SetActive(true);
        }
    }
    public void BackToDrawerCloseup()
    {
        if (drawerInteriorPanel != null)
        {
            drawerInteriorPanel.SetActive(false);
        }

        if (drawerCloseupPanel != null)
        {
            drawerCloseupPanel.SetActive(true);
        }
    }
}