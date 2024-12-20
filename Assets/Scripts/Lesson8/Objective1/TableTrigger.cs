using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DCMotorPart"))
        {
            SnapObject snapScript = other.GetComponent<SnapObject>();
            snapScript.Snap();
        }
    }
}
