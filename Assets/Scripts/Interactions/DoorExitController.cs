using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExitController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject doorCloseupLayer;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private AudioClip openSound;

    [Header("Room")]
    [SerializeField] private MouseZoomController zoomController;

    [Header("Scene")]
    [SerializeField] private string nextSceneName = "Level3";

    private bool isDoorUnlocked;

    private void Start()
    {
        if (doorCloseupLayer != null)
        {
            doorCloseupLayer.SetActive(false);
        }
    }

    public void ShowDoorCloseup()
    {
        if (doorCloseupLayer == null)
        {
            return;
        }

        doorCloseupLayer.SetActive(true);

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

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryOpenDoor()
    {
        if (!isDoorUnlocked)
        {
            if (audioSource != null && lockedSound != null)
            {
                audioSource.PlayOneShot(lockedSound);
            }

            return;
        }

        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        SceneManager.LoadScene(nextSceneName);
    }

    public void UnlockDoor()
    {
        isDoorUnlocked = true;
    }
}