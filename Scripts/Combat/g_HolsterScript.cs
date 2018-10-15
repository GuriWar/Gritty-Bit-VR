using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class g_HolsterScript : MonoBehaviour {
    [SerializeField]
    g_ObjectManager ObjectManager;
    [SerializeField]
    gameState gameStateScript;
    [SerializeField]
    float refreshTime;
    float currentRefreshTime;
    [SerializeField]
    GameObject WeaponPrefab;
    [SerializeField]
    Slider clipSlider;
    GameObject gun;
    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    Vector3 spawnRotation;
    bool refreshing = true;

    void Start()
    {
        SpawnGun();
    }
    // Update is called once per frame
    void Update ()
    {
		if (refreshing)
        {
            currentRefreshTime += Time.deltaTime;
            if (currentRefreshTime >= refreshTime)
            {
                SpawnGun();
            }
        }
	}

    void SpawnGun()
    {
        gun = (GameObject)Instantiate(WeaponPrefab, transform.position, transform.rotation);
        gun.transform.parent = transform;
        gun.transform.localEulerAngles = spawnRotation;
        gun.transform.localPosition = spawnPosition;
        gun.GetComponent<Rigidbody>().isKinematic = true;
        gun.GetComponent<ObjectState>().currentState = ObjectState.ObjectStates.Holstered;
        if (gun.GetComponent<gunShooter>()!= null)
            gun.GetComponent<gunShooter>().laser.enabled = false;
        if (gun.GetComponent<GunAmmo>())
        {
            gun.GetComponent<GunAmmo>().slider = clipSlider;
            gun.GetComponent<GunAmmo>().CalculateInitialAmmo();
        }
        ObjectManager.m_Objects.Add(gun);
        refreshing = false;
    }

    public void BeginRefresh()
    {
        if (gameStateScript.playerState != gameState.GameStates.Dead)
        {
            gun = null;
            currentRefreshTime = 0;
            refreshing = true;
        }
    }
}
