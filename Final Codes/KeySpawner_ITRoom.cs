using UnityEngine;

public class KeySpawner_ITRoom : MonoBehaviour
{
    public GameObject keyPrefab; // Assign your key prefab in inspector
    public Transform[] spawnPoints; // Assign spawn points in inspector

    private GameObject currentKey;

    void Start()
    {
        SpawnKeyOnce();
    }

    void SpawnKeyOnce()
    {
        if (keyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("IT Room KeySpawner is missing keyPrefab or spawnPoints.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        currentKey = Instantiate(keyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Debug.Log("IT Room key spawned at: " + spawnPoints[randomIndex].name);
    }
}
