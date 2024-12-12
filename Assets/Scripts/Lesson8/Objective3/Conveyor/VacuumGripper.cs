using UnityEngine;

public class VacuumGripper : MonoBehaviour
{
    private FixedJoint joint; // Fixed joint for connection between gripper and box
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Box") && !joint)
        {
            Rigidbody boxRigidbody = other.rigidbody;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = boxRigidbody;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt") && joint)
        {
            Destroy(joint);
        }
    }
}