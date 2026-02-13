using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game"); // load gameplay
    }

    public void ShowCredits()
    {
        Application.OpenURL("https://yourwebsite.com"); // replace with your link
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
