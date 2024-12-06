using UnityEngine;
using System.Collections;

public class BoxReset : MonoBehaviour
{
    public float resetTime = 5f;
    public GameObject prefab; // Reference to the original prefab
    private Vector3 originalPosition;
    private bool isResetting = false; // To prevent multiple coroutines

    void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        {
            if (Vector3.Distance(transform.position, originalPosition) > .1f && !isResetting)
            {
                StartCoroutine(ResetObject());
            }
        }
    }

    IEnumerator ResetObject()
    {
        isResetting = true;

        // Wait for the specified time
        yield return new WaitForSeconds(resetTime);

        // Instantiate a new object from the prefab at the original position and rotation
        Instantiate(prefab, originalPosition, transform.rotation);

        // Destroy the current object
        Destroy(gameObject);

        isResetting = false;
    }
}
