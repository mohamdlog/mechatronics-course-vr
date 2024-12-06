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
            joint.breakForce = Mathf.Infinity;
            joint.breakTorque = Mathf.Infinity;
            Debug.Log("Box attached");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ConveyorBelt"))
        Destroy(joint);
        Debug.Log("Box Detached");
    }
}