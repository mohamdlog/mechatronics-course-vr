using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    private Renderer _renderer; // Reference to the Renderer component
    private Material conveyorMaterial; // Material of the conveyor's renderer
    private Conveyor conveyor; // Conveyor shared values

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            conveyorMaterial = _renderer.sharedMaterial;
        }
        else
        {
            Debug.LogError("Renderer component not found on this GameObject");
        }
        conveyor = GetComponent<Conveyor>();

    }

    void Update()
    {
        if (conveyorMaterial != null)
        {
            Vector2 offset = conveyorMaterial.mainTextureOffset;
            offset.y = (offset.y - conveyor.scrollSpeed * Time.deltaTime) % 1.0f;
            conveyorMaterial.mainTextureOffset = offset;
        }
    }
}
