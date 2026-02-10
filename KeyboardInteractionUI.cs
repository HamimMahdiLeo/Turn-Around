using UnityEngine;
using TMPro;

public class KeyboardInteractionUI : MonoBehaviour
{
    [Header("Player & Raycast")]
    public Transform playerCamera;
    public float interactDistance = 3f;

    [Header("Keys")]
    public KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };

    [Header("UI")]
    public TMP_Text HintMessage;
    public CameraStatusUI cameraStatus;

    [Header("Hold Settings")]
    public float holdTime = 3f;
    public float decaySpeed = 1.5f;

    private float[] holdProgress;
    private int holdingIndex = -1;
    private bool inRange;

    private DeskController currentDesk;

    void Start()
    {
        holdProgress = new float[keys.Length];
    }

    void Update()
    {
        CheckForDesk();
        DetectHeldKey();

        HintMessage.gameObject.SetActive(inRange && !cameraStatus.AllCamerasOnline());

        UpdateProgress();
    }

    void CheckForDesk()
    {
        inRange = false;
        currentDesk = null;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            DeskController desk = hit.collider.GetComponentInParent<DeskController>();
            if (desk != null)
            {
                inRange = true;
                currentDesk = desk;
            }
        }
    }

    void DetectHeldKey()
    {
        holdingIndex = -1;

        if (!inRange || currentDesk == null)
            return;

        for (int i = 0; i < keys.Length; i++)
        {
            int camIndex = currentDesk.startCameraIndex + i;

            if (camIndex >= cameraStatus.cameras.Length)
                continue;

            if (Input.GetKey(keys[i]) && !cameraStatus.cameras[camIndex].isOnline)
            {
                holdingIndex = i;
                break;
            }
        }
    }

    void UpdateProgress()
    {
        if (currentDesk == null)
            return;

        for (int i = 0; i < holdProgress.Length; i++)
        {
            int camIndex = currentDesk.startCameraIndex + i;

            if (camIndex >= cameraStatus.cameras.Length)
                continue;

            if (cameraStatus.cameras[camIndex].isOnline)
                continue;

            if (i == holdingIndex)
            {
                holdProgress[i] += Time.deltaTime;
                if (holdProgress[i] > holdTime)
                    holdProgress[i] = holdTime;
            }
            else
            {
                holdProgress[i] -= Time.deltaTime * decaySpeed;
                if (holdProgress[i] < 0f)
                    holdProgress[i] = 0f;
            }

            cameraStatus.cameras[camIndex].progress = holdProgress[i] / holdTime;
        }
    }
}
