using UnityEngine;
using System.Collections;

public class g_AIGunScript : MonoBehaviour {

	[SerializeField]
	Vector2 fireRateRange;
	[SerializeField]
	GameObject ProjectilePrefab;
	[SerializeField]
	GameObject MuzzleFlashPrefab;
	[SerializeField]
	GameObject ShellPrefab;
	public Transform FireTransform;
	[SerializeField]
	Transform MuzzleFlashTransform;
	[SerializeField]
	Transform ShellEjectTransform;
	AudioSource sound;
    float m_fireRate;
	float m_currentFire;

	void Start()
	{
		sound = GetComponent<AudioSource> ();
	}

	public void Fire()
	{
		m_currentFire += Time.deltaTime;

		if (m_fireRate <= m_currentFire) 
		{
           // print("Fire Transform: "+FireTransform.position);
            //shoot straight, gun will already be aimed at player
           if (MuzzleFlashPrefab)
           {
               GameObject muzzleFlash;
               muzzleFlash = (GameObject)Instantiate(MuzzleFlashPrefab);
               muzzleFlash.transform.parent = FireTransform;
               muzzleFlash.transform.localPosition = new Vector3(0, 0, 0);
               muzzleFlash.transform.localEulerAngles = new Vector3(0, 0, 0);
               muzzleFlash.SendMessage("Shoot");
               Destroy(muzzleFlash, .1f);
           }
            GameObject projectile = (GameObject)Instantiate (ProjectilePrefab, FireTransform.position, FireTransform.rotation);
            //projectile.transform.parent = FireTransform;
            projectile.GetComponent<g_ProjectileBullet>().bulletSpawnTransform = FireTransform;
            //Debug.Break();


            m_fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
			m_currentFire = 0;
		}
	}
}
