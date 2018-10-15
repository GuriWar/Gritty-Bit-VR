using UnityEngine;
using System.Collections;

/*
 * Moves an object until it hits a target, at which time it calls the Damage(float) method on all scripts on the hit object
 * Can also home in on targets and "detonate" when in close proximity.
 * */

public class MeleeWeapon : MonoBehaviour
{
	public LayerMask layerMask;

	public float damage = 16;
	public float m_SparkFactor = 0.5f;		// chance of bullet impact generating a spark

	GameObject m_meleeHit;
	[SerializeField]
	float m_hitforce = 1;

	void Start()
	{
	}


	IEnumerator ApplyDamage()
	{

		m_meleeHit.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);

		//Wait a fram to apply forces as we need to make sure the thing is dead
		if (m_meleeHit != null)
		{
			if (m_meleeHit.GetComponent<Rigidbody> () != null) 
			{
				m_meleeHit.GetComponent<Rigidbody> ().AddForceAtPosition (transform.forward * m_hitforce, m_meleeHit.transform.position, ForceMode.Impulse);

			}
		}

		yield return true;
	}
		


	void OnTriggerEnter(Collider other)
	{
		RaycastHit hit;
		print(other.name);
		if(Physics.Linecast(transform.position, other.transform.position, out hit, layerMask)) // ignoring layermask, did we hit something
		{
			print(other.name);
			m_meleeHit = other.gameObject;
			StartCoroutine(ApplyDamage());
		}
	}
}
