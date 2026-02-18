using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class JumpscareManager : MonoBehaviour
{
    public GameObject jumpscareVideoUI; // Raw Image
    public VideoPlayer videoPlayer;

    private bool triggered = false;

    public void TriggerJumpscare()
    {
        if (triggered) return;
        triggered = true;

        Time.timeScale = 0f; // freeze gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        jumpscareVideoUI.SetActive(true);
        videoPlayer.Play();

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Time.timeScale = 1f;
        jumpscareVideoUI.SetActive(false);

        SceneManager.LoadScene("GameOver"); // load GameOver
    }
}
