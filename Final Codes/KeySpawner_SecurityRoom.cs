using UnityEngine;

public class KeySpawner_SecurityRoom : MonoBehaviour
{
    public GameObject keyPrefab; 
    public Transform[] spawnPoints;

    private GameObject currentKey;
    private bool hasSpawned = false;

    void Start()
    {
        // Do NOT spawn at start anymore
    }

    public void SpawnKeyAfterCamerasActivated()
    {
        if (hasSpawned) return;

        if (keyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Security Room KeySpawner is missing keyPrefab or spawnPoints.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        currentKey = Instantiate(keyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);

        hasSpawned = true;

        Debug.Log("All cameras activated. Key spawned at: " + spawnPoints[randomIndex].name);
    }
}
