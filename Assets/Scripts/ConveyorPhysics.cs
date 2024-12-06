using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ConveyorBeltPhysics : MonoBehaviour
{
    public Vector3 conveyorDirection = Vector3.forward;
    public float conveyorSpeed = 1.0f;

    private void OnCollisionStay(Collision collision)
    {
        // Check if the object has a Rigidbody
        Rigidbody rb = collision.rigidbody;
        if (rb != null && !rb.isKinematic)
        {
            // Apply movement to the object
            Vector3 movement = transform.forward * conveyorSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
