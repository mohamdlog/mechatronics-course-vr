using UnityEngine;

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

    void Start()
    {
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
    }

    private void HandleCameraRotation()
    {
        float mouseX = 0f;
        float mouseY = 0f;

        if (Input.GetMouseButton(1)) // Right mouse button held
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
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
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            if (Input.GetButtonDown("Jump"))
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