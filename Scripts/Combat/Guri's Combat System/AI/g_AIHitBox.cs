using UnityEngine;
using System.Collections;

/*
 * This script takes damage from various body parts, multiplies it, and passes it onto the Health Script.
 * 
 * */


	public class g_AIHitBox : MonoBehaviour
	{

		public float damageMultiplyer = 1;
		private Vector3 addForceVector;

		public g_AIHealthScript myScript;
		public bool canDoSingleHealthBoxDamage = true;

		[HideInInspector]
		public float damageTakenThisFrame = 0;

		void Start()
		{
			GetComponent<Rigidbody> ().isKinematic = true;
			if (GetComponent<Rigidbody>())
				myScript.rigidbodies.Add (GetComponent<Rigidbody>());
        if (GetComponent<Collider>())
            myScript.colliders.Add(GetComponent<Collider>());
		}

		public void Damage(float damage)
		{
			if (myScript)
			{
				//Use the multiplier to take differing amounts of damage depending on where the AI is hit
				damage = damage * damageMultiplyer;
				if (GetComponent<headShotMultiplier> ()) 
				{
                    GetComponent<headShotMultiplier> ().HeadShot ();
                    //GetComponent<headShotMultiplier> ().MultiplyScore ();
                }
            if (myScript)
					myScript.Damage(damage);
			}
		}

		//Use for explosives
		public void SingleHitBoxDamage(float damage)
		{
			//We don't do the damage multiplier here because this is used for explosions, and we  don't want to leave it up to RNG which hitbox is used first
			if (myScript)
			{
				if (canDoSingleHealthBoxDamage)
					StartCoroutine(myScript.SingleHitBoxDamage(damage));
				else
					myScript.Damage(damage);
			}
		}
	}

