using UnityEngine;

public class MannequinSpawner : MonoBehaviour
{
    public GameObject MannequinPrefab; // Assign your key prefab in inspector
    public Transform[] spawnPoints; // Assign spawn points in inspector

    private GameObject currentMannequin;

    void Start()
    {
        SpawnMannequinOnce();
    }

    void SpawnMannequinOnce()
    {
        if (MannequinPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawner is missing prefab or spawn points.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        currentMannequin = Instantiate(MannequinPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Debug.Log("Mannequin spawned at: " + spawnPoints[randomIndex].name);
    }
}
