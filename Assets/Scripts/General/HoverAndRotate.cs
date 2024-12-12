using UnityEngine;

public class HoverAndRotate : MonoBehaviour
{
    public float rotationSpeed = 50f; // Degrees per second
    public float bobbingSpeed = 3f; // Oscillation speed
    public float bobbingHeight = 0.1f; // Ocillation height
    private float originalY; // Starting height
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);

        float hoverHeight = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, originalY + hoverHeight, transform.position.z);
    }
}
