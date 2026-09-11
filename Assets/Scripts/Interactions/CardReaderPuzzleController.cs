using UnityEngine;
using UnityEngine.UI;

public class CardReaderPuzzleController : MonoBehaviour
{
    [Header("Closeup")]
    [SerializeField] private GameObject closeupLayer;
    [SerializeField] private Image readerImage;

    [Header("Sprites")]
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    [Header("Game")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private DoorExitController doorExitController;
    [SerializeField] private MouseZoomController zoomController;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip unlockSound;

    private bool isUnlocked;

    public bool IsUnlocked => isUnlocked;

    private void Start()
    {
        if (closeupLayer != null)
        {
            closeupLayer.SetActive(false);
        }

        UpdateVisual();
    }

    public void ShowCloseup()
    {
        if (closeupLayer == null)
        {

            return;
        }

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

        if (zoomController != null)
        {
            zoomController.enabled = true;
        }
    }

    public void TryUseKeycard()
    {
        if (isUnlocked)
        {
            return;
        }

        if (gameState == null)
        {

            return;
        }

        if (!gameState.IsItemSelected("keycard"))
        {
            return;
        }
        isUnlocked = true;

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        if (doorExitController != null)
        {
            doorExitController.UnlockDoor();
        }

        if (audioSource != null && unlockSound != null)
        {
            audioSource.PlayOneShot(unlockSound);
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (readerImage == null)
        {
            return;
        }

        readerImage.sprite = isUnlocked
            ? unlockedSprite
            : lockedSprite;
    }
}