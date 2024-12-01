using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class XRKeyboardMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.807f;
    public float mouseSensitivity = 5.0f;

    private Transform cameraTransform; // Reference to XR Rig's camera
    private CharacterController characterController;
    private float verticalVelocity = 0f;
    private float cameraPitch = 0f;

    private Keyboard keyboard;
    private Mouse mouse;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        keyboard = Keyboard.current;
        mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
    }

    private void HandleCameraRotation()
    {
        float mouseX = 0f;
        float mouseY = 0f;

        if (mouse.rightButton.isPressed) // Right mouse button held
        {
            mouseX = mouse.delta.x.ReadValue() * mouseSensitivity * 0.1f;
            mouseY = mouse.delta.y.ReadValue() * mouseSensitivity * 0.1f;
        }

        // Rotate camera horizontally and vertically
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        // Get input for movement
        float horizontal = (keyboard.aKey.isPressed ? -1f : 0f) + (keyboard.dKey.isPressed ? 1f : 0f);
        float vertical = (keyboard.wKey.isPressed ? 1f : 0f) + (keyboard.sKey.isPressed ? -1f : 0f);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        characterController.Move(move * speed * Time.deltaTime);

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Apply vertical movement
        Vector3 verticalMovement = Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(verticalMovement);
    }
}