using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform teleportTarget;   // Assign the transform where player should teleport (next room)
    public float interactRange = 3f;   // How close player must be to interact

    public GameObject cameraStatusPanel;   // NEW (assign in inspector)

    private Transform playerTransform;
    private PlayerKeyPickup playerKeyPickup;
    private CharacterController characterController;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerKeyPickup = playerTransform.GetComponent<PlayerKeyPickup>();
        characterController = playerTransform.GetComponent<CharacterController>();
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerKeyPickup.HasKey())
            {
                // Teleport the player safely (disable CharacterController first)
                if (characterController != null) characterController.enabled = false;
                playerTransform.position = teleportTarget.position;
                if (characterController != null) characterController.enabled = true;

                playerKeyPickup.UseKey();

                // NEW: Enable camera UI when entering Security Room
                if (cameraStatusPanel != null)
                    cameraStatusPanel.SetActive(true);

                Debug.Log("Door opened and player teleported.");
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }
        }
    }
}
