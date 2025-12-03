using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float lookSensitivity = 2f;

    Rigidbody rb;
    Camera cam;
    float rotationY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look (Y rotation only)
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.Rotate(0f, mouseX, 0f);
    }

    void FixedUpdate()
    {
        // WASD movement (relative to player forward/right)
        float h = Input.GetAxis("Horizontal");   // A/D
        float v = Input.GetAxis("Vertical");     // W/S

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;
        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;
    }
}
