using UnityEngine;
using UnityEngine.UI;

public class SmallSafePuzzleController : MonoBehaviour
{
    private const int CodeLength = 4;

    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;

    [Header("Safe")]
    [SerializeField] private GameObject lockedSafeImage;
    [SerializeField] private GameObject openSafeImage;
    [SerializeField] private GameObject keypadButtons;
    [SerializeField] private GameObject fuseObject;

    [Header("Code")]
    [SerializeField] private string correctCode = "1025";

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

        if (fuseObject != null)
        {
            fuseObject.SetActive(false);
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

        if (fuseObject != null)
        {
            fuseObject.SetActive(true);
        }
    }

    private void UpdateVisual()
    {
        if (lockedSafeImage != null)
        {
            lockedSafeImage.SetActive(!isOpen);
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