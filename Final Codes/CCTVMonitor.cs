using UnityEngine;

public class CCTVMonitor : MonoBehaviour
{
    public MeshRenderer screenRenderer;
    public Material glitchMat;   // default glitch material
    public Material liveMat;     // material using RT

    void Awake()
    {
        if (screenRenderer != null && glitchMat != null)
            screenRenderer.material = glitchMat;  // start with glitch
    }

    public void SetLive()
    {
        if (screenRenderer != null && liveMat != null)
            screenRenderer.material = liveMat;    // show RT material
    }

    public void SetGlitch()
    {
        if (screenRenderer != null && glitchMat != null)
            screenRenderer.material = glitchMat;  // show glitch
    }
}
