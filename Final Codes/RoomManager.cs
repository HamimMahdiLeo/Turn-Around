using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int roomID;
    public int spawnCount;

    private void Start()
    {
        SetSpawnCount();
    }

    void SetSpawnCount()
    {
        switch (roomID)
        {
            case 1: // Power Room
                spawnCount = 1;
                break;

            case 2: // IT Room
                spawnCount = 2;
                break;

            case 3: // Store Room
                spawnCount = 3;
                break;

            case 4: // Security Room
                spawnCount = 4;
                break;
				
			case 5: //Office Room
				spawnCount = 0;
				break;
				
			case 6: //Maze
				spawnCount = 5;
				break;
        }

        Debug.Log("Room " + roomID + " Spawn Count: " + spawnCount);
    }
}
