using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class g_shootEffect : MonoBehaviour 
{
	[SerializeField]
	float m_effectSpeed;
	[SerializeField]
	float m_fadeOutSpeed;
	[SerializeField]
	float m_timeToDestroy;
	Light light;
	List<GameObject> ParticleEffects = new List<GameObject>();
 	
	Vector3 m_hitPosition;
	Vector3 m_startPosition;

	void Start()
	{
		foreach(Transform child in transform)
		{
			if (child.GetComponent<Light>())
			{
				light = child.GetComponent<Light>();
			}
			if (child.GetComponent<ParticleSystem>())
			{
				ParticleEffects.Add(child.gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector3.MoveTowards(transform.position, m_hitPosition, Time.deltaTime*m_effectSpeed);

		if (transform.position == m_hitPosition)
		{
			for (int i =0; i <ParticleEffects.Count; i ++)
			{
				Color tempColor = ParticleEffects[i].GetComponent<Renderer>().material.GetColor("_TintColor");
				tempColor.a -= Time.deltaTime*m_fadeOutSpeed;
				ParticleEffects[i].GetComponent<Renderer>().material.SetColor("_TintColor", tempColor);
			}
			light.intensity -= Time.deltaTime*m_fadeOutSpeed;
			if (light.intensity<=0)
			Destroy(gameObject, m_timeToDestroy);
			//fade out effect
		}
	}

	public void SetHitPosition(Vector3 x, Vector3 y, Quaternion rotation)
	{
		//get position of bullethit and fire effect from fire position to bullethit
		//use the projectile we currently have
		m_hitPosition = x;
		m_startPosition = y;

	}
}
