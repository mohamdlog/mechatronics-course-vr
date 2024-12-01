using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    // Speed at which the conveyor texture scrolls
    public float scrollSpeed = 1.0f;

    private Renderer _renderer; // Reference to the Renderer component
    private Material _conveyorMaterial; // Material of the conveyor's renderer

    void Start()
    {
        // Get the Renderer component attached to the GameObject
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            // Access the material used by the Renderer
            _conveyorMaterial = _renderer.sharedMaterial;
        }
        else
        {
            // Log an error if the Renderer component is missing
            Debug.LogError("Renderer component not found on this GameObject");
        }
    }

    void Update()
    {
        if (_conveyorMaterial != null)
        {
            // Calculate the new texture offset based on the scroll speed and time
            Vector2 offset = _conveyorMaterial.mainTextureOffset;
            offset.y = (offset.y + scrollSpeed * Time.deltaTime) % 1.0f; // Wrap offset to avoid large values
            _conveyorMaterial.mainTextureOffset = offset; // Apply the updated offset to the material
        }
    }
}
