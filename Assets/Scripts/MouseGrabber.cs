using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MouseGrabInteraction : MonoBehaviour
{
    private Camera mainCamera;
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;
    private Vector3 grabOffset;

    void Start()
    {
        mainCamera = Camera.main;
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("This GameObject must have an XRGrabInteractable component.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Hit object: {hit.transform.name}");
                if (hit.transform == this.transform)
                {
                    isGrabbing = true;
                    grabOffset = transform.position - hit.point;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isGrabbing = false;
        }

        if (isGrabbing)
        {
            Debug.Log("Object is being grabbed");
            MoveObjectWithMouse();
        }
    }

    void MoveObjectWithMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point + grabOffset;
        }
    }
}
