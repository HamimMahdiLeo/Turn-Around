using UnityEngine;

public class PlayerKeyPickup : MonoBehaviour
{
    public float interactRange = 3f;
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
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Key"))
            {
                hasKey = true;
                Destroy(hit.gameObject);
                Debug.Log("🔑 Key picked up!");
                return;
            }
        }
        Debug.Log("No key in range to pick up.");
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
