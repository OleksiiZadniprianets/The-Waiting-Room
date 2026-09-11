using UnityEngine;
using UnityEngine.UI;

public class BookshelfPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject bookshelfCloseupLayer;
    [SerializeField] private GameObject bookshelfCloseupPanel;
    [SerializeField] private Image bookshelfImage;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    [Header("Puzzle")]
    [SerializeField] private GameObject bookButtonsRoot;
    [SerializeField] private GameObject secretCompartmentItems;

    [Header("Note")]
    [SerializeField] private GameObject symbolNotePanel;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private readonly string[] correctSequence =
    {
        "diamond",
        "crescent",
        "triangle"
    };

    private int currentSequenceIndex;
    private bool isSolved;

    private void Start()
    {
        if (bookshelfCloseupLayer != null)
        {
            bookshelfCloseupLayer.SetActive(false);
        }

        if (symbolNotePanel != null)
        {
            symbolNotePanel.SetActive(false);
        }

        if (secretCompartmentItems != null)
        {
            secretCompartmentItems.SetActive(false);
        }

        UpdateVisual();
    }

    public void ShowBookshelfCloseup()
    {
        if (bookshelfCloseupLayer == null)
        {
            return;
        }

        bookshelfCloseupLayer.SetActive(true);

        if (bookshelfCloseupPanel != null)
        {
            bookshelfCloseupPanel.SetActive(true);
        }

        if (symbolNotePanel != null)
        {
            symbolNotePanel.SetActive(false);
        }

        UpdateVisual();

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void PressBook(string symbol)
    {
        if (isSolved)
        {
            return;
        }

        if (string.IsNullOrEmpty(symbol))
        {
            return;
        }

        if (symbol == correctSequence[currentSequenceIndex])
        {
            currentSequenceIndex++;

            if (currentSequenceIndex >= correctSequence.Length)
            {
                SolvePuzzle();
            }

            return;
        }

        currentSequenceIndex = 0;

        if (symbol == correctSequence[0])
        {
            currentSequenceIndex = 1;
        }
    }

    private void SolvePuzzle()
    {
        isSolved = true;
        currentSequenceIndex = 0;

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (bookshelfImage != null)
        {
            bookshelfImage.sprite =
                isSolved
                    ? openSprite
                    : closedSprite;
        }

        if (bookButtonsRoot != null)
        {
            bookButtonsRoot.SetActive(!isSolved);
        }

        if (secretCompartmentItems != null)
        {
            secretCompartmentItems.SetActive(isSolved);
        }
    }

    public void ShowSymbolNote()
    {
        if (!isSolved)
        {
            return;
        }

        if (bookshelfCloseupPanel != null)
        {
            bookshelfCloseupPanel.SetActive(false);
        }

        if (symbolNotePanel != null)
        {
            symbolNotePanel.SetActive(true);
        }
    }

    public void BackFromSymbolNote()
    {
        if (symbolNotePanel != null)
        {
            symbolNotePanel.SetActive(false);
        }

        if (bookshelfCloseupPanel != null)
        {
            bookshelfCloseupPanel.SetActive(true);
        }
    }

    public void CloseBookshelfCloseup()
    {
        if (symbolNotePanel != null &&
            symbolNotePanel.activeSelf)
        {
            BackFromSymbolNote();
            return;
        }

        if (bookshelfCloseupLayer != null)
        {
            bookshelfCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }
}