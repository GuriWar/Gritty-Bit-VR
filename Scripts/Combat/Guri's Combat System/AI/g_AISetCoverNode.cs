using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class g_AISetCoverNode : MonoBehaviour 
{
	CoverManager m_cover;
    [SerializeField]
    List<GameObject> m_coverSpots = new List<GameObject>();

    public void SetUpCoverNodes()
    {
        m_cover = GameObject.Find("CoverSpots").GetComponent<CoverManager>();
        m_coverSpots = m_cover.m_coverSpots;
    }
    public void FindCloseCover()
	{
		//get distance to all cover spots and set covertransform to closest one
		float distanceToCover;
		float min = Mathf.Infinity;
		GameObject currentClosest = null;

        for (int i =0; i < m_coverSpots.Count; i++)
		{
            distanceToCover = (transform.position - m_coverSpots[i].transform.position).magnitude;
			//cant be closer than a certain number
			if (distanceToCover < min)
			{
				currentClosest = m_coverSpots[i];
				min = distanceToCover;
			}
		}
        currentClosest.GetComponent<g_CoverSpot>().taken = true;
        m_cover.RemoveCoverSpot(currentClosest);
        GetComponent<g_AIBehaviourScript>().m_coverTarget = currentClosest;
	}
}
