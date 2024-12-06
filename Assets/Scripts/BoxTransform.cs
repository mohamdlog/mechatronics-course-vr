using UnityEngine;
using System.Collections;
public class BoxTransform : MonoBehaviour
{
    public Material material;
    private Renderer parentRenderer;
    private Transform child;
    private MeshRenderer meshRenderer;
    private Collider box;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            box = other;
            StartCoroutine(TransformObject());
        }
    }

    IEnumerator TransformObject()
    {
        yield return new WaitForSeconds(1f);
        parentRenderer = box.GetComponent<Renderer>();
        Material[] materials = parentRenderer.materials;
        materials[0] = material;
        parentRenderer.materials = materials;

        child = box.transform.GetChild(0);
        meshRenderer = child.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }
}
