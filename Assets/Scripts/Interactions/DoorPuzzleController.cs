using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPuzzleController : MonoBehaviour
{
    private const int CodeLength = 6;

    [Header("References")]
    [SerializeField] private GameObject doorCloseupLayer;
    [SerializeField] private GameObject doorKeyHotspot;
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private MouseZoomController zoomController;

    [Header("Code")]
    [SerializeField] private string correctCode = "482731";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip correctCodeSound;
    [SerializeField] private AudioClip wrongCodeSound;
    [SerializeField] private AudioClip doorLockedSound;
    [SerializeField] private AudioClip doorOpenSound;

    [Header("Scene")]
    [SerializeField] private string nextSceneName = "NextRoomScene";
    [SerializeField] private float sceneLoadDelay = 0.8f;

    private string currentCode = "";

    private bool isCodeUnlocked;
    private bool isDoorOpening;

    private void Start()
    {
        if (doorCloseupLayer != null)
        {
            doorCloseupLayer.SetActive(false);
        }

        if (doorKeyHotspot != null)
        {
            doorKeyHotspot.SetActive(true);
        }

        if (correctCode.Length != CodeLength)
        {
        }
    }

    public void OpenDoorCloseup()
    {
        if (doorCloseupLayer == null)
        {
            return;
        }

        doorCloseupLayer.SetActive(true);
        currentCode = "";

        if (zoomController != null)
        {
            zoomController.enabled = false;
        }
    }

    public void CloseDoorCloseup()
    {
        if (doorCloseupLayer != null)
        {
            doorCloseupLayer.SetActive(false);
        }

        currentCode = "";

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void AddDigit(string digit)
    {
        if (isCodeUnlocked)
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
            UnlockCode();
        }
        else
        {
            WrongCode();
        }

        currentCode = "";
    }

    private void UnlockCode()
    {
        isCodeUnlocked = true;

        PlaySound(correctCodeSound);

        if (doorKeyHotspot != null)
        {
            doorKeyHotspot.SetActive(true);
        }

        if (doorCloseupLayer != null)
        {
            doorCloseupLayer.SetActive(false);
        }

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }

    }

    private void WrongCode()
    {
        PlaySound(wrongCodeSound);
    }

    public void TryUseKey()
    {
        if (isDoorOpening)
        {
            return;
        }

        if (!isCodeUnlocked)
        {
            PlaySound(doorLockedSound);
            return;
        }

        if (gameState == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("key"))
        {
            PlaySound(doorLockedSound);

            return;
        }

        isDoorOpening = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        StartCoroutine(
            OpenDoorAndLoadNextScene()
        );
    }

    private IEnumerator OpenDoorAndLoadNextScene()
    {
        PlaySound(doorOpenSound);

        yield return new WaitForSeconds(
            sceneLoadDelay
        );

        SceneManager.LoadScene(
            nextSceneName
        );
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}