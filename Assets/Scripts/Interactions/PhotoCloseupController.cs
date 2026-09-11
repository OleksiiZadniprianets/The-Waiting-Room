using UnityEngine;

public class PhotoCloseupController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject photoCloseupLayer;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private bool openedFromDesk;

    private void Start()
    {
        if (photoCloseupLayer != null)
        {
            photoCloseupLayer.SetActive(false);
        }
    }

    public void ShowPhotoFromRoom()
    {
        openedFromDesk = false;

        OpenPhoto();
    }

    public void ShowPhotoFromDesk()
    {
        openedFromDesk = true;

        OpenPhoto();
    }

    private void OpenPhoto()
    {

        photoCloseupLayer.SetActive(true);

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void ClosePhoto()
    {
        if (photoCloseupLayer != null)
        {
            photoCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = !openedFromDesk;
        }
    }
}