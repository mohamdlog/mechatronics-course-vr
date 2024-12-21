using UnityEngine;
using TMPro;

public class SnapObject : MonoBehaviour
{
    public Transform snapTarget; // The target's position we want to snap to
    public float snapDistance = 2f; // The distance tolerated
    public GameObject requiredChild; // Optional required object before snapping
    public TextMeshPro textMesh; // Optional text box

    private bool isSnapped = false; // To track if item has been snapped
    private Transform originalParent; // To track original parent
    private Vector3 originalPosition; // To track original position
    private Quaternion originalRotation; // To track original rotation

    // Start is called once after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Snap()
    {
        if (!isSnapped && CanSnap())
        {
            float distance = Vector3.Distance(transform.position, snapTarget.position);
            if (distance < snapDistance)
            {
                transform.parent = snapTarget;
                transform.position = snapTarget.position;
                transform.rotation = Quaternion.Euler(0, 0, 0);

                isSnapped = true;
                textMesh.text = ($"{gameObject.name} snapped into place!");
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

        textMesh.text = ($"Retrieve {requiredChild.name} before assembling {transform.name}.");
        ResetObject();
        return false;
    }

    private void ResetObject()
    {
        transform.parent = originalParent;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
