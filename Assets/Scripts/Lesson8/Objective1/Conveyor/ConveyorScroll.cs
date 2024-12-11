using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    public float conveyorSpeed = 1f; // Conveyor speed
    private Renderer _renderer; // Reference to the Renderer component
    private Material conveyorMaterial; // Material of the conveyor's renderer
    private Vector3 conveyorDirection = Vector3.forward; // Direction of conveyor belt

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
    }

    void Update()
    {
        if (conveyorMaterial != null)
        {
            Vector2 offset = conveyorMaterial.mainTextureOffset;
            offset.y = (offset.y - conveyorSpeed * Time.deltaTime) % 1.0f;
            conveyorMaterial.mainTextureOffset = offset;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        conveyorSpeed = newSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && !rb.isKinematic)
        {
            Vector3 movement = transform.forward * conveyorSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
