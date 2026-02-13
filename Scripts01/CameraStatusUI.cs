using UnityEngine;
using TMPro;

[System.Serializable]
public class CameraLine
{
    public string cameraName;
    public TMP_Text textField;
    public CCTVMonitor monitor;   // assign the monitor here
    [HideInInspector] public float progress;
    [HideInInspector] public bool isOnline;
}

public class CameraStatusUI : MonoBehaviour
{
    public CameraLine[] cameras; // size = 9
    public GameObject allOnlineMessage;   // "System Online" message

    void Update()
    {
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
                        cam.monitor.SetLive();   // switch to RT texture material
                }
                cam.textField.color = Color.green;
            }
            else
            {
                cam.isOnline = false;
                cam.textField.color = Color.white;

                if (cam.monitor != null)
                    cam.monitor.SetGlitch();   // show glitch while not online
                allOnline = false;
            }
        }

        if (allOnline && allOnlineMessage != null)
            allOnlineMessage.SetActive(true);
    }

    string GenerateBar(float progress)
    {
        int total = 12;
        int filled = Mathf.RoundToInt(progress * total);
        return new string('█', filled) + new string('░', total - filled);
    }

    // <<< ADD THIS METHOD
    public bool AllCamerasOnline()
    {
        foreach (var cam in cameras)
            if (!cam.isOnline) return false;
        return true;
    }
}
