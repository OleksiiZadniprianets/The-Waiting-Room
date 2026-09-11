using UnityEngine;
using UnityEngine.UI;

public class PaintingPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject paintingCloseupLayer;
    [SerializeField] private Image paintingImage;

    [Header("Painting Sprites")]
    [SerializeField] private Sprite normalPaintingSprite;
    [SerializeField] private Sprite uvPaintingSprite;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private MouseZoomController zoomController;

    private bool isRevealed;

    private void Start()
    {
        if (paintingCloseupLayer != null)
        {
            paintingCloseupLayer.SetActive(false);
        }

        UpdatePaintingVisual();
    }

    public void ShowPaintingCloseup()
    {
        if (paintingCloseupLayer == null)
        {
            return;
        }

        paintingCloseupLayer.SetActive(true);

        UpdatePaintingVisual();

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void ClosePaintingCloseup()
    {
        if (paintingCloseupLayer != null)
        {
            paintingCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryUseUVFlashlight()
    {
        if (isRevealed)
        {
            return;
        }

        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("uv_flashlight_powered"))
        {
            return;
        }

        isRevealed = true;

        UpdatePaintingVisual();
    }

    private void UpdatePaintingVisual()
    {
        if (paintingImage == null)
        {
            return;
        }

        paintingImage.sprite = isRevealed
            ? uvPaintingSprite
            : normalPaintingSprite;
    }
}