using UnityEngine;
using UnityEngine.AI;

public class MazeMannequinSpawner : MonoBehaviour
{
    public GameObject MannequinPrefab;
    public Transform[] spawnPoints;

    void Start()
	{
		Debug.Log("MazeMannequinSpawner STARTED");
		Debug.Log("SpawnPoints count: " + spawnPoints.Length);
		SpawnAllMannequins();
	}

    void SpawnAllMannequins()
    {
        foreach (Transform point in spawnPoints)
        {
            NavMeshHit hit;

            if (NavMesh.SamplePosition(point.position, out hit, 2f, NavMesh.AllAreas))
            {
                Instantiate(MannequinPrefab, hit.position, Quaternion.identity);
                Debug.Log("Maze mannequin spawned at: " + point.name);
            }
            else
            {
                Debug.LogWarning("No NavMesh found near: " + point.name);
            }
        }
    }
}