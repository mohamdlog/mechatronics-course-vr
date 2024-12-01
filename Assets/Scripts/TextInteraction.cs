using UnityEngine;
using System.Collections;
using TMPro;

public class RobotInteraction : MonoBehaviour
{
    [Header("Speech Bubble Settings")]
    public GameObject speechBubbleInstance;
    public TextMeshPro speechTextMesh;
    public string text1 = "Text 1";
    public string text2 = null;

    private bool playerInZone = false;

    // Start is called once after the MonoBehaviour is created
    void Start()
    {

        speechBubbleInstance.SetActive(false);
    }

    // Called when player is in trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speechTextMesh.text = text1;
            speechBubbleInstance.SetActive(true);
            if (!string.IsNullOrEmpty(text2))
            {
                playerInZone = true;
                StartCoroutine(RepeatAction());
            }
        }
    }

    IEnumerator RepeatAction()
    {
        while (playerInZone)
        {
            yield return new WaitForSeconds(4);
            speechTextMesh.text = (speechTextMesh.text == text1) ? text2 : text1;
        }
    }

    // Called when player exits trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            speechBubbleInstance.SetActive(false);
        }
    }
}
