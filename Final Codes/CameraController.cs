using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float sensitivity = 200f;
    public float minXAngle = -80f;
    public float maxXAngle = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minXAngle, maxXAngle);

        rotationY += mouseX;

        // Apply rotation
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);         // Camera X (up/down)
        playerTransform.rotation = Quaternion.Euler(0f, rotationY, 0f);        // Player Y (left/right)
    }
}
