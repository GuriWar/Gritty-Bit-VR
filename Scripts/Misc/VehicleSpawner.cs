using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Spawn a vehicle away from screen and have it fly around so the player can see it
// Have multiple spawn positions
public class VehicleSpawner : MonoBehaviour {
    [SerializeField]
    List<GameObject> Vehicles = new List<GameObject>();
    [SerializeField]
    List<Transform> SpawnPositions = new List<Transform>();

    [SerializeField]
    float spawnRate;
    float currentSpawn;
	void Start()
    {

        for (int i =0; i < SpawnPositions.Count; i++)
        {
            if (SpawnPositions[i].transform.localPosition.y > -6)
            {
                SpawnPositions[i].transform.localPosition = new Vector3(SpawnPositions[i].transform.localPosition.x, -6, SpawnPositions[i].transform.localPosition.z);
            }
            else if (SpawnPositions[i].transform.localPosition.y < -21)
            {
                SpawnPositions[i].transform.localPosition = new Vector3(SpawnPositions[i].transform.localPosition.x, -21, SpawnPositions[i].transform.localPosition.z);
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        currentSpawn += Time.deltaTime;
        if (currentSpawn >= spawnRate)
        {
            Transform mySpawnPosition = SpawnPositions[Random.Range(0, SpawnPositions.Count)];
            GameObject tempVehicle = (GameObject)Instantiate(Vehicles[Random.Range(0, Vehicles.Count)], mySpawnPosition.position, mySpawnPosition.rotation);
            tempVehicle.transform.parent = mySpawnPosition;
            tempVehicle.transform.localEulerAngles = new Vector3(0, 0, 0);

            currentSpawn = 0;
          //  Debug.Break();
        }	
	}
}
