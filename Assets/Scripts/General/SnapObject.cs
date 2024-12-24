using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class SnapObject : MonoBehaviour
{
    public Transform snapTarget; // The target's position we want to snap to
    public Collider snapCollider; // Collider that defines snapping zone
    public GameObject requiredChild; // Optional required object before snapping
    public TextMeshPro textMesh; // Optional text box

    private bool isSnapped = false; // To track if item has been snapped
    private XRGrabInteractable grabInteractable; // Reference to XRGRabInteractable
    private Collider objectCollider; // Reference to this object's collider

    private Vector3 originalPosition; // To track original position
    private Quaternion originalRotation; // To track original rotation

    // Awake is called once GameObject is loaded
    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectCollider = GetComponent<Collider>();
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnSelectExited); // Subscribe to event
    }

    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnSelectExited); // Unsubscribe to avoid errors
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (!isSnapped && CanSnap())
        {
            if (snapCollider.bounds.Intersects(objectCollider.bounds))
            {
                transform.SetParent(snapTarget);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                isSnapped = true;
                if (textMesh != null)
                {
                    textMesh.text = $"{gameObject.name} snapped into place!\nReturn to examine the next component.";
                }
            }
        }
    }

    private bool CanSnap()
    {
        if (requiredChild == null)
            return true;

        foreach (Transform child in snapTarget)
        {
            if (child.gameObject == requiredChild)
                return true;
        }

        if (textMesh != null)
        {
            textMesh.text = $"Retrieve {requiredChild.name} before assembling {transform.name}.";
        }

        ResetObject(); // Reset if missing required child
        return false;
    }

    private void ResetObject()
    {
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        Debug.Log($"{gameObject.name} reset to its original position");
    }
}