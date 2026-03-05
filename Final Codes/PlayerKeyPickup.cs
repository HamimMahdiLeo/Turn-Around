using UnityEngine;

public class PlayerKeyPickup : MonoBehaviour
{
    public float interactRange = 2.5f;
    public float sphereRadius = 0.3f;
    public LayerMask keyLayer;
    public UnityEngine.Camera playerCamera;  // ✅ fixed

    private bool hasKey = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasKey)
        {
            TryPickupKey();
        }
    }

    void TryPickupKey()
    {
        RaycastHit hit;

        if (Physics.SphereCast(playerCamera.transform.position, sphereRadius, playerCamera.transform.forward, out hit, interactRange, keyLayer))
        {
            if (hit.collider.CompareTag("Key"))
            {
                hasKey = true;
                Destroy(hit.collider.gameObject);
                Debug.Log("🔑 Key picked up by looking at it!");
                return;
            }
        }

        Debug.Log("No key in sight to pick up.");
    }

    public bool HasKey()
    {
        return hasKey;
    }

    public void UseKey()
    {
        hasKey = false;
        Debug.Log("🔓 Key used.");
    }
}
