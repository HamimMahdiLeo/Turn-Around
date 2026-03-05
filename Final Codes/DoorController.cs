using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform teleportTarget;
    public float interactRange = 3f;

    [Header("Security Room UI")]
    public GameObject cameraStatusPanel;      // assign on entry/exit doors
    public GameObject systemOnlineMessage;    // assign on exit door only

    [Header("Door Type")]
    public bool isSecurityRoomEntry = false;
    public bool isSecurityRoomExit = false;

    private Transform playerTransform;
    private PlayerKeyPickup playerKeyPickup;
    private CharacterController characterController;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerKeyPickup = playerTransform.GetComponent<PlayerKeyPickup>();
        characterController = playerTransform.GetComponent<CharacterController>();

        if (cameraStatusPanel != null)
            cameraStatusPanel.SetActive(false);
        if (systemOnlineMessage != null)
            systemOnlineMessage.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!playerKeyPickup.HasKey())
            {
                Debug.Log("You need a key to open this door.");
                return;
            }

            if (characterController != null)
                characterController.enabled = false;

            playerTransform.position = teleportTarget.position;

            if (characterController != null)
                characterController.enabled = true;

            playerKeyPickup.UseKey();
            Debug.Log("Door opened and player teleported.");

            // Enter Security Room → show panel
            if (isSecurityRoomEntry && cameraStatusPanel != null)
            {
                cameraStatusPanel.SetActive(true);

                // make sure UI can update again
                var statusUI = cameraStatusPanel.GetComponent<CameraStatusUI>();
                if (statusUI != null)
                    statusUI.disableUI = false;

                Debug.Log("Security Room UI enabled.");
            }

            // Exit Security Room → hide panel and prevent System Online from showing
            if (isSecurityRoomExit)
            {
                if (cameraStatusPanel != null)
                    cameraStatusPanel.SetActive(false);

                if (systemOnlineMessage != null)
                    systemOnlineMessage.SetActive(false);

                // disable UI updates in CameraStatusUI
                if (cameraStatusPanel != null)
                {
                    var statusUI = cameraStatusPanel.GetComponent<CameraStatusUI>();
                    if (statusUI != null)
                        statusUI.disableUI = true;
                }

                Debug.Log("Security Room UI and System Online message disabled.");
            }
        }
    }
}