using UnityEngine;
using TMPro;

[System.Serializable]
public class CameraLine
{
    public string cameraName;
    public TMP_Text textField;
    [HideInInspector] public float progress;
    [HideInInspector] public bool isOnline;
}

public class CameraStatusUI : MonoBehaviour
{
    public CameraLine[] cameras;
    public GameObject allOnlineMessage;

    void Update()
    {
        bool allOnline = true;

        foreach (var cam in cameras)
        {
            int percent = Mathf.RoundToInt(cam.progress * 100f);
            cam.textField.text =
                cam.cameraName + " [" + GenerateBar(cam.progress) + "] " + percent + "%";

            if (percent >= 100)
            {
                cam.isOnline = true;
                cam.textField.color = Color.green;
            }
            else
            {
                cam.isOnline = false;
                cam.textField.color = Color.white;
                allOnline = false;
            }
        }

        if (allOnline)
        {
            allOnlineMessage.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    string GenerateBar(float progress)
    {
        int total = 12;
        int filled = Mathf.RoundToInt(progress * total);
        return new string('█', filled) + new string('░', total - filled);
    }

    public bool AllCamerasOnline()
    {
        foreach (var cam in cameras)
            if (!cam.isOnline) return false;
        return true;
    }
}
