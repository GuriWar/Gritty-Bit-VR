using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

/*
 * Manages the agent's health. 
 * Will trigger the suppresion state on agents using cover if shields are down.
 * */

public class g_AIHealthScript : MonoBehaviour {

	public float health = 100;

	[SerializeField]
	public List <Rigidbody> rigidbodies;// make this a list instead
    [SerializeField]
    public List<Collider> colliders;// make this a list instead
    public Animator animator;
	private bool beenHitYetThisFrame = false;
	public Transform m_gun;
	public enum DeathStyles{dissolve, explode, done};
	public DeathStyles onDeath;
	[SerializeField]
	GameObject m_explosionEffect;
    [SerializeField]
    GameObject popUpScore;
    [SerializeField]
    GameObject popUpCombo;
	g_AIBehaviourScript behaviourScript;
	g_AIAnimationScript animationScript;
	g_AIGunScript gunScript;
	g_AISoundScript soundScript;
	g_AISetCoverNode coverScript;
	g_AIRotateToTarget rotateScript;
	CustomDissolve dissolveScript;
	G_AIMeleeScript meleeScript;
	NavMeshAgent navmesh;
    [SerializeField]
    HolographicEditor mesh;
    [SerializeField]
    bool kill = false;
    bool dead = false;
    [HideInInspector]
    public float maxHealth;
    Color meshColor;
	void Start()
	{
		behaviourScript = GetComponent<g_AIBehaviourScript>();
		animationScript = GetComponent<g_AIAnimationScript>();
		gunScript = GetComponent<g_AIGunScript>();
		soundScript = GetComponent<g_AISoundScript>();
		coverScript = GetComponent<g_AISetCoverNode>();
		rotateScript = GetComponent<g_AIRotateToTarget>();
		navmesh = GetComponent<NavMeshAgent>();
		dissolveScript = GetComponent<CustomDissolve>();
		meleeScript = GetComponent<G_AIMeleeScript>();
        maxHealth = health;
	}
	void Update()
	{
		//Only let us take explosion damage once per frame. (could also be used for weapons that would pass through an agent's body)
		//This will prevent the agent from taking the damage multiple times- once for each hitbox.
		beenHitYetThisFrame = false;
        mesh.ChangeHologramColor(new Color((255 - 255 * health / maxHealth)/255, 0,(255 * health / maxHealth)/255, 1));
        if (kill)
        {
            KillAI();
        }
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
            if (animationScript)
			animationScript.PlayGotShotAnimation();
			soundScript.PlayPainSound();
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
        if (dead)
            return;
        dead = true;
        mesh.ChangeHologramColor(new Color(1, 0, 0)) ;
        GameObject.Find("Lights").GetComponent<g_LightManager>().ChangeToKillColour();
        float score = GetComponent<scoreWorth> ().points;
		float timeToEnable = score / 2f;
		if (timeToEnable > 4f)
			timeToEnable = 4f;
		//needs to be a time limit
        if (animationScript)
		animationScript.PlayDieAnimation();

		if (soundScript)
		{
			soundScript.PlayDeathSound();
            Destroy(soundScript);
		}
		if (behaviourScript)
		{
			behaviourScript.enabled = false;
		}
		if (animationScript)
		{
			animationScript.enabled = false;
		}
		if (gunScript)
		{
			gunScript.enabled = false;
		}
		if (rotateScript)
		{
			rotateScript.enabled = false;
		}
		if (navmesh)
		{
			navmesh.enabled = false;
		}
		if (meleeScript)
		{
			meleeScript.enabled = false;
		}
		//Enable the ragdoll
		for(int i = 0; i < rigidbodies.Count; i++)
		{
			rigidbodies[i].isKinematic = false;
		}
			
        for (int i =0; i < colliders.Count; i++)
        {
            Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), colliders[i]);
        }
        if (animator)
			animator.enabled = false;				

		if (m_gun)
		{
			m_gun.parent = null;
			m_gun.GetComponent<Rigidbody> ().isKinematic = false;
			m_gun.GetComponent<Collider> ().enabled = true;
			Destroy(m_gun.gameObject, 2f);

		}
        popUpScore.SetActive(true);
        popUpCombo.SetActive(true);
		GameObject.Find("Player").GetComponent<scoreTracker>().AddKill(GetComponent<scoreWorth>().points);
		GameObject.Find("Player").GetComponent<PlayerMoneyScript>().AddMoney(GetComponent<scoreWorth>().points);
        if (behaviourScript.m_coverTarget != null)
        {
            behaviourScript.m_coverTarget.GetComponent<g_CoverSpot>().taken = false;
        }
        if (GetComponent<g_AIID>().ID == 0)
        GameObject.Find("CoverSpots").GetComponent<CoverManager>().AddCoverSpot(behaviourScript.m_coverTarget);
        //GameObject.Find ("ScoreUI").GetComponent<enableOnTime> ().Enable (timeToEnable);
        if (onDeath == DeathStyles.dissolve)
        {
         //   dissolveScript.Dissolve();
        }
        else if (onDeath == DeathStyles.explode)
        {
            Instantiate(m_explosionEffect, transform.position + new Vector3(0, 1, 0), transform.rotation);
            TransformDeepChildExtension.FindDeepChild(transform, "Mesh").gameObject.SetActive(false);
        }
        if (GameObject.Find("Enemy Manager") != null)
        GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>().KillEnemy(gameObject);
        Destroy(this);
		if (gameObject != null)
		Destroy(gameObject, 2f);

	}
}