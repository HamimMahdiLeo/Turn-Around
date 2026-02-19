using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeExitController : MonoBehaviour
{
    public float interactRange = 3f;   // How close player must be to interact
    public string endingSceneName = "EndingScene"; // Scene to load

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(endingSceneName);

            Debug.Log("Maze exited. Loading Ending Scene.");
        }
    }
}
