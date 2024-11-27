/* A script to simulate XR object grabbing using mouse controls for testing without VR hardware.
 Note: Might alter this script to make it global instead of just per GameObject*/

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MouseGrabInteraction : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera for raycasting.
    private XRGrabInteractable grabInteractable; // Holds the XRGI component of the GameObject.
    private bool isGrabbing; // Boolean to track whether object is being grabbed.
    private Vector3 grabOffset; // Positional offset between mouse cursor's hit point and object's center

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("This GameObject must have an XRGrabInteractable component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If left click is being held
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Casts ray from screen point where mouse clicked
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
        else if (Input.GetMouseButtonUp(0)) // Detects when left mouse button is released
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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point + grabOffset;
        }
    }
}
