using UnityEngine;

public class FolderShelfController : MonoBehaviour
{
    [Header("Shelf")]
    [SerializeField] private GameObject shelfCloseupLayer;

    [Header("Folder")]
    [SerializeField] private GameObject folderCloseupLayer;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private void Start()
    {
        if (shelfCloseupLayer != null)
        {
            shelfCloseupLayer.SetActive(false);
        }

        if (folderCloseupLayer != null)
        {
            folderCloseupLayer.SetActive(false);
        }
    }

    public void OpenShelf()
    {
        if (shelfCloseupLayer == null)
        {
            return;
        }

        shelfCloseupLayer.SetActive(true);

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseShelf()
    {
        if (folderCloseupLayer != null)
        {
            folderCloseupLayer.SetActive(false);
        }

        if (shelfCloseupLayer != null)
        {
            shelfCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void OpenFolder()
    {
        if (folderCloseupLayer == null)
        {
            return;
        }

        folderCloseupLayer.SetActive(true);
    }

    public void CloseFolder()
    {
        if (folderCloseupLayer != null)
        {
            folderCloseupLayer.SetActive(false);
        }
    }
}