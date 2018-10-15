using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class g_CompanionMovement : MonoBehaviour {

	public enum states{followPlayer, goToPosition,idle};
	bool findNewPosition= true;
	[SerializeField]
	float m_moveSpeed;
	[SerializeField]
	Vector3 m_minOffsetFromPlayer;
	[SerializeField]
	Vector3 m_maxOffsetFromPlayer;
	Vector3 m_positionToMoveTo;
	Vector3 m_positionLastFrame;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (findNewPosition)
		{
			FindNewMovePosition();
		}
		DriftAround ();
	}

	void FindNewMovePosition()
	{
		m_positionToMoveTo = GameObject.Find ("Player").transform.position 
			+ new Vector3(Random.Range(m_minOffsetFromPlayer.x, m_maxOffsetFromPlayer.x), 
				Random.Range(m_minOffsetFromPlayer.y, m_maxOffsetFromPlayer.y), 
				Random.Range(m_minOffsetFromPlayer.z, m_maxOffsetFromPlayer.z));
		findNewPosition = false;
	}

	void DriftAround()
	{
		//transform.LookAt (GameObject.Find ("Player").transform);

		//smoothly interpolate to a position behind the player
		transform.position = Vector3.MoveTowards(m_positionLastFrame,	m_positionToMoveTo, Time.deltaTime*m_moveSpeed);
		m_positionLastFrame = transform.position;

		if ((transform.position - m_positionToMoveTo).magnitude < 1f)
		{
			findNewPosition = true;
		}
	}

}
