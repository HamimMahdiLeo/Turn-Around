using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeExitController : MonoBehaviour
{
    public float interactRange = 3f;   // How close player must be to interact
    public string endingSceneName = "Ending"; // Scene to load

    public GameObject cameraStatusPanel; // optional, but we do NOT enable it

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
            // Load Ending Scene
            SceneManager.LoadScene(endingSceneName);

            Debug.Log("Maze exited. Loading Ending Scene.");
        }
    }
}
