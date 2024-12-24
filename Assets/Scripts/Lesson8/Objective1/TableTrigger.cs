using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    private MonoBehaviour targetScript; // The script that will be enabled once part is in collider

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DCMotorPart"))
        {
            GameObject targetObject = other.gameObject;

            targetScript = targetObject.GetComponent<SnapObject>();

            if (targetScript != null)
            {
                targetScript.enabled = true;
            }
            else
            {
                Debug.LogWarning("No target script found on: " + targetObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DCMotorPart"))
        {
            GameObject targetObject = other.gameObject;

            if (targetScript != null)
            {
                targetScript.enabled = false;
            }
        }
    }
}
