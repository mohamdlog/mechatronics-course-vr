using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    private Renderer _renderer;
    private Material _conveyorMaterial;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _conveyorMaterial = _renderer.sharedMaterial;
        }
        else
        {
            Debug.LogError("Renderer component not found on this GameObject");
        }
    }

    void Update()
    {
        if (_conveyorMaterial != null)
        {
            Vector2 offset = _conveyorMaterial.mainTextureOffset;
            offset.y = (offset.y + scrollSpeed * Time.deltaTime) % 1.0f;
            _conveyorMaterial.mainTextureOffset = offset;
        }
    }
}
