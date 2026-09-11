using UnityEngine;
using UnityEngine.UI;

public class ToolboxPuzzleController : MonoBehaviour
{
    [Header("Toolbox")]
    [SerializeField] private GameObject toolboxCloseupLayer;
    [SerializeField] private GameObject toolboxLockedImage;
    [SerializeField] private GameObject toolboxOpenImage;

    [Header("Lock")]
    [SerializeField] private GameObject lockCloseupLayer;
    [SerializeField] private Image[] digitImages;

    [Header("Digit Sprites")]
    [SerializeField] private Sprite[] digitSprites;

    [Header("Code")]
    [SerializeField] private string correctCode = "1234";

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    [Header("Navigation")]
    [SerializeField] private GameObject toolboxBackButton;

    private int[] currentDigits = { 0, 0, 0, 0 };
    private bool isUnlocked;

    private void Start()
    {
        if (toolboxCloseupLayer != null)
        {
            toolboxCloseupLayer.SetActive(false);
        }

        if (lockCloseupLayer != null)
        {
            lockCloseupLayer.SetActive(false);
        }

        UpdateToolboxVisual();
        UpdateAllDigits();
    }

    public void ShowToolboxCloseup()
    {
        if (toolboxCloseupLayer == null)
        {
            return;
        }

        toolboxCloseupLayer.SetActive(true);

        UpdateToolboxVisual();

        if (toolboxBackButton != null)
        {
            toolboxBackButton.SetActive(true);
        }

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseToolboxCloseup()
    {
        if (lockCloseupLayer != null)
        {
            lockCloseupLayer.SetActive(false);
        }

        if (toolboxCloseupLayer != null)
        {
            toolboxCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void ShowLockCloseup()
    {
        if (isUnlocked)
        {
            return;
        }

        if (lockCloseupLayer != null)
        {
            lockCloseupLayer.SetActive(true);
        }

        if (toolboxBackButton != null)
        {
            toolboxBackButton.SetActive(false);
        }
    }

    public void CloseLockCloseup()
    {
        if (lockCloseupLayer != null)
        {
            lockCloseupLayer.SetActive(false);
        }

        if (toolboxBackButton != null)
        {
            toolboxBackButton.SetActive(true);
        }
    }

    public void NextDigit(int index)
    {
        if (isUnlocked)
        {
            return;
        }

        if (index < 0 || index >= currentDigits.Length)
        {
            return;
        }

        currentDigits[index]++;

        if (currentDigits[index] > 9)
        {
            currentDigits[index] = 0;
        }

        UpdateDigit(index);
        CheckCode();
    }

    private void UpdateDigit(int index)
    {
        if (digitImages == null ||
            index >= digitImages.Length ||
            digitImages[index] == null)
        {
            return;
        }

        int value = currentDigits[index];

        if (digitSprites == null ||
            value >= digitSprites.Length ||
            digitSprites[value] == null)
        {
            return;
        }

        digitImages[index].sprite = digitSprites[value];
    }

    private void UpdateAllDigits()
    {
        for (int i = 0; i < currentDigits.Length; i++)
        {
            UpdateDigit(i);
        }
    }

    private void CheckCode()
    {
        string enteredCode =
            currentDigits[0].ToString() +
            currentDigits[1].ToString() +
            currentDigits[2].ToString() +
            currentDigits[3].ToString();

        if (enteredCode != correctCode)
        {
            return;
        }

        UnlockToolbox();
    }

    private void UnlockToolbox()
    {
        isUnlocked = true;

        if (lockCloseupLayer != null)
        {
            lockCloseupLayer.SetActive(false);
        }

        if (toolboxBackButton != null)
        {
            toolboxBackButton.SetActive(true);
        }

        UpdateToolboxVisual();
    }

    private void UpdateToolboxVisual()
    {
        if (toolboxLockedImage != null)
        {
            toolboxLockedImage.SetActive(!isUnlocked);
        }

        if (toolboxOpenImage != null)
        {
            toolboxOpenImage.SetActive(isUnlocked);
        }
    }
}