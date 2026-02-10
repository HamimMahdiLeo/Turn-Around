using UnityEngine;

public class DeskKeyboard : MonoBehaviour
{
    [Header("CCTV Setup")]
    public CCTVUnit[] cctvCameras;
    public CCTVMonitor[] monitors;

    [Header("Raycast Settings")]
    public Transform playerCameraTransform;
    public float interactDistance = 3f;

    [Header("Activation Settings")]
    public float holdTimeRequired = 3f; // seconds to fully restore camera
    public float decayMultiplier = 1.2f; // decay faster than fill

    private float[] holdProgress;
    private bool[] isActivated;
    private int currentHoldingIndex = -1; // only one camera can be held

    void Start()
    {
        int camCount = cctvCameras.Length;
        holdProgress = new float[camCount];
        isActivated = new bool[camCount];
    }

    void Update()
    {
        // Raycast to detect if player is looking at this desk
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, interactDistance))
        {
            currentHoldingIndex = -1;
            DecayProgress();
            return;
        }

        if (hit.collider.GetComponentInParent<DeskKeyboard>() != this)
        {
            currentHoldingIndex = -1;
            DecayProgress();
            return;
        }

        HandleHoldInput();
        UpdateCameras();
    }

    void HandleHoldInput()
    {
        currentHoldingIndex = -1;

        for (int i = 0; i < cctvCameras.Length; i++)
        {
            KeyCode key = KeyCode.Alpha1 + i;
            if (Input.GetKey(key) && !isActivated[i])
            {
                currentHoldingIndex = i;
                break; // only one key at a time
            }
        }
    }

    void UpdateCameras()
    {
        for (int i = 0; i < cctvCameras.Length; i++)
        {
            // Already fully restored
            if (isActivated[i])
            {
                holdProgress[i] = holdTimeRequired;
                if (i < monitors.Length)
                    monitors[i].SetLive();
                continue;
            }

            if (i == currentHoldingIndex)
            {
                // Increase progress
                holdProgress[i] += Time.deltaTime;

                if (holdProgress[i] >= holdTimeRequired)
                {
                    holdProgress[i] = holdTimeRequired;
                    isActivated[i] = true;

                    if (i < monitors.Length)
                        monitors[i].SetLive();

                    Debug.Log($"{gameObject.name}: Camera {i + 1} fully restored");
                    currentHoldingIndex = -1;
                }
            }
            else
            {
                // Decay when not holding
                holdProgress[i] -= Time.deltaTime * decayMultiplier;
                if (holdProgress[i] < 0f)
                    holdProgress[i] = 0f;

                // ✅ Only set glitch if camera not fully restored
                if (!isActivated[i] && i < monitors.Length)
                    monitors[i].SetGlitch();
            }
        }
    }

    void DecayProgress()
    {
        // Called when raycast misses desk
        for (int i = 0; i < cctvCameras.Length; i++)
        {
            if (!isActivated[i])
            {
                holdProgress[i] -= Time.deltaTime * decayMultiplier;
                if (holdProgress[i] < 0f)
                    holdProgress[i] = 0f;

                if (i < monitors.Length)
                    monitors[i].SetGlitch();
            }
        }
    }
}
