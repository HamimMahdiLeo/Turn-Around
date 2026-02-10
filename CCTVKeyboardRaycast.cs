using UnityEngine;

public class CCTVKeyboardRaycast : MonoBehaviour
{
    [Header("Camera / Monitor Setup")]
    public CCTVUnit[] cctvCameras;       // Cameras for this keyboard
    public CCTVMonitor[] monitors;       // Monitors for the cameras (1:1)

    [Header("Raycast Settings")]
    public Transform playerCameraTransform;  // Assign the player camera Transform
    public float interactDistance = 3f;      // Max distance to interact

    void Update()
    {
        // Shoot a ray from the player camera forward
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Check if we are looking at this keyboard
            if (hit.collider.gameObject == gameObject)
            {
                CheckNumberKeyInput();
            }
        }
    }

    void CheckNumberKeyInput()
    {
        // Loop through all cameras
        for (int i = 0; i < cctvCameras.Length; i++)
        {
            // KeyCode Alpha1 = 49, Alpha2 = 50, etc.
            KeyCode key = KeyCode.Alpha1 + i;

            if (Input.GetKeyDown(key))
            {
                // Activate the camera & monitor
                cctvCameras[i].ActivateCamera();
                monitors[i].SetLive();
                Debug.Log("Activated Camera " + (i + 1) + " via keyboard " + gameObject.name);
            }
        }
    }
}
