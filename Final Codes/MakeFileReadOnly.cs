using System.IO;
using UnityEngine;

public class MakeFileReadOnly : MonoBehaviour
{
    public string filePath = "Assets/StreamingAsset/Instructions.txt";

    void Start()
    {
        if (File.Exists(filePath))
        {
            FileInfo info = new FileInfo(filePath);
            info.IsReadOnly = true;
            Debug.Log("Instructions.txt is now read-only.");
        }
    }
}