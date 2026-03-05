using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // for reading/opening files

public class MainMenuUI : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("IntroScene"); // load gameplay
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // ===== New method for Instructions =====
	public void ShowInstructions()
	{
		string instructionsPath = Path.Combine(Application.streamingAssetsPath, "Instructions.txt"); 

		if (File.Exists(instructionsPath))
		{
			// Make the file read-only
			FileInfo info = new FileInfo(instructionsPath);
			info.IsReadOnly = true;

			// Open in default text editor (build-safe)
			string url = "file:///" + instructionsPath.Replace("\\", "/");
			Application.OpenURL(url);

			Debug.Log("Instructions file opened (read-only): " + url);
		}
		else
		{
			Debug.LogWarning("Instructions file not found at: " + instructionsPath);
		}
	}

    // ===== New method for Feedback =====
    public void GiveFeedback()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdmMirgKKuhcSzAfCFQSaVZr0__OdTUfwHRQ11C2PhRYlSiTw/viewform?usp=publish-editor");
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}