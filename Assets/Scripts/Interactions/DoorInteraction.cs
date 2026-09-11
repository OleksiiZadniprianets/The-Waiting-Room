using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameState gameState;
    [SerializeField] private InventoryManager inventoryManager;

    [Header("Message UI")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    [Header("Scene")]
    [SerializeField] private string nextSceneName = "NextRoomScene";

    [Header("Settings")]
    [SerializeField] private float messageDuration = 2f;

    private Coroutine messageCoroutine;

    public void TryOpenDoor()
    {
        if (gameState == null)
        {
            return;
        }

        if (gameState.SelectedItem == null)
        {
            return;
        }

        if (!gameState.IsItemSelected("key"))
        {
            return;
        }

        if (inventoryManager != null)
        {
            inventoryManager.RemoveSelectedItem();
        }

        SceneManager.LoadScene(nextSceneName);
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

        messageCoroutine = StartCoroutine(
            ShowMessageRoutine(message));
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