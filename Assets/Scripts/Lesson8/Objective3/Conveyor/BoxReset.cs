using UnityEngine;
using System.Collections;

public class BoxReset : MonoBehaviour
{
    private Vector3 originalPosition; // Store the original position
    private Quaternion originalRotation; // Store the original rotation
    private Transform child; // Contains the SLU logo
    private MeshRenderer meshRenderer; // The SLU logo
    private Renderer _renderer; // Renderer of the box
    private Material originalMaterial; // Store the original shared material
    private bool isResetting = false; // To prevent multiple coroutines

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        child = transform.GetChild(0);
        meshRenderer = child.GetComponent<MeshRenderer>();
        _renderer = GetComponent<Renderer>();
        originalMaterial = _renderer.sharedMaterial;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        {
            if (Vector3.Distance(transform.position, originalPosition) > 0.1f && !isResetting)
            {
                StartCoroutine(ResetObject());
            }
        }
    }

    IEnumerator ResetObject()
    {
        isResetting = true;
        yield return new WaitForSeconds(5f);
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        meshRenderer.enabled = false;
        _renderer.sharedMaterial = originalMaterial;
        isResetting = false;
    }
}
