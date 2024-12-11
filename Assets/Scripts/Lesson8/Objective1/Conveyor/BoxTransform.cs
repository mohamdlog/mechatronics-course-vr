using UnityEngine;
using System.Collections;
public class BoxTransform : MonoBehaviour
{
    public Material material; // New material to apply
    private Renderer _renderer; // The box's renderer
    private Transform child; // The game object containing the SLU logo
    private MeshRenderer meshRenderer; // Mesh renderer for child game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            _renderer = other.GetComponent<Renderer>();
            Material[] materials = _renderer.materials;
            materials[0] = material;
            _renderer.materials = materials;

            child = other.transform.GetChild(0);
            meshRenderer = child.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
        }
    }
}
