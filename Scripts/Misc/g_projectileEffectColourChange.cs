using UnityEngine;
using System.Collections;

public class g_projectileEffectColourChange : MonoBehaviour {

	[SerializeField]
	Color m_effectColor;
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<g_ParticleEffectEditor>().SetColor(m_effectColor);
	}
}
