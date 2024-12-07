using UnityEngine;

public class VacuumGripper : MonoBehaviour
{
    private FixedJoint joint;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            Rigidbody boxRigidbody = other.rigidbody;
            joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = boxRigidbody;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        Destroy(joint);
    }
}