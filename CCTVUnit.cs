using UnityEngine;

public class CCTVUnit : MonoBehaviour
{
    private UnityEngine.Camera cctvCamera;

    void Awake()
    {
        // Find camera on this GameObject or children
        cctvCamera = GetComponentInChildren<UnityEngine.Camera>();
        if (cctvCamera == null)
        {
            Debug.LogError("CCTVUnit: No Camera found in children!");
            return;
        }

        cctvCamera.enabled = false;
    }

    public void ActivateCamera()
    {
        if (cctvCamera != null)
            cctvCamera.enabled = true;
    }
}
