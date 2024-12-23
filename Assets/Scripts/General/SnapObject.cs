using UnityEngine;
using TMPro;

public class SnapObject : MonoBehaviour
{
    public Transform snapTarget; // The target's position we want to snap to
    public float snapDistance = 2.2f; // The distance tolerated
    public GameObject requiredChild; // Optional required object before snapping
    public TextMeshPro textMesh; // Optional text box

    private bool isSnapped = false; // To track if item has been snapped
    private Transform originalParent; // To track original parent
    private Vector3 originalPosition; // To track original position
    private Quaternion originalRotation; // To track original rotation

    // Awake is called once GameObject is loaded
    private void Awake()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        if (!isSnapped && CanSnap())
        {
            float distance = Vector3.Distance(transform.position, snapTarget.position);
            if (distance < snapDistance)
            {
                transform.parent = snapTarget;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                isSnapped = true;
                textMesh.text = ($"{gameObject.name} snapped into place!\nReturn to examine the next component.");
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
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        Debug.Log($"{gameObject.name} reset to its original position");
    }
}
