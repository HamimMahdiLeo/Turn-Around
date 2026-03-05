using UnityEngine;

public class PlayerMoneyPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    private bool hasMoney = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !hasMoney)
        {
            TryPickupMoney();
        }
    }

    void TryPickupMoney()
    {
        GameObject money = GameObject.FindGameObjectWithTag("Money");

        if (money != null)
        {
            float distance = Vector3.Distance(transform.position, money.transform.position);

            if (distance <= pickupRange)
            {
                hasMoney = true;
                Destroy(money);

                Debug.Log("Money picked up.");
            }
            else
            {
                Debug.Log("Too far from money.");
            }
        }
    }

    public bool HasMoney()
    {
        return hasMoney;
    }

    public void UseMoney()
    {
        hasMoney = false;
        Debug.Log("Money used.");
    }
}