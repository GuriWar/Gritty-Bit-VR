using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour {
    [SerializeField]
    List<GameObject> CoverGroup = new List<GameObject>();

    public List<GameObject> m_coverSpots = new List<GameObject>();
    // Use this for initialization
    void Awake () {

        for (int i = 0; i < CoverGroup.Count; i++)
        {
            foreach (Transform child in CoverGroup[i].transform)
            {
                m_coverSpots.Add(child.gameObject);
            }
        }
    }
	
    public void RemoveCoverSpot(GameObject cover)
    {
        m_coverSpots.Remove(cover);
    }

    public void AddCoverSpot(GameObject cover)
    {
        for (int i = 0; i < m_coverSpots.Count; i++)
        {
            if (m_coverSpots[i] == cover)
                return;
        }
        m_coverSpots.Add(cover);
    }
}
