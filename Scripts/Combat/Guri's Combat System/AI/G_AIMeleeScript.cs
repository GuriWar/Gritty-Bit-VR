using UnityEngine;
using System.Collections;

public class G_AIMeleeScript : MonoBehaviour 
{
	[SerializeField]
	float m_meleeRange;
	[SerializeField]
	float m_damage;
	[SerializeField]
	float m_attackRate;
	float m_currentAttackTime;
	// Update is called once per frame
	void Update () 
	{
		m_currentAttackTime += Time.deltaTime;
		if (m_currentAttackTime >= m_attackRate && GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance <= m_meleeRange)
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
			DoMeleeAttack();
			GetComponent<g_AIAnimationScript>().StopRunAnimation();

		}
		else if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance >= m_meleeRange)
		{
			GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3.5f;
			//GetComponent<g_AIAnimationScript>().PlayRunAnimation();
		}
	}

	void DoMeleeAttack()
	{
		GetComponent<g_AISoundScript> ().PlayAttackSound ();
		GetComponent<g_AIAnimationScript>().PlayAttackAnimation();
		GetComponent<g_AIBehaviourScript>().m_attackTarget.SendMessageUpwards("Damage", m_damage, SendMessageOptions.DontRequireReceiver);
		m_currentAttackTime = 0;
	}
		
}
