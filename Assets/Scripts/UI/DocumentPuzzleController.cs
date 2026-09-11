using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentPuzzleController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject documentCloseupLayer;
    [SerializeField] private Image documentImage;
    [SerializeField] private MouseZoomController zoomController;
    [SerializeField] private GameState gameState;

    [Header("Message UI")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private float messageDuration = 2f;

    [Header("Document Sprites")]
    [SerializeField] private Sprite[] normalSprites = new Sprite[3];
    [SerializeField] private Sprite[] revealedSprites = new Sprite[3];

    [Header("Puzzle Settings")]
    [SerializeField] private int correctDocumentIndex = 2;

    private int currentDocumentIndex = -1;
    private bool[] isRevealed = new bool[3];
    private Coroutine messageCoroutine;

    public bool HasCodeBeenRevealed =>
        correctDocumentIndex >= 0 &&
        correctDocumentIndex < isRevealed.Length &&
        isRevealed[correctDocumentIndex];

    private void Start()
    {
        if (documentCloseupLayer != null)
        {
            documentCloseupLayer.SetActive(false);
        }
    }

    public void ShowDocument(int index)
    {
        if (!IsValidIndex(index))
        {
            return;
        }

        currentDocumentIndex = index;
        UpdateCurrentDocumentVisual();

        if (documentCloseupLayer != null)
        {
            documentCloseupLayer.SetActive(true);
        }

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseDocument()
    {
        if (documentCloseupLayer != null)
        {
            documentCloseupLayer.SetActive(false);
        }

        currentDocumentIndex = -1;

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryUseBottleOnCurrentDocument()
    {
        if (!IsValidIndex(currentDocumentIndex))
        {
            return;
        }

        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("bottle"))
        {
            ShowMessage("Потрібно вибрати пляшку.");
            return;
        }

        if (isRevealed[currentDocumentIndex])
        {
            if (currentDocumentIndex == correctDocumentIndex)
            {
                ShowMessage("Код уже видно.");
            }
            else
            {
                ShowMessage("На цьому листку більше нічого немає.");
            }

            return;
        }

        isRevealed[currentDocumentIndex] = true;
        UpdateCurrentDocumentVisual();

        if (currentDocumentIndex == correctDocumentIndex)
        {
            ShowMessage("На папері з'явився код.");
        }
        else
        {
            ShowMessage("Папір намок, але нічого не з'явилося.");
        }
    }

    private void UpdateCurrentDocumentVisual()
    {
        if (documentImage == null)
        {
            return;
        }

        if (isRevealed[currentDocumentIndex])
        {
            documentImage.sprite = revealedSprites[currentDocumentIndex];
        }
        else
        {
            documentImage.sprite = normalSprites[currentDocumentIndex];
        }
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 &&
               index < normalSprites.Length &&
               index < revealedSprites.Length;
    }

    private void ShowMessage(string message)
    {
        if (messagePanel == null || messageText == null)
        {
            return;
        }

        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(ShowMessageRoutine(message));
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        messageText.text = message;
        messagePanel.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        messagePanel.SetActive(false);
        messageCoroutine = null;
    }
}