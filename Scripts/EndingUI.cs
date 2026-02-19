using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // load main menu
    }
}
