using UnityEngine;

public class OfficeDoorController : MonoBehaviour
{
    public Transform teleportTarget;   
    public float interactRange = 3f;   

    private Transform playerTransform;
    private CharacterController characterController;
    private PlayerMoneyPickup playerMoneyPickup;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            characterController = playerTransform.GetComponent<CharacterController>();
            playerMoneyPickup = playerTransform.GetComponent<PlayerMoneyPickup>();
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerMoneyPickup != null && playerMoneyPickup.HasMoney())
            {
                if (characterController != null)
                    characterController.enabled = false;

                playerTransform.position = teleportTarget.position;

                if (characterController != null)
                    characterController.enabled = true;

                playerMoneyPickup.UseMoney();

                // ✅ REMOVE ANY UI ACTIVATION HERE
                // Do not activate cameraStatusPanel in the Office

                Debug.Log("Office door opened. Player escaped with money.");
            }
            else
            {
                Debug.Log("You need your money to leave the office.");
            }
        }
    }
}