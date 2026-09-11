using UnityEngine;
using UnityEngine.UI;

public class PaperClueController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image paperImage;

    [Header("Paper Sprites")]
    [SerializeField] private Sprite instructionPaperSprite;
    [SerializeField] private Sprite countingPaperSprite;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }
    }

    public void OpenInstructionPaper()
    {
        OpenPaper(instructionPaperSprite);
    }

    public void OpenCountingPaper()
    {
        OpenPaper(countingPaperSprite);
    }

    public void ClosePaper()
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

    private void OpenPaper(Sprite sprite)
    {
        if (closeupLayer == null)
        {

            return;
        }

        if (paperImage == null)
        {

            return;
        }

        paperImage.sprite = sprite;

        closeupLayer.SetActive(true);

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }
}