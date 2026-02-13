using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class JumpscareManager : MonoBehaviour
{
    public GameObject jumpscareVideoUI;  // Raw Image GameObject
    public VideoPlayer videoPlayer;

    private bool triggered = false;

    public void TriggerJumpscare()
    {
        if (triggered) return;
        triggered = true;

        // Stop everything
        Time.timeScale = 0f;

        // Show video UI
        jumpscareVideoUI.SetActive(true);

        // Play video (audio included)
        videoPlayer.Play();

        // Listen for video end
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Video");
    }
}
