using UnityEngine;
using UnityEngine.UI;

public class BlueprintPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image blueprintImage;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite filteredSprite;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private MouseZoomController zoomController;

    private bool isFiltered;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        ResetBlueprint();
    }

    public void ShowCloseup()
    {
        if (closeupLayer == null)
        {
            return;
        }

        ResetBlueprint();

        closeupLayer.SetActive(true);

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

        ResetBlueprint();

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryUseRedFilter()
    {
        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("red_filter"))
        {
            return;
        }

        if (isFiltered)
        {
            return;
        }

        isFiltered = true;

        UpdateVisual();
    }

    private void ResetBlueprint()
    {
        isFiltered = false;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (blueprintImage == null)
        {
            return;
        }

        blueprintImage.sprite =
            isFiltered
                ? filteredSprite
                : normalSprite;
    }
}