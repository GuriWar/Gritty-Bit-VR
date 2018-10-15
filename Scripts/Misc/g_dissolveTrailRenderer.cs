using UnityEngine;
using System.Collections;

public class g_dissolveTrailRenderer : MonoBehaviour 
{
	public Transform toTrack;
	
	// Update is called once per frame
	void Update () 
	{
		if (toTrack != null)
		{
			transform.position = toTrack.position;
			transform.rotation = toTrack.rotation;
		}
		else
			Destroy(gameObject);
	}
}
