using UnityEngine;
using UnityEngine.UI;

public class FolderPuzzleController : MonoBehaviour
{
    [Header("Folder Image")]
    [SerializeField] private Image folderImage;
    [SerializeField] private RectTransform folderRect;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite page1Sprite;
    [SerializeField] private Sprite page1FilteredSprite;
    [SerializeField] private Sprite page2Sprite;
    [SerializeField] private Sprite page2FilteredSprite;

    [Header("Closed Transform")]
    [SerializeField] private Vector2 closedSize;
    [SerializeField] private Vector2 closedPosition;

    [Header("Open Transform")]
    [SerializeField] private Vector2 openSize;
    [SerializeField] private Vector2 openPosition;

    [Header("Game")]
    [SerializeField] private GameState gameState;

    private bool isFolderOpen;
    private bool isFilterActive;
    private int currentPage = 1;

    private void OnEnable()
    {
        ResetFolder();
    }

    public void HandleFolderClick()
    {
        if (!isFolderOpen)
        {
            OpenFolder();
            return;
        }

        if (gameState != null &&
            gameState.IsItemSelected("red_filter"))
        {
            ApplyRedFilter();
            return;
        }

        FlipPage();
    }

    private void OpenFolder()
    {
        isFolderOpen = true;
        currentPage = 1;
        isFilterActive = false;

        UpdateVisual();
    }

    private void FlipPage()
    {
        if (!isFolderOpen)
        {
            return;
        }

        currentPage = currentPage == 1 ? 2 : 1;
        isFilterActive = false;

        UpdateVisual();
    }

    private void ApplyRedFilter()
    {
        if (!isFolderOpen)
        {
            return;
        }

        isFilterActive = true;

        UpdateVisual();
    }

    private void ResetFolder()
    {
        isFolderOpen = false;
        currentPage = 1;
        isFilterActive = false;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (folderImage == null)
        {
            return;
        }

        if (!isFolderOpen)
        {
            folderImage.sprite = closedSprite;
            ApplyClosedTransform();
            return;
        }

        ApplyOpenTransform();

        if (currentPage == 1)
        {
            folderImage.sprite = isFilterActive
                ? page1FilteredSprite
                : page1Sprite;

            return;
        }

        folderImage.sprite = isFilterActive
            ? page2FilteredSprite
            : page2Sprite;
    }

    private void ApplyClosedTransform()
    {
        if (folderRect == null)
        {
            return;
        }

        folderRect.sizeDelta = closedSize;
        folderRect.anchoredPosition = closedPosition;
    }

    private void ApplyOpenTransform()
    {
        if (folderRect == null)
        {
            return;
        }

        folderRect.sizeDelta = openSize;
        folderRect.anchoredPosition = openPosition;
    }
}