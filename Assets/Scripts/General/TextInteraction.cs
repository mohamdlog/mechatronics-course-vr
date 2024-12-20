using UnityEngine;
using System.Collections;
using TMPro;

public class TextInteraction : MonoBehaviour
{
    [Header("Speech Bubble Settings")]
    public GameObject optionalInstance = null; // An optional object besides speech bubble we want to hide/show
    public GameObject textInstance; // The speech bubble
    public TextMeshPro textMesh; // The text box
    public string text1 = "Text 1"; // Default text
    public string text2 = null; // Optional second text
    public string text3 = null; // Optional third text

    private bool playerInZone = false; // To help with coroutine
    private string[] textArray; // For looping through texts
    private Coroutine repeatCoroutine; // To easily stop coroutine

    // Start is called once after the MonoBehaviour is created
    void Start()
    {

        textInstance.SetActive(false);
    }

    // Called when player is in trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (string.IsNullOrEmpty(textMesh.text))
            {
                textMesh.text = text1;
            }
            if (optionalInstance) optionalInstance.SetActive(false);
            textInstance.SetActive(true);
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
                textMesh.text = textArray[index];
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
            textInstance.SetActive(false);
            if (optionalInstance) optionalInstance.SetActive(true);
        }
    }
}
