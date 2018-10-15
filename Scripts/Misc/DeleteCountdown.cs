using UnityEngine;
using System.Collections;

public class DeleteCountdown : MonoBehaviour {

	[SerializeField]
	float m_LifeTime;

	float m_LifeSpan;


	
	// Update is called once per frame
	void Update () 
	{
		m_LifeSpan += Time.deltaTime;
		if (m_LifeSpan>= m_LifeTime)
			Destroy(gameObject);
	}
}
