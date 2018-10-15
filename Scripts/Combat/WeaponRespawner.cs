using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeaponRespawner : MonoBehaviour 
{
	[SerializeField]
	List<GameObject> WeaponPrefabs  = new List<GameObject>();
	[SerializeField]
	Transform gunSpawnTransform;
	GameObject tempWeapon;
	[SerializeField]
	float spawnRate;
	float currentSpawnTime;
	bool weaponPresent = false;
	// Update is called once per frame
	void Update () 
	{
		SpawnTime();
	}

	void SpawnTime()
	{
		currentSpawnTime += Time.deltaTime;

		if (spawnRate <= currentSpawnTime)
		{
			//spawn random weapon
			SpawnWeapon(WeaponPrefabs[Random.Range(0,3)]);
		}

	}

	void SpawnWeapon(GameObject weapon)
	{
		tempWeapon = (GameObject)Instantiate(weapon, gunSpawnTransform.position, gunSpawnTransform.rotation);
		//tempWeapon.name = tempWeapon.GetComponent<weaponIdentifier>().gunName;
		tempWeapon.GetComponent<CustomDissolve>().Undissolve();
		currentSpawnTime = 0;

	}
}
