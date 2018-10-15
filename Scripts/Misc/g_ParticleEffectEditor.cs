using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class g_ParticleEffectEditor : MonoBehaviour {
	[SerializeField]
	List<GameObject> ParticleEffects = new List<GameObject>();
	Color m_effectColor;
	// Use this for initialization
	void Start () 
	{
		foreach(Transform child in transform)
		{
			if (child.GetComponent<ParticleSystem>())
			{
				ParticleEffects.Add(child.gameObject);
			}
		}
	}

	public void SetColor(Color x)
	{
		m_effectColor = x;
		for (int i =0; i <ParticleEffects.Count; i ++)
		{
			ParticleEffects[i].GetComponent<Renderer>().material.SetColor("_TintColor", m_effectColor);
		}
	}
}
