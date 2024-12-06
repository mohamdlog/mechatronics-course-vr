using UnityEngine;
using System.Collections;

public class BoxReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation; // Store the original rotation
    private Transform child;
    private MeshRenderer meshRenderer;
    private Collision box;
    private Renderer renderer;
    private Material originalMaterial; // Store the original shared material
    private bool isResetting = false; // To prevent multiple coroutines

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation; // Store the original rotation
        child = transform.GetChild(0);
        meshRenderer = child.GetComponent<MeshRenderer>();
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.sharedMaterial; // Store the original shared material
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        {
            if (Vector3.Distance(transform.position, originalPosition) > 0.1f && !isResetting)
            {
                box = other;
                StartCoroutine(ResetObject());
            }
        }
    }

    IEnumerator ResetObject()
    {
        isResetting = true;
        yield return new WaitForSeconds(5f);
        transform.position = originalPosition;
        transform.rotation = originalRotation; // Reset to the original rotation
        meshRenderer.enabled = false;
        renderer.sharedMaterial = originalMaterial; // Reset to the original shared material
        isResetting = false;
    }
}
