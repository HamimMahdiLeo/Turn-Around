using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "Game";

    void Start()
    {
        videoPlayer.Play();
    }

    void Update()
    {
        if (!videoPlayer.isPlaying && videoPlayer.frame > 0)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
