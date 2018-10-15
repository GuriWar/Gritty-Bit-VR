using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * Manages the agent's health. 
 * Will trigger the suppresion state on agents using cover if shields are down.
 * */

public class g_CompanionHealthScript : MonoBehaviour {

	public float health = 100;

	[SerializeField]
	public List <Rigidbody> rigidbodies;// make this a list instead
	public Animator animator;
	private bool beenHitYetThisFrame = false;
	g_CompanionAnimationScript animationScript;
	bool disabled;
	[SerializeField]
	float disabledTime;
	float currentDisabledTime;
	void Start()
	{
		animationScript = GetComponent<g_CompanionAnimationScript>();
	

	}
	void Update()
	{
		if (disabled)
			currentDisabledTime += Time.deltaTime;

		if (currentDisabledTime >= disabledTime)
			disabled = false;

		if (!disabled)
		{
			health = 100;

		}
		//Only let us take explosion damage once per frame. (could also be used for weapons that would pass through an agent's body)
		//This will prevent the agent from taking the damage multiple times- once for each hitbox.
		beenHitYetThisFrame = false;

	}

	public void Damage(float damage)
	{	
		ReduceHealthAndShields(damage);

		if(health <= 0)
		{
			DeathCheck();
		}
		else
		{
			animationScript.PlayBattleTakeDamageAnimation();
		}
	}

	public IEnumerator SingleHitBoxDamage(float damage)
	{

		//Only let us take explosion damage once per frame. (could also be used for weapons that would pass through an agent's body)
		//This will prevent the agent from taking the damage multiple times- once for each hitbox.
		if(!beenHitYetThisFrame)
		{
			ReduceHealthAndShields(damage);

			if(health <= 0)
			{
				DeathCheck();
			}
			beenHitYetThisFrame = true;	
		}

		yield return null;
		beenHitYetThisFrame = false;
	}


	public void ReduceHealthAndShields(float damage)
	{
		//Shields are mandatory for the suppressioon mechanic to work.
		//However, as you may not want the agent to have any sort of regenerating health, you can choose whether or not they will actually block damage or merely work as a recent damage counter.

		health -= damage;
		//Sound
		//if(soundScript)
		//soundScript.PlayDamagedAudio();					

	}



	//Check to see if we are dead.
	void DeathCheck()
	{
		KillAI();
	}

	void KillAI()
	{
		animationScript.PlayBattleDie();
		disabled = true;
		if (animationScript)
		{
			animationScript.enabled = false;

		}

		//Enabe the ragdoll
		for(int i = 0; i < rigidbodies.Count; i++)
		{
			rigidbodies[i].isKinematic = false;
		}				
	}
}