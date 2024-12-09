using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ConveyorBeltPhysics : MonoBehaviour
{
    private Vector3 conveyorDirection = Vector3.forward; // Direction of conveyor belt
    private Conveyor conveyor; // Conveyor shared values

    private void OnCollisionStay(Collision collision)
    {
        conveyor = GetComponent<Conveyor>();

        // Check if the object has a Rigidbody
        Rigidbody rb = collision.rigidbody;
        if (rb != null && !rb.isKinematic)
        {
            // Apply movement to the object
            Vector3 movement = transform.forward * conveyor.scrollSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
