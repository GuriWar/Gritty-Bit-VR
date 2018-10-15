using UnityEngine;
using System.Collections;

/*
 * Moves an object until it hits a target, at which time it calls the Damage(float) method on all scripts on the hit object
 * Can also home in on targets and "detonate" when in close proximity.
 * */

    public class g_ProjectileBullet : MonoBehaviour
    {
	public Transform bulletSpawnTransform;
    public float speed = 1;
    public LayerMask layerMask;
	public Vector2 maxLifeTime;
    //Bullet Stuff
    public float damage = 16;

    public float bulletForce = 100;

	public float m_inaccuracy;
	private Vector3 m_inaccuracyVector;
		 
    private GameObject m_bulletHit;

    public float m_SparkFactor = 1f;		// chance of bullet impact generating a spark

       //Hit Effects
    public GameObject m_HitEnemyImpact = null;	// a flash or burst illustrating the shock of impact
    public GameObject m_DustPrefab = null;		// evaporating dust / moisture from the hit material
    public GameObject m_SparkPrefab = null;		// a quick spark, as if hitting stone or metal
    public GameObject m_DebrisPrefab = null;	// pieces of material thrust out of the bullet hole and / or falling to the ground
	Quaternion rotTarget;
    RaycastHit hit;
     
	void Start()
     {
		m_inaccuracyVector = new Vector3 (Random.Range(-m_inaccuracy, m_inaccuracy), Random.Range(-m_inaccuracy, m_inaccuracy), 0);		 
        
		if (bulletSpawnTransform == null)
		{
			bulletSpawnTransform = transform;
		}
        transform.position = bulletSpawnTransform.position;
        transform.rotation = bulletSpawnTransform.rotation;
        if (m_inaccuracy > 0)
        {
            rotTarget = Quaternion.LookRotation(bulletSpawnTransform.forward + m_inaccuracyVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, 10000000);
        }

        StartCoroutine(SetTimeToDestroy());
		//Debug.Break();
	}

        //Automatically destroy the bullet after a certain amount of time
        //Especially useful for missiles, which may end up flying endless circles around their target,
        //long after the appropriate sound effects have ended.
        IEnumerator SetTimeToDestroy()
        {
			yield return new WaitForSeconds(Random.Range(maxLifeTime.x, maxLifeTime.y));

            Destroy(gameObject);
        }

        IEnumerator ApplyDamage()
        {

            this.enabled = false;
            yield return null;
			if (m_bulletHit != null)
			{
				if (m_bulletHit.GetComponent<Joint>() != null)
				m_bulletHit.GetComponent<Joint> ().enablePreprocessing = false;
            //Linger around for a while to let the trail renderer dissipate (if the bullet has one.)

            if (m_bulletHit.tag != "Player")
            {
                if (m_bulletHit.tag != "Weapon")
                {
                    SpawnEffects();
                    m_bulletHit.SendMessageUpwards("Damage", damage, SendMessageOptions.DontRequireReceiver);
                    Destroy(gameObject);
                }
                else
                {
                    SpawnEffects();
                    Destroy(gameObject);
                }
            }
            else
            {
                m_bulletHit.SendMessageUpwards("Damage", damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }

            if (hit.rigidbody)
                hit.rigidbody.AddForceAtPosition(transform.forward * bulletForce, hit.point, ForceMode.Impulse);
        }
			Destroy(gameObject);

		}

        // Update is called once per frame
        void Update()
        {
			if (GetComponent<collisionDoubleCheck>().m_hitSomething)
			{
				m_bulletHit = GetComponent<collisionDoubleCheck>().m_hitObject;
                hit = GetComponent<collisionDoubleCheck>().m_rayHit;
				StartCoroutine(ApplyDamage());
			}
        Move();
       // transform.parent = null;
    }
    void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void SpawnEffects()
		{
			if (m_HitEnemyImpact != null)
			{
				Instantiate(m_HitEnemyImpact, transform.position, transform.rotation);
			}
			// spawn dust effect
			if (m_DustPrefab != null)
				Instantiate(m_DustPrefab, transform.position, transform.rotation);
		
			// spawn spark effect
			if (m_SparkPrefab != null)
			{
				if (Random.value < m_SparkFactor)
					Instantiate(m_SparkPrefab, transform.position, transform.rotation);
			}
		
			// spawn debris particle fx
			if (m_DebrisPrefab != null)
				Instantiate(m_DebrisPrefab, transform.position, transform.rotation);
		}
    }
