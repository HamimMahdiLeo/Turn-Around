using UnityEngine;

public class CCTVUnit : MonoBehaviour
{
    [HideInInspector] public Camera cam;

    void Awake()
    {
        // Find the camera in this object or children
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
            Debug.LogError("CCTVUnit: No Camera found in children!");

        // Camera is always enabled now
        if (cam != null)
            cam.enabled = true;
    }

    // Optional Activate method if you want additional logic in future
    public void Activate()
    {
        // Cameras are already enabled, so nothing needed here
    }
}
