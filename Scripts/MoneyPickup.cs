using UnityEngine;

public class MoneyPickup_SecurityRoom : MonoBehaviour
{
    public GameObject moneyObject; // Assign the money object in Inspector
    public bool moneyTaken = false; // Prevent multiple pickups

    void Update()
    {
        if (!moneyTaken && Input.GetKeyDown(KeyCode.M))
        {
            TakeMoney();
        }
    }

    void TakeMoney()
    {
        if (moneyObject != null)
        {
            moneyObject.SetActive(false); // Hide the money object
            moneyTaken = true;
            Debug.Log("Player has taken the money in the Security→Office sequence!");

            // Optional: trigger next event, like the final Hallway chase
            // Example: GameManager.Instance.StartFinalChase();
        }
        else
        {
            Debug.LogWarning("Money object not assigned in Inspector!");
        }
    }
}
