using UnityEngine;
using System.Collections;

public class GruntDeathExplosion : MonoBehaviour {

	Animator m_myAnimator;
	bool m_spawned = false;
	[SerializeField]
	GameObject m_ExplosionPrefab;
	void Start()
	{
		m_myAnimator = GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () 
	{
		if (!m_spawned)
		{
		if (!m_myAnimator.enabled)
			{
				Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
				Destroy(gameObject);
			}
		}
	}
}
