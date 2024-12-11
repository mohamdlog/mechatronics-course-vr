using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.InputSystem;

public class MouseGrabInteraction : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera for raycasting
    private XRGrabInteractable grabInteractable; // Holds the XRGI component of the GameObject
    private bool isGrabbing; // Boolean to track whether object is being grabbed
    private Vector3 grabOffset; // Positional offset between mouse cursor's hit point and object's center
    private Mouse mouse; // Reference to the mouse input device

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Mouse.current;
        mainCamera = Camera.main;
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        if (grabInteractable == null)
        {
            Debug.LogError($"GameObject '{gameObject.name}' must have an XRGrabInteractable component.", gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue()); // Casts ray from mouse position
            if (Physics.Raycast(ray, out RaycastHit hit)) // Checks is ray intersects an object and stores details
            {
                Debug.Log($"Hit object: {hit.transform.name}");
                if (hit.transform == this.transform)
                {
                    isGrabbing = true;
                    grabOffset = transform.position - hit.point; // Calculates offset between GameObject's position and hit point.
                }
            }
        }
        else if (mouse.leftButton.wasReleasedThisFrame) // Detects when left mouse button is released
        {
            isGrabbing = false;
        }

        if (isGrabbing)
        {
            Debug.Log("Object is being grabbed");
            MoveObjectWithMouse();
        }
    }

    // Casts ray from mouse current position. If hit occurs, updates GameObject's position to the hit point.
    void MoveObjectWithMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point + grabOffset;
        }
    }
}
