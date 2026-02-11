using UnityEngine;
using TMPro;  // make sure this is included

public class KeyboardInteractionUI : MonoBehaviour
{
    [Header("Player & Raycast")]
    public Transform playerCamera;
    public float interactDistance = 3f;

    [Header("Keys")]
    public KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    [Header("UI")]
    public CameraStatusUI cameraStatus;

    [Header("Hint")]
    [Tooltip("Hint shown when player is in front of desk and not holding any key")]
    public TMP_Text HintMessage; // this will now show in inspector

    [Header("Hold Settings")]
    public float holdTime = 3f;
    public float decaySpeed = 1.5f;

    private int holdingIndex = -1;
    private DeskController currentDesk;

    void Update()
    {
        DetectDesk();
        DetectHeldKey();
        UpdateProgress();

        // show hint only when looking at a desk and not holding any key
        if (HintMessage != null)
            HintMessage.gameObject.SetActive(currentDesk != null && holdingIndex == -1);
    }

    void DetectDesk()
    {
        currentDesk = null;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            currentDesk = hit.collider.GetComponentInParent<DeskController>();
        }
    }

    void DetectHeldKey()
    {
        holdingIndex = -1;

        if (currentDesk == null)
            return;

        for (int i = 0; i < currentDesk.cameraCount; i++)
        {
            int camIndex = currentDesk.startCameraIndex + i;

            if (camIndex >= cameraStatus.cameras.Length)
                continue;

            if (Input.GetKey(keys[i]) && !cameraStatus.cameras[camIndex].isOnline)
            {
                holdingIndex = camIndex;
                break; // only ONE key at a time
            }
        }
    }

    void UpdateProgress()
    {
        for (int i = 0; i < cameraStatus.cameras.Length; i++)
        {
            var cam = cameraStatus.cameras[i];

            if (cam.isOnline)
                continue;

            if (i == holdingIndex)
                cam.progress += Time.deltaTime / holdTime;
            else
                cam.progress -= Time.deltaTime * decaySpeed / holdTime;

            cam.progress = Mathf.Clamp01(cam.progress);
        }
    }
}
