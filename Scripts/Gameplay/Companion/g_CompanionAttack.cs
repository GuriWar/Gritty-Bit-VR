using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class g_CompanionAttack : MonoBehaviour 
{
	List<GameObject> m_AllEnemies = new List<GameObject>();
	GameObject enemyToShoot;
	[SerializeField]
	GameObject ProjectilePrefab;
	[SerializeField]
	float m_fireRate;
	[SerializeField]
	Transform[] FirePositions;
	float m_currentFireTime;
	int currentFirePositionIndex;
	GameObject m_projectile;
	[HideInInspector]
	public bool attacking;
	// Update is called once per frame
	void Update () 
	{
		m_AllEnemies.Clear();
		m_AllEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
		for (int i =0; i < m_AllEnemies.Count; i++)
		{
			if (m_AllEnemies[i] != null)
			{
				if(m_AllEnemies[i].GetComponent<g_AIBehaviourScript>() == null)
				{
					m_AllEnemies.Remove(m_AllEnemies[i]);
				}
			}
		}

		if (m_AllEnemies.Count > 0)
		{
			if (enemyToShoot == null)
			{
				attacking = false;
				FindNewTarget();
			}
			else
			{
				attacking = true;

				AttackTarget();
			}
		}

	}

	void AttackTarget()
	{
		GetComponent<g_CompanionAnimationScript>().PlayBattleIdleAnimation();

		GetComponent<g_CompanionAnimationScript>().PlayAttackAnimation();
		m_currentFireTime += Time.deltaTime;
		transform.LookAt(enemyToShoot.transform.position);
		//get ready to fire
		//do animationscript
		if (m_currentFireTime >= m_fireRate)
		{
			
			m_projectile = (GameObject)Instantiate(ProjectilePrefab, FirePositions[currentFirePositionIndex], FirePositions[currentFirePositionIndex]);
			m_projectile.GetComponent<g_ProjectileBullet>().bulletSpawnTransform = FirePositions[currentFirePositionIndex];
			currentFirePositionIndex ++;
			if (currentFirePositionIndex > 3)
				currentFirePositionIndex = 0;
			m_currentFireTime = 0;
		}
	}

	void FindNewTarget()
	{
		GetComponent<g_CompanionAnimationScript>().StopBattleIdleAnimation();
		float min = Mathf.Infinity;
		float distanceToTarget;
		for (int i =0; i < m_AllEnemies.Count; i++)
		{
			//need to find the closest target
			distanceToTarget = (transform.position - m_AllEnemies[i].transform.position).magnitude;
			//cant be closer than a certain number
			if (distanceToTarget < min)
			{
				enemyToShoot = m_AllEnemies[i];
				min = distanceToTarget;
			}
		}
	}
}
