/////////////////////////////////////////////////////////////////////////////////
//
//	vp_HitscanBullet.cs
//	© VisionPunk. All Rights Reserved.
//	https://twitter.com/VisionPunk
//	http://www.visionpunk.com
//
//	description:	a script for hitscan projectiles. this script should be
//					attached to a gameobject with a mesh to be used as the impact
//					decal (bullet hole)
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class g_HitscanBullet : MonoBehaviour
{

	// gameplay
	public bool scaleDamage = false;
	public float Range = 100.0f;			// max travel distance of this type of bullet in meters
	public float Force = 100.0f;			// force applied to any rigidbody hit by the bullet
	public float Damage = 1.0f;				// the damage transmitted to target by the bullet
	public string DamageMethodName = "Damage";  // user defined name of damage method on target
                                                // TIP: this can be used to apply different types of damage, i.e
                                                // magical, freezing, poison, electric
    [SerializeField]
    LayerMask layerMask;

	public float m_SparkFactor = 0.5f;		// chance of bullet impact generating a spark

	public string [] NoDecalOnTheseTags;
	
	protected Transform m_Transform = null;
	protected Renderer m_Renderer = null;
	protected bool m_Initialized = false;
    
	RaycastHit m_Hit;
	GameObject m_bulletHit;
	float m_distanceToTarget;
	[SerializeField]
	GameObject EffectPrefab;

	// these gameobjects will all be spawned at the point and moment
	// of impact. technically they could be anything, but their
	// intended uses are as follows:
	public GameObject m_ImpactPrefab = null;	// a flash or burst illustrating the shock of impact
	public GameObject m_DustPrefab = null;		// evaporating dust / moisture from the hit material
	public GameObject m_SparkPrefab = null;		// a quick spark, as if hitting stone or metal
	public GameObject m_DebrisPrefab = null;	// pieces of material thrust out of the bullet hole and / or falling to the ground
	GameObject m_effect;
	GameObject m_sparkEffect;
	[SerializeField]
	Color m_effectColor;
	void Awake()
	{
	
		m_Transform = transform;
		m_Renderer = GetComponent<Renderer>();	
	}


	/// <summary>
	/// 
	/// </summary>
	void Start()
	{

		m_Initialized = true;
		DoHit();
	
	}



	/// <summary>
	/// 
	/// </summary>
	void OnEnable()
	{
	
		if(!m_Initialized)
			return;
	
		DoHit();
	}


	/// <summary>
	/// everything happens in the DoHit method. the script that
	/// spawns the bullet is responsible for setting its position 
	/// and angle. after being instantiated, the bullet immediately
	/// raycasts ahead for its full range, then snaps itself to
	/// the surface of the first object hit. it then spawns a
	/// number of particle effects and plays a random impact sound.
	/// </summary>
	void DoHit()
	{

		Ray ray = new Ray(transform.position, transform.forward);

        // raycast against all big, solid objects except the player itself
        // SNIPPET: using this instead may be useful in cases where bullets
        // fail to hit colliders (however likely at a performance cost)
        //if (Physics.Linecast(m_Transform.position, m_Transform.position + (m_Transform.forward * Range), out hit, LayerMask))
        if (Physics.Raycast(ray, out m_Hit, Range, layerMask)) // need to do a hitscan from fireposition out
        {

            m_distanceToTarget = (transform.position - m_Hit.point).magnitude;
            m_bulletHit = m_Hit.collider.gameObject;
            m_effect = (GameObject)Instantiate(EffectPrefab, transform.position, transform.rotation);
            Debug.DrawLine(transform.position, m_Hit.point);
            m_effect.GetComponent<g_shootEffect>().SetHitPosition(m_Hit.point, transform.position, transform.rotation);
            m_effect.GetComponent<g_ParticleEffectEditor>().SetColor(m_effectColor);
            
            // move this gameobject instance to the hit object
            Vector3 scale = m_Transform.localScale; // save scale to handle scaled parent objects
            m_Transform.parent = m_Hit.transform;
            m_Transform.localPosition = m_Hit.transform.InverseTransformPoint(m_Hit.point);
            m_Transform.rotation = Quaternion.LookRotation(m_Hit.normal);                   // face away from hit surface


            //more range is more damage
            if (scaleDamage)
                Damage -= (m_distanceToTarget / Range) * 80f;
            if (Damage < 0)
                Damage = 0;
            if (m_Hit.transform.lossyScale == Vector3.one)                              // if hit object has normal scale
                m_Transform.Rotate(Vector3.forward, Random.Range(0, 360), Space.Self);  // spin randomly
            else
            {
                // rotated child objects will get skewed if the parent object has been
                // unevenly scaled in the editor, so on scaled objects we don't support
                // spin, and we need to unparent, rescale and reparent the decal.
                m_Transform.parent = null;
                m_Transform.localScale = scale;
                m_Transform.parent = m_Hit.transform;
            }
            //Debug.Break();
            // if hit object has physics, add the bullet force to it
            if (m_Hit.rigidbody != null)
                m_Hit.rigidbody.AddForceAtPosition((ray.direction * Force), m_Hit.point, ForceMode.Impulse);

            // spawn impact effect
            if (m_ImpactPrefab != null)
                Instantiate(m_ImpactPrefab, m_Transform.position, m_Transform.rotation);

            // spawn dust effect
            if (m_DustPrefab != null)
                Instantiate(m_DustPrefab, m_Transform.position, m_Transform.rotation);

            // spawn spark effect
            if (m_SparkPrefab != null)
            {
                if (Random.value < m_SparkFactor)
                {
                    m_sparkEffect = (GameObject)Instantiate(m_SparkPrefab, m_Transform.position, m_Transform.rotation);
                }
            }

            // spawn debris particle fx
            if (m_DebrisPrefab != null)
                Instantiate(m_DebrisPrefab, m_Transform.position, m_Transform.rotation);

            m_Hit.collider.SendMessage(DamageMethodName, Damage, SendMessageOptions.DontRequireReceiver);

            for (int i = 0; i < NoDecalOnTheseTags.Length; i++)
            {
                if (m_Hit.collider.gameObject.tag == NoDecalOnTheseTags[i])
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {

             m_effect = (GameObject)Instantiate(EffectPrefab, transform.position, transform.rotation);
             m_effect.GetComponent<g_shootEffect>().SetHitPosition(transform.position + transform.forward * 100, transform.position, transform.rotation);
             m_effect.GetComponent<g_ParticleEffectEditor>().SetColor(m_effectColor);

            Destroy(gameObject);
        }
	}
}

