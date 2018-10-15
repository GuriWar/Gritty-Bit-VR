using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class collisionDoubleCheck : MonoBehaviour {

	[SerializeField]
	LayerMask layerMask;
	public List<Transform> m_raycastPoints = new List<Transform>();
	[HideInInspector]
	public bool m_hitSomething = false;
	[HideInInspector]
	public GameObject m_hitObject;
    [HideInInspector]
    public RaycastHit m_rayHit;
	// Update is called once per frame
	void Update () 
	{
		for (int i =0; i < m_raycastPoints.Count; i++)
		{
			Ray ray = new Ray(m_raycastPoints[i].position, m_raycastPoints[i].forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 2f, layerMask)) // ignoring layermask, did we hit something
			{
				m_hitObject = hit.collider.gameObject;
                m_rayHit = hit;
				m_hitSomething = true;
			}
		}
	}
}
