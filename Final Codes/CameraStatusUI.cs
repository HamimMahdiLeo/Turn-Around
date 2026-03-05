using UnityEngine;
using TMPro;

[System.Serializable]
public class CameraLine
{
    public string cameraName;
    public TMP_Text textField;
    public CCTVMonitor monitor;
    [HideInInspector] public float progress;
    [HideInInspector] public bool isOnline;
}

public class CameraStatusUI : MonoBehaviour
{
    public CameraLine[] cameras; // size = 9
    public GameObject allOnlineMessage;   // "System Online" message

    [Header("Key Spawn")]
    public KeySpawner_SecurityRoom keySpawner;

    private bool keyTriggered = false;

    [HideInInspector] public bool disableUI = false; // NEW: prevents showing message

    void Update()
    {
        // skip message updates if disabled
        if (disableUI)
        {
            if (allOnlineMessage != null)
                allOnlineMessage.SetActive(false);
            return;
        }

        bool allOnline = true;

        foreach (var cam in cameras)
        {
            int percent = Mathf.RoundToInt(cam.progress * 100f);
            cam.textField.text = cam.cameraName + " [" + GenerateBar(cam.progress) + "] " + percent + "%";

            if (percent >= 100)
            {
                if (!cam.isOnline)
                {
                    cam.isOnline = true;
                    if (cam.monitor != null)
                        cam.monitor.SetLive();
                }
                cam.textField.color = Color.green;
            }
            else
            {
                cam.isOnline = false;
                cam.textField.color = Color.white;

                if (cam.monitor != null)
                    cam.monitor.SetGlitch();

                allOnline = false;
            }
        }

        if (allOnline)
        {
            if (allOnlineMessage != null)
                allOnlineMessage.SetActive(true);

            if (!keyTriggered && keySpawner != null)
            {
                keySpawner.SpawnKeyAfterCamerasActivated();
                keyTriggered = true;
            }
        }
    }

    string GenerateBar(float progress)
    {
        int total = 12;
        int filled = Mathf.RoundToInt(progress * total);
        return new string('█', filled) + new string('░', total - filled);
    }
}