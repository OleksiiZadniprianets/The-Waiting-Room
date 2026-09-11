using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalDoorPuzzleController : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private GameObject doorCloseupLayer;
    [SerializeField] private Image doorImage;
    [SerializeField] private RectTransform doorRect;
    [SerializeField] private GameObject doorHandleHotspot;
    [SerializeField] private GameObject doorEndingHotspot;
    [SerializeField] private GameObject doorBackButton;

    [Header("Door Sprites")]
    [SerializeField] private Sprite doorClosedSprite;
    [SerializeField] private Sprite doorOpenSprite;

    [Header("Closed Door Transform")]
    [SerializeField] private Vector2 closedDoorSize;
    [SerializeField] private Vector2 closedDoorPosition;

    [Header("Open Door Transform")]
    [SerializeField] private Vector2 openDoorSize;
    [SerializeField] private Vector2 openDoorPosition;

    [Header("Ending")]
    [SerializeField] private GameObject doorEyesImage;
    [SerializeField] private GameObject waitingTextImage;
    [SerializeField] private GameObject comeInTextImage;
    [SerializeField] private GameObject endingBlackOverlay;
    [SerializeField] private GameObject endingTheEndImage;
    [SerializeField] private GameObject endingMenuImage;
    [SerializeField] private GameObject endingContinueHotspot;

    [Header("Keypad")]
    [SerializeField] private GameObject keypadCloseupLayer;
    [SerializeField] private Image keypadImage;

    [Header("Keypad Sprites")]
    [SerializeField] private Sprite keypadRedSprite;
    [SerializeField] private Sprite keypadYellowSprite;
    [SerializeField] private Sprite keypadGreenSprite;

    [Header("Code")]
    [SerializeField] private string correctCode = "#642879*";

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip keypadClickClip;
    [SerializeField] private AudioClip wrongCodeClip;
    [SerializeField] private AudioClip redToYellowClip;
    [SerializeField] private AudioClip yellowToGreenClip;
    [SerializeField] private AudioClip doorLockedClip;
    [SerializeField] private AudioClip doorOpenClip;

    [Header("Music")]
    [SerializeField] private AudioClip horrorMusicClip;

    [Header("Scene")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    private string currentInput = "";

    private bool firstPassAccepted;
    private bool doorUnlocked;
    private bool doorOpened;

    private int endingStage;

    public bool DoorUnlocked => doorUnlocked;

    private void Start()
    {
        if (doorCloseupLayer != null)
            doorCloseupLayer.SetActive(false);

        if (keypadCloseupLayer != null)
            keypadCloseupLayer.SetActive(false);

        ResetAuthentication();

        doorOpened = false;
        endingStage = 0;

        SetClosedDoorVisual();

        if (doorHandleHotspot != null)
            doorHandleHotspot.SetActive(true);

        if (doorEndingHotspot != null)
            doorEndingHotspot.SetActive(false);

        if (doorBackButton != null)
            doorBackButton.SetActive(true);

        if (doorEyesImage != null)
            doorEyesImage.SetActive(false);

        if (waitingTextImage != null)
            waitingTextImage.SetActive(false);

        if (comeInTextImage != null)
            comeInTextImage.SetActive(false);

        if (endingBlackOverlay != null)
            endingBlackOverlay.SetActive(false);

        if (endingTheEndImage != null)
            endingTheEndImage.SetActive(false);

        if (endingMenuImage != null)
            endingMenuImage.SetActive(false);

        if (endingContinueHotspot != null)
            endingContinueHotspot.SetActive(false);
    }

    public void OpenDoorCloseup()
    {
        if (doorCloseupLayer == null)
            return;

        doorCloseupLayer.SetActive(true);

        if (zoomController != null)
            zoomController.enabled = false;
    }

    public void CloseDoorCloseup()
    {
        if (doorOpened)
            return;

        if (doorCloseupLayer != null)
            doorCloseupLayer.SetActive(false);

        if (zoomController != null)
            zoomController.enabled = true;
    }

    public void TryOpenDoor()
    {
        if (doorOpened)
            return;

        if (!doorUnlocked)
        {
            PlaySound(doorLockedClip);
            return;
        }

        doorOpened = true;
        endingStage = 0;

        PlaySound(doorOpenClip);

        SetOpenDoorVisual();

        if (doorHandleHotspot != null)
            doorHandleHotspot.SetActive(false);

        if (doorBackButton != null)
            doorBackButton.SetActive(false);

        if (doorEyesImage != null)
            doorEyesImage.SetActive(true);

        if (doorEndingHotspot != null)
            doorEndingHotspot.SetActive(true);

        StartHorrorMusic();
    }

    public void AdvanceEnding()
    {
        if (!doorOpened)
            return;

        endingStage++;

        if (endingStage == 1)
        {
            ShowWaitingMessage();
            return;
        }

        if (endingStage == 2)
        {
            ShowComeInMessage();
            return;
        }

        if (endingStage == 3)
        {
            FinishDoorEnding();
        }
    }

    private void ShowWaitingMessage()
    {
        if (waitingTextImage != null)
            waitingTextImage.SetActive(true);

        if (comeInTextImage != null)
            comeInTextImage.SetActive(false);
    }

    private void ShowComeInMessage()
    {
        if (waitingTextImage != null)
            waitingTextImage.SetActive(false);

        if (comeInTextImage != null)
            comeInTextImage.SetActive(true);
    }

    private void FinishDoorEnding()
    {
        if (doorEndingHotspot != null)
            doorEndingHotspot.SetActive(false);

        if (doorEyesImage != null)
            doorEyesImage.SetActive(false);

        if (waitingTextImage != null)
            waitingTextImage.SetActive(false);

        if (comeInTextImage != null)
            comeInTextImage.SetActive(false);

        if (endingBlackOverlay != null)
            endingBlackOverlay.SetActive(true);

        if (endingContinueHotspot != null)
            endingContinueHotspot.SetActive(true);

        endingStage = 3;
    }

    public void AdvanceEndScreen()
    {
        if (endingStage == 3)
        {
            ShowTheEnd();
            return;
        }

        if (endingStage == 4)
        {
            ShowReturnToMenu();
        }
    }

    private void ShowTheEnd()
    {
        endingStage = 4;

        if (endingBlackOverlay != null)
            endingBlackOverlay.SetActive(false);

        if (endingTheEndImage != null)
            endingTheEndImage.SetActive(true);
    }

    private void ShowReturnToMenu()
    {
        endingStage = 5;

        if (endingTheEndImage != null)
            endingTheEndImage.SetActive(false);

        if (endingMenuImage != null)
            endingMenuImage.SetActive(true);

        if (endingContinueHotspot != null)
            endingContinueHotspot.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void SetClosedDoorVisual()
    {
        if (doorImage != null && doorClosedSprite != null)
            doorImage.sprite = doorClosedSprite;

        if (doorRect != null)
        {
            doorRect.sizeDelta = closedDoorSize;
            doorRect.anchoredPosition = closedDoorPosition;
        }
    }

    private void SetOpenDoorVisual()
    {
        if (doorImage != null && doorOpenSprite != null)
            doorImage.sprite = doorOpenSprite;

        if (doorRect != null)
        {
            doorRect.sizeDelta = openDoorSize;
            doorRect.anchoredPosition = openDoorPosition;
        }
    }

    private void StartHorrorMusic()
    {
        if (BackgroundMusicManager.Instance == null)
            return;

        if (horrorMusicClip == null)
            return;

        BackgroundMusicManager.Instance.PlayMusic(horrorMusicClip);
    }

    public void OpenKeypadCloseup()
    {
        if (keypadCloseupLayer == null)
            return;

        currentInput = "";

        UpdateKeypadVisual();

        keypadCloseupLayer.SetActive(true);

        if (zoomController != null)
            zoomController.enabled = false;
    }

    public void CloseKeypadCloseup()
    {
        currentInput = "";

        if (!doorUnlocked && firstPassAccepted)
        {
            firstPassAccepted = false;
            UpdateKeypadVisual();
        }

        if (keypadCloseupLayer != null)
            keypadCloseupLayer.SetActive(false);

        if (zoomController != null)
            zoomController.enabled = true;
    }

    public void PressKey(string key)
    {
        if (doorUnlocked)
            return;

        PlaySound(keypadClickClip);

        currentInput += key;

        if (currentInput.Length < correctCode.Length)
            return;

        if (currentInput.Length > correctCode.Length)
        {
            HandleWrongCode();
            return;
        }

        CheckCode();
    }

    private void CheckCode()
    {
        if (currentInput != correctCode)
        {
            HandleWrongCode();
            return;
        }

        currentInput = "";

        if (!firstPassAccepted)
        {
            firstPassAccepted = true;

            UpdateKeypadVisual();
            PlaySound(redToYellowClip);

            return;
        }

        doorUnlocked = true;
        firstPassAccepted = false;

        UpdateKeypadVisual();
        PlaySound(yellowToGreenClip);
    }

    private void HandleWrongCode()
    {
        currentInput = "";

        PlaySound(wrongCodeClip);

        if (firstPassAccepted)
        {
            firstPassAccepted = false;
            UpdateKeypadVisual();
        }
    }

    private void ResetAuthentication()
    {
        currentInput = "";
        firstPassAccepted = false;
        doorUnlocked = false;

        UpdateKeypadVisual();
    }

    private void UpdateKeypadVisual()
    {
        if (keypadImage == null)
            return;

        if (doorUnlocked)
        {
            if (keypadGreenSprite != null)
                keypadImage.sprite = keypadGreenSprite;

            return;
        }

        if (firstPassAccepted)
        {
            if (keypadYellowSprite != null)
                keypadImage.sprite = keypadYellowSprite;

            return;
        }

        if (keypadRedSprite != null)
            keypadImage.sprite = keypadRedSprite;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}