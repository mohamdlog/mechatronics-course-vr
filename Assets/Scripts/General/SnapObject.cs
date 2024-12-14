using UnityEngine;
using TMPro;

public class SnapObject : MonoBehaviour
{
    public Transform snapTarget; // The target's position we want to snap to
    public float snapDistance = 0.2f; // The distance tolerated
    public GameObject requiredChild; // Optional required object before snapping
    public TextMeshPro speechTextMesh; // Optional text box

    private bool isSnapped = false; // To track if item has been snapped

    // Update is called once per frame
    void Update()
    {
        if (!isSnapped && CanSnap())
        {
            float distance = Vector3.Distance(transform.position, snapTarget.position);
            if (distance < snapDistance)
            {
                transform.position = snapTarget.position;
                transform.parent = snapTarget;
                isSnapped = true;
                Debug.Log($"{gameObject.name} snapped into place!");
            }
        }
    }

    private bool CanSnap()
    {
        if (requiredChild == null)
        {
            return true;
        }

        foreach (Transform child in snapTarget)
        {
            if (child.gameObject == requiredChild)
            {
                return true;
            }
        }
        speechTextMesh.text = ($"Retrieve {requiredChild.name} before assembling {transform.name}");
        return false;
    }
}
