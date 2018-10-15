using UnityEngine;
using System.Collections;

public class G_COVERSPOTfix : MonoBehaviour {


	
	void OnCollisionEnter(Collision collider)
	{
		if (collider.collider.gameObject.tag == "Enemy")
		{
			if (collider.collider.gameObject.transform.root.GetComponent<g_AIHealthScript>()!= null)
			{
				if (collider.collider.gameObject.transform.root.GetComponent<g_AIHealthScript>().health > 0)
					print (transform.name);
				
			}
		}
	}
}
