using UnityEngine;

public class BigSafePuzzleController : MonoBehaviour
{
    private const int CodeLength = 6;

    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;

    [Header("Safe")]
    [SerializeField] private GameObject closedSafeImage;
    [SerializeField] private GameObject openSafeImage;
    [SerializeField] private GameObject keypadButtons;
    [SerializeField] private GameObject keycardObject;

    [Header("Power")]
    [SerializeField] private ElectricPanelPuzzleController electricPanel;

    [Header("Code")]
    [SerializeField] private string correctCode = "972146";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip correctCodeSound;
    [SerializeField] private AudioClip wrongCodeSound;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private string currentCode = "";
    private bool isOpen;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        if (openSafeImage != null)
        {
            openSafeImage.SetActive(false);
        }

        if (keycardObject != null)
        {
            keycardObject.SetActive(false);
        }

        UpdateVisual();
    }

    public void ShowCloseup()
    {
        if (closeupLayer == null)
        {
            return;
        }

        currentCode = "";

        closeupLayer.SetActive(true);

        UpdateVisual();

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

        currentCode = "";

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void AddDigit(string digit)
    {
        if (isOpen)
        {
            return;
        }

        if (electricPanel == null || !electricPanel.IsPowered)
        {
            return;
        }

        if (string.IsNullOrEmpty(digit))
        {
            return;
        }

        PlaySound(buttonClickSound);

        currentCode += digit;

        if (currentCode.Length >= CodeLength)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (currentCode == correctCode)
        {
            OpenSafe();
        }
        else
        {
            PlaySound(wrongCodeSound);
        }

        currentCode = "";
    }

    private void OpenSafe()
    {
        isOpen = true;

        PlaySound(correctCodeSound);

        UpdateVisual();

        if (keycardObject != null)
        {
            keycardObject.SetActive(true);
        }
    }

    private void UpdateVisual()
    {
        if (closedSafeImage != null)
        {
            closedSafeImage.SetActive(!isOpen);
        }

        if (openSafeImage != null)
        {
            openSafeImage.SetActive(isOpen);
        }

        if (keypadButtons != null)
        {
            keypadButtons.SetActive(!isOpen);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}