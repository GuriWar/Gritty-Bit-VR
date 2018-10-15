using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class g_HologramWeaponHolder : MonoBehaviour {
	[SerializeField]
	GameObject[] weapons;
	[HideInInspector]
	public GameObject m_currentWeapon;
    [SerializeField]
    Vector3 weaponRotation;
    //put in weapon prefabs
    enum Weapons
    {
        None,
        Pistol,
        Shotgun,
        AutoPistol,
        Grenade
    }
    Weapons currentlySpawning;
    void Start()
    {
        currentlySpawning = Weapons.None;
    }
   
	public void ClearWeapon()
	{
		//if (m_currentWeapon != null)
		//{
        //    if (m_currentWeapon.GetComponent<weaponState>().currentState == weaponState.WeaponStates.Hologram)
        //    {
        //        Destroy(m_currentWeapon);
        //    }
        //    else if (m_currentWeapon.GetComponent<weaponState>().currentState == weaponState.WeaponStates.Held)
        //        m_currentWeapon = null;
		//}
	}

	public void SpawnWeapon(int weaponIndex)
	{
        //print("Once");
		ClearWeapon ();
		m_currentWeapon = (GameObject)Instantiate (weapons [weaponIndex], transform.position, transform.rotation);
		m_currentWeapon.transform.parent = transform;
        m_currentWeapon.transform.localEulerAngles = weaponRotation;
		//m_currentWeapon.name = m_currentWeapon.GetComponent<weaponIdentifier>().gunName;
		m_currentWeapon.GetComponent<Rigidbody> ().isKinematic = true;
        m_currentWeapon.GetComponent<g_HologramToPhysical>().weaponReplacement = this;
		if (m_currentWeapon.GetComponent<Animator>())
		m_currentWeapon.GetComponent<Animator> ().enabled = true;
		//m_currentWeapon.GetComponent<weaponState> ().currentState = weaponState.WeaponStates.Hologram;
	}
    public void SpawnWeapon(GameObject weapon)
    {
        //print("Once");
        ClearWeapon();
        m_currentWeapon = (GameObject)Instantiate(weapon, transform.position, transform.rotation);
        m_currentWeapon.transform.parent = transform;
        m_currentWeapon.transform.localEulerAngles = weaponRotation;
        //m_currentWeapon.name = m_currentWeapon.GetComponent<weaponIdentifier>().gunName;
        m_currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        m_currentWeapon.GetComponent<g_HologramToPhysical>().weaponReplacement = this;
        if (m_currentWeapon.GetComponent<Animator>())
            m_currentWeapon.GetComponent<Animator>().enabled = true;
        //m_currentWeapon.GetComponent<weaponState>().currentState = weaponState.WeaponStates.Hologram;
    }

}
