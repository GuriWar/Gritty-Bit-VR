using UnityEngine;
using System.Collections;

public class GrenadeThrow : MonoBehaviour {

	public float m_throwForce;
	public void ThrowGrenade(Vector3 velocity)
	{
		transform.parent = null;
		GetComponent<g_GrenadeScript>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, velocity.y, velocity.z) * m_throwForce);
        GetComponent<TrailRenderer>().enabled = true;
		GetComponent<Rigidbody> ().AddTorque (transform.right * 300f);
		Destroy(GetComponent<weaponIdentifier>());
	}

}
