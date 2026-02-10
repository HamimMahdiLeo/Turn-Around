using UnityEngine;

public class CCTVMonitor : MonoBehaviour
{
    public MeshRenderer screenRenderer;

    [Header("Materials")]
    public Material glitchMat;
    public Material liveMat;

    void Awake()
    {
        if (screenRenderer == null)
            screenRenderer = GetComponent<MeshRenderer>();

        // Start glitch
        SetGlitch();
    }

    // Call only when camera fully restored
    public void SetLive()
    {
        if (screenRenderer != null && liveMat != null)
            screenRenderer.material = liveMat;
    }

    public void SetGlitch()
    {
        if (screenRenderer != null && glitchMat != null)
            screenRenderer.material = glitchMat;
    }
}
