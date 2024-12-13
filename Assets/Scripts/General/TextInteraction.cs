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
    public string text3 = null;

    private bool playerInZone = false;
    private string[] textArray;
    private Coroutine repeatCoroutine;

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
            if (string.IsNullOrEmpty(speechTextMesh.text))
            {
                speechTextMesh.text = text1;

            }
            speechBubbleInstance.SetActive(true);
            if (!string.IsNullOrEmpty(text2))
            {
                textArray = new string[] { text1, text2, text3 };
                playerInZone = true;
                repeatCoroutine = StartCoroutine(RepeatAction());
            }
        }
    }

    IEnumerator RepeatAction()
    {
        int index = 1;
        while (playerInZone)
        {
            if (!string.IsNullOrEmpty(textArray[index]))
            {
                yield return new WaitForSeconds(4);
                speechTextMesh.text = textArray[index];
            }
            index = (index + 1) % textArray.Length;
        }
    }

    // Called when player exits trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (repeatCoroutine != null)
            {
                StopCoroutine(repeatCoroutine);
            }
            playerInZone = false;
            speechBubbleInstance.SetActive(false);
        }
    }
}
